using UnityEngine;
using UnityEngine.UI;

namespace Game.Views.Enemy
{
    public class EnemyView : ACharacterView
    {
        [field: SerializeField] 
        public Image HealthbarImage { get; private set; }
    }
}