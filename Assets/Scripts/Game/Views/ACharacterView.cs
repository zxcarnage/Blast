using UnityEngine;

namespace Game.Views
{
    public abstract class ACharacterView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public Collider Collider { get; private set; }
    }
}