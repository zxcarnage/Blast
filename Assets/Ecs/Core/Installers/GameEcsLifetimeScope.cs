using Ecs.Core.Utils;
using Ecs.Game.Systems.Initialize;
using Ecs.Game.Systems.Input;
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
            builder.Register<KeyboardInputSystem>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}