using Ecs.Core.Utils;
using Ecs.Game.Systems.Character;
using Ecs.Game.Systems.Enemy.Movement;
using Ecs.Game.Systems.Enemy.Shooting;
using Ecs.Game.Systems.Enemy.Spawn;
using Ecs.Game.Systems.Initialize;
using Ecs.Game.Systems.Input;
using Ecs.Game.Systems.Movement;
using Ecs.Game.Systems.Shooting;
using Ecs.Game.Systems.Timer;
using VContainer;
using VContainer.Unity;

namespace Ecs.Core.Installers
{
    public class GameEcsLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<RootWorld>();
            
            builder.Register<PlayerInitializeSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<InputInitializeSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TimerInitializeSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SpawnPointInitializeSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<KeyboardInputSystem>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<TimerUpdateSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerMovementSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<CameraRotationSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemySpawnSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemyRotationSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ShootingSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemyDamageHandlerSystem>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemyDeathSystem>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}