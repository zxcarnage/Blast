using UnityEngine;

namespace Views
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public Collider Collider { get; private set; }
    }
}