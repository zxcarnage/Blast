using Ecs.Core.Utils;
using Ecs.Game.Systems.Enemy.Spawn;
using Ecs.Game.Systems.Initialize;
using Ecs.Game.Systems.Input;
using Ecs.Game.Systems.Movement;
using Ecs.Game.Systems.Timer;
using Scellecs.Morpeh;
using VContainer;
using VContainer.Unity;

namespace Ecs.Core.Installers
{
    public class GameEcsLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            var world = World.Default;
            
            builder.RegisterEntryPoint<RootWorld>();
            builder.RegisterInstance(world);
            
            builder.Register<PlayerInitializeSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<InputInitializeSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TimerInitializeSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SpawnPointInitializeSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<KeyboardInputSystem>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<TimerUpdateSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerMovementSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CameraRotationSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemySpawnSystem>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}