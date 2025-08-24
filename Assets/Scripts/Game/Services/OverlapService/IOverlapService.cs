using UnityEngine;

namespace Game.Services.OverlapService
{
    public interface IOverlapService
    {
        bool CheckOverlapSphere(
            Vector3 position, 
            float radius, 
            int searchLayerMask
        );
    }
}