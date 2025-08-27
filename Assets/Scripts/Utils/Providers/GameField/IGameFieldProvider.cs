using Game.Views;

namespace Utils.Providers.GameField
{
    public interface IGameFieldProvider
    {
        GameFieldView GameField { get; }
    }
}