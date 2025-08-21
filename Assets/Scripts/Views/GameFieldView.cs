using UnityEngine;

namespace Views
{
    public class GameFieldView : MonoBehaviour
    {
        [field: SerializeField] 
        public PlayerView Player { get; private set; }
    }
}