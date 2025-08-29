using System.Collections.Generic;
using Config.UpgradeData;
using Core.Dao;
using Ecs.Game.Components.Player;
using Ecs.Game.Components.Upgrade;
using Game.Utils.Dao;
using Game.Utils.Dao.UpgradeData;
using Game.Utils.UI;
using Game.Utils.UpgradeData;
using R3;
using Scellecs.Morpeh;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils;
using Utils.Transaction;
using Utils.Transaction.Impls;
using Utils.UI;
using VContainer;

namespace Game.Views.UI.UpgradeMenu
{
    public class UpgradeMenuView : SerializedMonoBehaviour //TODO: Only prototyping in real-world needed VC-system
    {
        [SerializeField] 
        private CanvasGroup _menuCanvasGroup;

        [SerializeField]
        private ButtonR3View _upgradeButton;
        
        [SerializeField]
        private ButtonR3View _backButton;
        
        [SerializeField]
        private LevelView _levelView;

        [SerializeField]
        private Dictionary<EUpgradeType, UpgradeItemView> _upgradeItemViews;
        
        private Dictionary<EUpgradeType, ITransaction<int>> _upgradeItemData = new();
        private ITransaction<int> _levelTransaction;
        private IUpgradeDataParameters _upgradeDataParameters;
        private World _world;

        private Filter _playerFilter;

        private Stash<ApplyHealthUpgradeComponent> _applyHealthUpgradeStash;
        private Stash<ApplyDamageUpgradeComponent> _applyDamageUpgradeStash;
        private Stash<ApplySpeedUpgradeComponent> _applySpeedUpgradeStash;
        private Stash<PlayerSkillpointComponent> _playerSkillpointStash;
        private IDao<UpgradeSaveData> _upgradeSaveDao;
        private IDao<LevelSaveData> _levelSaveDao;


        [Inject]
        private void Construct(
            World world,
            IUpgradeDataParameters upgradeDataParameters,
            IDao<UpgradeSaveData> upgradeSaveDao,
            IDao<LevelSaveData> levelSaveDao
        )
        {
            _upgradeDataParameters = upgradeDataParameters;
            _upgradeSaveDao = upgradeSaveDao;
            _levelSaveDao = levelSaveDao;
            _world = world;
        }

        private void OnEnable()
        {
            Subscribe();
            GetStashes();
            
            _playerFilter = _world.Filter
                .With<PlayerComponent>()
                .Build();
            
            //TODO: Take savings
            foreach (var (upgradeType, upgradeItemView) in _upgradeItemViews)
            {
                InitializeItem(upgradeType, upgradeItemView);
            }
            
            _levelTransaction = new Transaction<int>(new IntDeltaApplier());

            return;

            void Subscribe()
            {
                _upgradeButton.OnClickCommand.Subscribe(_ => TryApplyUpgrade()).AddTo(this);
                _backButton.OnClickCommand.Subscribe(_ => HideView()).AddTo(this);
            }

            void InitializeItem(EUpgradeType upgradeType, UpgradeItemView upgradeItemView)
            {
                var transaction = new Transaction<int>(new IntDeltaApplier());
                transaction.SaveBackup(0); //TODO: save value here
                upgradeItemView.OnClickCommand.Subscribe(_ => OnUpgradeButton(upgradeType, upgradeItemView)).AddTo(this);
                upgradeItemView.UpdateView(0);
                _upgradeItemData.Add(upgradeType, transaction);
            }

            

            void GetStashes()
            {
                _applyHealthUpgradeStash = _world.GetStash<ApplyHealthUpgradeComponent>();
                _applyDamageUpgradeStash = _world.GetStash<ApplyDamageUpgradeComponent>();
                _applySpeedUpgradeStash = _world.GetStash<ApplySpeedUpgradeComponent>();
                _playerSkillpointStash = _world.GetStash<PlayerSkillpointComponent>();
            }
        }

        private void Start()
        {
            InitializeSavings();

            return;

            void InitializeSavings()
            {
                TryInitializeUpgradeSavings();
                TryInitializeLevelSavings();
                
                return;

                void TryInitializeUpgradeSavings()
                {
                    var saveData = _upgradeSaveDao.Load();

                    if (saveData?.UpgradeDatas == null)
                        return;
                
                    foreach (var data in saveData.UpgradeDatas)
                    {
                        var targetTransaction = _upgradeItemData[data.UpgradeType];
                        targetTransaction.SaveBackup(data.Level);
                    }
                }

                void TryInitializeLevelSavings()
                {
                    var saveData = _levelSaveDao.Load();
                    
                    if (saveData == null)
                        return;

                    _levelTransaction.SaveBackup(saveData.Level);
                }
                
            }
        }


