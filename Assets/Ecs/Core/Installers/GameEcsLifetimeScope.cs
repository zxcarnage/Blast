using Ecs.Core.Utils;
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

            GameEcsSystems.Register(builder);
        }
    }
}