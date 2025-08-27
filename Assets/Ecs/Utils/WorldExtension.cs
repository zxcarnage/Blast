using Ecs.Game.Components.Character;
using Ecs.Game.Components.Player;
using Ecs.Game.Components.SpawnPoint;
using Ecs.Game.Components.Timer;
using Game.Views.Player;
using Scellecs.Morpeh;
using UnityEngine;

namespace Ecs.Utils
{
    public static class WorldExtension
    {
        public static void CreatePlayer(this World world, PlayerView playerView)
        {
            var player = world.Filter.With<PlayerComponent>().Build().First();
            
            world.GetStash<ColliderComponent>().Set(player, new ColliderComponent() { Value = playerView.Collider});
            world.GetStash<RigidbodyComponent>().Set(player, new RigidbodyComponent() { Value = playerView.Rigidbody});
            world.GetStash<TransformComponent>().Set(player, new TransformComponent() { Value = playerView.Transform });
            world.GetStash<LookDirectionComponent>().Set(player, new LookDirectionComponent() { Value = Vector3.zero });
            world.GetStash<PlayerSkillpointComponent>().Set(player, new PlayerSkillpointComponent() { Value = 0 /*TODO: TEMP, LATER REIMPLEMENT WITH SAVINGS*/ });
        }

        public static Entity CreateTimer(this World world)
        {
            var timerEntity = world.CreateEntity();
            world.GetStash<TimerComponent>().Add(timerEntity);
            return timerEntity;
        }
        
        public static void CreateSpawnPoint(this World world, GameObject spawnPoint)
        {
            var spawnPointEntity = world.CreateEntity();
            world.GetStash<SpawnPointComponent>().Add(spawnPointEntity);
            world.GetStash<TransformComponent>().Add(spawnPointEntity,  new TransformComponent() { Value = spawnPoint.transform });
        }

        public static void CreatePlayerHead(this World world, PlayerView playerView)
        {
            var playerHead = world.Filter.With<PlayerHeadComponent>().Build().First();
            
            world.GetStash<TransformComponent>().Set(playerHead, new TransformComponent() { Value = playerView.PlayerHead.transform });
        }
    }
}