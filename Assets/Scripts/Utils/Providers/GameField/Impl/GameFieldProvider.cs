using Game.Views;

namespace Utils.Providers.GameField.Impl
{
    public class GameFieldProvider : IGameFieldProvider
    {
        public GameFieldView GameField { get; }

        public GameFieldProvider(GameFieldView gameField)
        {
            GameField = gameField;
        }
    }
}