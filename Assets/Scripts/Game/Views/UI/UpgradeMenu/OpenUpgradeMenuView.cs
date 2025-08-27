using Game.Utils.UI;
using UnityEngine;

namespace Game.Views.UI.UpgradeMenu
{
    public class OpenUpgradeMenuView : AButtonView
    {
        [SerializeField] 
        private UpgradeMenuView _upgradeMenuView;

        protected override void OnClick()
        {
            _upgradeMenuView.ChangeState(EUpgradeWindowState.Shown);
        }
    }
}