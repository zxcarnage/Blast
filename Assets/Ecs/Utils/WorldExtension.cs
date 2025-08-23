using Ecs.Game.Components.Character;
using Ecs.Game.Components.Player;
using Game.Views;
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
        }

        public static void CreatePlayerHead(this World world, PlayerView playerView)
        {
            var playerHead = world.Filter.With<PlayerHeadComponent>().Build().First();
            
            world.GetStash<TransformComponent>().Set(playerHead, new TransformComponent() { Value = playerView.PlayerHead.transform });
        }
    }
}