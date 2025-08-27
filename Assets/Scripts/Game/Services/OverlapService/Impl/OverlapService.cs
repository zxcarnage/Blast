using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Game.Services.OverlapService.Impl
{
    public class OverlapService : IOverlapService
    {
        private const int COMMAND_BATCH_SIZE = 1;
        private const int COMMAND_BATCH_COUNT = 1;
    
        public bool CheckOverlapSphere(Vector3 position, float radius, int searchLayerMask)
        {
            var commands = new NativeArray<OverlapSphereCommand>(COMMAND_BATCH_SIZE, Allocator.TempJob);
            var collidersHitForOverlap = new NativeArray<ColliderHit>(COMMAND_BATCH_SIZE, Allocator.TempJob);
        
            var parameters = new QueryParameters(searchLayerMask, hitTriggers: QueryTriggerInteraction.UseGlobal);
            commands[0] = new OverlapSphereCommand(position, radius, parameters);
        
            var handle = OverlapSphereCommand
                .ScheduleBatch(commands, collidersHitForOverlap, COMMAND_BATCH_SIZE, COMMAND_BATCH_COUNT);
            handle.Complete();

            var isHit = collidersHitForOverlap[0].collider != null;

            commands.Dispose();
            collidersHitForOverlap.Dispose();

            return isHit;
        }

        public RaycastHit GetRaycastHit(Vector3 from, Vector3 direction, float distance, int searchLayerMask)
        {
            var commands = new NativeArray<RaycastCommand>(COMMAND_BATCH_SIZE, Allocator.TempJob);
            var collidersHits = new NativeArray<RaycastHit>(COMMAND_BATCH_SIZE, Allocator.TempJob);
        
            var parameters = new QueryParameters(searchLayerMask, hitTriggers: QueryTriggerInteraction.UseGlobal);
            commands[0] = new RaycastCommand(from, direction, parameters, distance);
        
            var handle = RaycastCommand
                .ScheduleBatch(commands, collidersHits, COMMAND_BATCH_SIZE, COMMAND_BATCH_COUNT);
            handle.Complete();

            var hit = collidersHits[0];

            commands.Dispose();
            collidersHits.Dispose();

            return hit;
        }
    }
}