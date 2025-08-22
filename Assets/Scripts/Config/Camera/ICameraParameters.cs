using UnityEngine;

namespace Config.Camera
{
    public interface ICameraParameters
    {
        float Sensitivity { get; }
        Vector2Int MinMaxY { get; }
    }
}