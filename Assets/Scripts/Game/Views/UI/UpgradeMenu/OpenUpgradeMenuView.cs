using Game.Utils.UI;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views.UI.UpgradeMenu
{
    public class OpenUpgradeMenuView : MonoBehaviour //TODO only prototype thing
    {
        [SerializeField] 
        private UpgradeMenuView _upgradeMenuView;

        [SerializeField] 
        private Button _button;
        
        private void OnEnable()
        {
            _button.OnClickAsObservable().Subscribe(_ => OnClick()).AddTo(this);
        }
        
        private void OnClick()
        {
            _upgradeMenuView.ChangeState(EUpgradeWindowState.Shown); 
        }
    }
}