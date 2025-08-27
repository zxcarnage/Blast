using Ecs.Game.Components.Timer;
using Ecs.Utils;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Ecs.Game.Systems.Initialize
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class TimerInitializeSystem : IInitializer
    {
        public World World { get; set; }

        public void OnAwake()
        {
            CreateEnemySpawnTimer();
        }

        private void CreateEnemySpawnTimer()
        {
            var timer = World.CreateTimer();
            World.GetStash<EnemySpawnTimerComponent>().Add(timer);
        }

        public void Dispose()
        {
        }
    }
}