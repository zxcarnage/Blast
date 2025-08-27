using Ecs.Game.Components.Player;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

namespace Ecs.Game.Providers.Player
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerHeadProvider : MonoProvider<PlayerHeadComponent>
    {
        
    }
}