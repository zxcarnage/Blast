using Unity.Collections;
using UnityEngine;

namespace Game.Services.OverlapService.Impl
{
    public class OverlapService : IOverlapService
    {
        public bool CheckOverlapSphere(Vector3 position, float radius, int searchLayerMask)
        {
            var commands = new NativeArray<OverlapSphereCommand>(1, Allocator.TempJob);
            var colliderHitsForGrounded = new NativeArray<ColliderHit>(1, Allocator.TempJob);
            
            var parameters = new QueryParameters(searchLayerMask, hitTriggers: QueryTriggerInteraction.UseGlobal);
            commands[0] = new OverlapSphereCommand(position, radius, parameters);
            
            OverlapSphereCommand
                .ScheduleBatch(
                    commands, 
                    colliderHitsForGrounded, 
                    1, 
                    1
                )
                .Complete();
            
            if (colliderHitsForGrounded[0].collider != null)
            {
                colliderHitsForGrounded.Dispose();
                commands.Dispose();
                return true;
            }
            
            colliderHitsForGrounded.Dispose();
            commands.Dispose();
            return false;
        }
    }
}