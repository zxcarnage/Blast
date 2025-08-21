using VContainer;

namespace Ecs.Core.Installers
{
    public static class GameEcsSystems
    {
        public static void Register(IContainerBuilder builder)
        {
            Urgent(builder);
            High(builder);
            Medium(builder);
            Low(builder);
        }

        private static void Urgent(IContainerBuilder builder)
        {
            //Install systems
        }

        private static void High(IContainerBuilder builder)
        {
            //Install systems
        }

        private static void Medium(IContainerBuilder builder)
        {
            //Install systems
        }

        private static void Low(IContainerBuilder builder)
        {
            //Install systems
        }
    }
}