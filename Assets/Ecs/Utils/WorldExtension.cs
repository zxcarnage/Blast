using Ecs.Game.Components.Character;
using Ecs.Game.Components.Player;
using Scellecs.Morpeh;
using Views;

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
        }
    }
}