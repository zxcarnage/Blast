using Ecs.Game.Components.Timer;
using Ecs.Utils;
using Scellecs.Morpeh;

namespace Ecs.Game.Systems.Initialize
{
    public class TimerInitializeSystem : IInitializer
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