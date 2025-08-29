using UnityEngine;

namespace Config.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerMovementParameters), fileName = nameof(PlayerMovementParameters))]
    public class PlayerMovementParameters : ScriptableObject, IPlayerMovementParameters
    {
        [field: SerializeField]
        public float Speed { get; private set; }
    }
}