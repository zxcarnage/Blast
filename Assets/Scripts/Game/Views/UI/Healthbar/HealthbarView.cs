using System;
using Config.Player;
using DG.Tweening;
using Ecs.Game.Components.Character;
using Ecs.Game.Components.Player;
using Scellecs.Morpeh;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Game.Views.UI.Healthbar
{
    public class HealthbarView : MonoBehaviour
    {
        [SerializeField] 
        private Image _healthbarImage;
        
        [SerializeField]
        private TMP_Text _healthbarText;

        private IPlayerBasicParameters _playerBasicParameters;
        
        private Filter _playerFilter;
        
        private Stash<MaxHealthComponent> _maxHealthStash;
        private World _world;

        [Inject]
        public void Construct(
            World world,
            IPlayerBasicParameters playerBasicParameters
        )
        {
            _playerBasicParameters = playerBasicParameters;
            _world = world;
        }

        private void OnEnable()
        {
            _playerFilter = _world.Filter
                .With<PlayerComponent>()
                .Build();

            _maxHealthStash = _world.GetStash<MaxHealthComponent>();
        }

        public void UpdateView(float newHealth)
        {
            foreach (var playerEntity in _playerFilter)
            {
                var currentMaxHealth = _maxHealthStash.Get(playerEntity).Value;

                _healthbarText.text = $"{newHealth :F}";
                
                _healthbarImage
                    .DOFillAmount(newHealth / currentMaxHealth, _playerBasicParameters.HealthbarAnimationDuration)
                    .SetEase(_playerBasicParameters.HealthbarAnimationEase);
            }
        }
    }
}