using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views.UI.UpgradeMenu
{
    public abstract class AButtonView : MonoBehaviour
    {
        [SerializeField] 
        private Button _button;
        
        private void OnEnable()
        {
            _button.OnClickAsObservable().Subscribe(_ => OnClick()).AddTo(this);
        }
        
        protected abstract void OnClick();
    }
}