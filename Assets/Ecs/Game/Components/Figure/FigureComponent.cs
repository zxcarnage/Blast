using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Ecs.Game.Components.Figure
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct FigureComponent : IComponent 
    {
    
    }
}