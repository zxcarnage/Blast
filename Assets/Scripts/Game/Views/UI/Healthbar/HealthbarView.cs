using Config.Player;
using DG.Tweening;
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

        [Inject]
        public void Construct(
            IPlayerBasicParameters playerBasicParameters    
        )
        {
            _playerBasicParameters = playerBasicParameters;
        }

        public void UpdateView(int newHealth)
        {
            _healthbarImage
                .DOFillAmount((float)newHealth / _playerBasicParameters.Health, _playerBasicParameters.HealthbarAnimationDuration)
                .SetEase(_playerBasicParameters.HealthbarAnimationEase);
        }
    }
}