using UnityEngine;

namespace Config.Player.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(PlayerShootingParameters), fileName = nameof(PlayerShootingParameters))]
    public class PlayerShootingParameters : ScriptableObject, IPlayerShootingParameters
    {
        [field: SerializeField]
        public float Damage { get; private set; }
    }
}