        private void OnUpgradeButton(EUpgradeType upgradeType, UpgradeItemView upgradeItemView)
        {
            var targetTransaction = _upgradeItemData[upgradeType];
            var currentValue = targetTransaction.Add(ConstValues.LEVEL_UP_DELTA);
            upgradeItemView.UpdateView(currentValue);
            DecreaseLevel();
            ValidateLevels();
        }

        private void TryApplyUpgrade()
        {
            var saveData = new List<UpgradeData>();
            foreach (var (type, transaction) in _upgradeItemData)
            {
                saveData.Add(new UpgradeData() { UpgradeType = type, Level = transaction.CurrentValue });
                
                if (!transaction.IsDirty)
                    continue;

                var targetLevel = transaction.Commit();
                foreach (var playerEntity in _playerFilter)
                {
                    ApplyUpgrade(type, targetLevel, playerEntity); 
                    _levelTransaction.Commit();
                    _playerSkillpointStash.Set(playerEntity,
                        new PlayerSkillpointComponent() { Value = _levelTransaction.CurrentValue });
                }
            }

            var upgradeSaveData = new UpgradeSaveData() { UpgradeDatas = saveData.ToArray() };
            _upgradeSaveDao.Save(upgradeSaveData);
            _levelSaveDao.Save(new LevelSaveData() { Level = _levelTransaction.CurrentValue });
            ChangeState(EUpgradeWindowState.Hidden);
        }
        
        private void ApplyUpgrade(EUpgradeType type, int targetLevel, Entity player)
        {
            switch (type)
            {
                case EUpgradeType.Health:
                    _applyHealthUpgradeStash.Set(player, new ApplyHealthUpgradeComponent() { Value = targetLevel });
                    break;
                case EUpgradeType.Damage:
                    _applyDamageUpgradeStash.Set(player, new ApplyDamageUpgradeComponent() { Value = targetLevel });
                    break;
                case EUpgradeType.Speed:
                    _applySpeedUpgradeStash.Set(player, new ApplySpeedUpgradeComponent() { Value = targetLevel });
                    break;
            }
        }

        private void DecreaseLevel()
        {
            _levelTransaction.Decrease(ConstValues.LEVEL_DECREASE_DELTA);
            _levelView.UpdateView(_levelTransaction.CurrentValue);
        }

        public void ShowView()
        {
            foreach (var (type, transaction) in _upgradeItemData)
            {
                _upgradeItemViews[type].UpdateView(transaction.CurrentValue);
            }

            ChangeState(EUpgradeWindowState.Shown);
        }

        private void HideView()
        {
            foreach (var (type, transaction) in _upgradeItemData)
            {
                transaction.Revert();
                _upgradeItemViews[type].UpdateView(transaction.CurrentValue);
            }

            _levelTransaction.Revert();
            ValidateLevels();
            ChangeState(EUpgradeWindowState.Hidden);
        }

        private void ValidateLevels()
        {
            foreach (var (type, transaction) in _upgradeItemData)
            {
                var currentSkillLevel = transaction.CurrentValue;
                var isLevelValid = _upgradeDataParameters.MaxLevels[type].MaxLevel > currentSkillLevel && _levelTransaction.CurrentValue > 0;
                _upgradeItemViews[type].ChangeButtonState(isLevelValid);
            }
        }

        private void ChangeState(EUpgradeWindowState state) //TODO: Only prototyping
        {
            var shown = state == EUpgradeWindowState.Shown;
            Cursor.visible = shown;
            Cursor.lockState = shown ? CursorLockMode.Confined : CursorLockMode.Locked;
            Time.timeScale = shown ? 0 : 1; //TODO: Only for prototype, in future change systems to APausableSystem etc.
            _menuCanvasGroup.alpha = shown ? 1 : 0;

            if (!shown)
                return;
            
            InitializeLevelTransaction();
            ValidateLevels();
        }
        
        private void InitializeLevelTransaction()
        {
            foreach (var playerEntity in _playerFilter)
            {
                var currentPlayerLevel = _playerSkillpointStash.Get(playerEntity).Value;
                _levelTransaction.SaveBackup(currentPlayerLevel);
                _levelView.UpdateView(_levelTransaction!.CurrentValue);
            }
        }
    }
}