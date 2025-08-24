using Game.Views.Player;
using UnityEngine;

namespace Game.Views
{
    public class GameFieldView : MonoBehaviour
    {
        [field: SerializeField] 
        public PlayerView Player { get; private set; }
        
        [field: SerializeField]
        public GameObject[] SpawnPoints { get; private set; }
    }
}