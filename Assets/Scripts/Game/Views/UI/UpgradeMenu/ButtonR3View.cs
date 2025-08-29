using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views.UI.UpgradeMenu
{
    public class ButtonR3View : MonoBehaviour
    {
        [SerializeField]
        private Button _button;
        
        public ReactiveCommand OnClickCommand { get; private set; } = new ReactiveCommand();

        private void OnEnable()
        {
            _button.OnClickAsObservable().Subscribe(_ => OnClickCommand.Execute(Unit.Default)).AddTo(this);
        }
    }
}