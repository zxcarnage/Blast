using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using Utils.Figure;

namespace Ecs.Game.Components.GameField
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct GameFieldComponent : IComponent
    {
        public FigureData[,] Value;
    }
}