using System.Collections.Generic;
using Ecs.Game.Components.Player;
using Ecs.Game.Components.UI;
using Game.Utils.UI;
using R3;
using Scellecs.Morpeh;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Utils;
using Utils.Transaction.Impls;
using Utils.UI;
using VContainer;

namespace Game.Views.UI.UpgradeMenu
{
    public class UpgradeMenuView : SerializedMonoBehaviour //TODO: Only prototyping in real-world needed VP-system
    {
        [SerializeField] 
        private CanvasGroup _menuCanvasGroup;

        [SerializeField]
        private ButtonR3View _upgradeButton;
        
        [SerializeField]
        private ButtonR3View _backButton;

        [SerializeField]
        private Dictionary<EUpgradeType, UpgradeItemView> _upgradeItemViews;
        
        private Dictionary<EUpgradeType, Transaction<int>> _upgradeItemData = new();

        private Filter _playerFilter;
        
        private Stash<ApplyUpgradeComponent> _applyUpgradeStash;
        
        private World _world;


        [Inject]
        private void Construct(World world)
        {
            _world = world;
        }
        
        private void OnEnable()
        {
            Subscribe();
            
            _playerFilter = _world.Filter
                .With<PlayerComponent>()
                .Build();
            
            _applyUpgradeStash = _world.GetStash<ApplyUpgradeComponent>();
            
            //TODO: Take savings
            
            foreach (var (upgradeType, upgradeItemView) in _upgradeItemViews)
            {
                InitializeItem(upgradeType, upgradeItemView);
            }

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
                upgradeItemView.OnClickCommand.Subscribe(_ => Upgrade(upgradeType)).AddTo(this);
                upgradeItemView.UpdateView(0);
                _upgradeItemData.Add(upgradeType, transaction);
            }
        }

        private void Upgrade(EUpgradeType upgradeType)
        {
            var targetTransaction = _upgradeItemData[upgradeType];
            targetTransaction.Add(ConstValues.LEVEL_UP_DELTA);
        }

        private void TryApplyUpgrade()
        {
            //TODO: Delete and check logic
            foreach (var (type, transaction) in _upgradeItemData)
            {
                transaction.Commit();
                foreach (var playerEntity in _playerFilter)
                {
                    _applyUpgradeStash.Set(playerEntity, new ApplyUpgradeComponent() { Value = type }); //TODO: ????
                }
            }
            ChangeState(EUpgradeWindowState.Hidden);
        }

        private void HideView()
        {
            foreach (var transaction in _upgradeItemData.Values)
            {
                transaction.Revert();
            }
            
            ChangeState(EUpgradeWindowState.Hidden);
        }
        
        public void ChangeState(EUpgradeWindowState state) //TODO: Only prototyping
        {
            var shown = state == EUpgradeWindowState.Shown;
            Cursor.visible = shown;
            Cursor.lockState = shown ? CursorLockMode.Confined : CursorLockMode.Locked;
            Time.timeScale = shown ? 0 : 1; //TODO: Only for prototype, in future change systems to APausableSystem etc.
            _menuCanvasGroup.alpha = shown ? 1 : 0;
        }
    }
}