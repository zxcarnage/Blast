using TMPro;
using UnityEngine;

namespace Game.Views.UI.UpgradeMenu
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _levelText;

        public void UpdateView(int level)
        {
            _levelText.text = $"Level: {level}";
        }
    }
}