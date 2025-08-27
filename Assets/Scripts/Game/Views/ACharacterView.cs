using UnityEngine;

namespace Game.Views
{
    public abstract class ACharacterView : AObjectView
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public Collider Collider { get; private set; }
    }
}