using UnityEngine;

namespace Game.Views.Player
{
    public class PlayerView : ACharacterView
    {
        [field: SerializeField] public GameObject PlayerHead { get; private set; }
    }
}