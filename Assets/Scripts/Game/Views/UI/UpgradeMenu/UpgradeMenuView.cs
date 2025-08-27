using Game.Utils.UI;
using UnityEngine;

namespace Game.Views.UI.UpgradeMenu
{
    public class UpgradeMenuView : MonoBehaviour
    {
        [SerializeField] 
        private CanvasGroup _menuCanvasGroup;

        public void ChangeState(EUpgradeWindowState state)
        {
            var shown = state == EUpgradeWindowState.Shown;
            Cursor.visible = shown;
            Cursor.lockState = shown ? CursorLockMode.Confined : CursorLockMode.Locked;
            Time.timeScale = shown ? 0 : 1; //TODO: Only for prototype, in future change systems to APausableSystem etc.
            _menuCanvasGroup.alpha = shown ? 1 : 0;
        }
    }
}