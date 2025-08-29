using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views.UI.UpgradeMenu
{
    public class UpgradeItemView : MonoBehaviour
    {
        [SerializeField]
        private Button _upgradeButton;
        
        [SerializeField]
        private TMP_Text _upgradeText;

        public ReactiveCommand OnClickCommand { get; private set; } = new ReactiveCommand();
        

        public void ChangeButtonState(bool state)
        {
            _upgradeButton.interactable = state;
        }

        private void OnEnable()
        {
            _upgradeButton.OnClickAsObservable().Subscribe(_ => OnClickCommand.Execute(Unit.Default)).AddTo(this);
        }
        

        public void UpdateView(int newLevel)
        {
            _upgradeText.text = $"Level: {newLevel}";
        }
    }
}