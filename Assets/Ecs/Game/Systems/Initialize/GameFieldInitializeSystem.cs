using Config.GameField;
using Ecs.Game.Components.GameField;
using Scellecs.Morpeh;

namespace Ecs.Game.Systems.Initialize
{
    public class GameFieldInitializeSystem : IInitializer
    {
        private readonly IGameFieldParameters _gameFieldParameters;

        public World World { get; set; }

        public GameFieldInitializeSystem(
            IGameFieldParameters gameFieldParameters    
        )
        {
            _gameFieldParameters = gameFieldParameters;
        }

        public void OnAwake()
        {
            var gameFieldEntity = World.CreateEntity();
            World.GetStash<GameFieldComponent>().Add(gameFieldEntity);
            
        }

        public void Dispose()
        {
            
        }
    }
}