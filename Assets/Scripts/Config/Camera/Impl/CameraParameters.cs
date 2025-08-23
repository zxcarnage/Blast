using Sirenix.OdinInspector;
using UnityEngine;

namespace Config.Camera.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(CameraParameters), fileName = nameof(CameraParameters))]
    
    public class CameraParameters : ScriptableObject, ICameraParameters
    {
        [field: SerializeField]
        public float Sensitivity { get; private set; }
        
        [field: MinMaxSlider(-90f,90f)]
        [field: SerializeField]
        public Vector2Int MinMaxY { get; private set; }
    }
}