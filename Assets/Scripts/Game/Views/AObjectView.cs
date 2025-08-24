using UnityEngine;

namespace Game.Views
{
    public class AObjectView : MonoBehaviour
    {
        [field: SerializeField] public Transform Transform { get; private set; }
    }
}