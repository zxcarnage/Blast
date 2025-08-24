using Ecs.Game.Components.Character;
using Ecs.Utils;
using Scellecs.Morpeh;
using Utils.Providers.GameField;

namespace Ecs.Game.Systems.Initialize
{
    public class SpawnPointInitializeSystem : IInitializer
    {
        private readonly IGameFieldProvider _gameFieldProvider;

        private Filter _spawnPointFilter;
        
        private Stash<TransformComponent> _transformStash;
        
        public World World { get; set; }

        public SpawnPointInitializeSystem(
            IGameFieldProvider gameFieldProvider
        )
        {
            _gameFieldProvider = gameFieldProvider;
        }

        public void OnAwake()
        {
            var spawnPoints = _gameFieldProvider.GameField.SpawnPoints;

            foreach (var spawnPoint in spawnPoints)
            {
                World.CreateSpawnPoint(spawnPoint);
            }
        }

        public void Dispose()
        {
        }
    }
}