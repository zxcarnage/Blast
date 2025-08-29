using Ecs.Game.Components.UI;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

namespace Ecs.Game.Providers.UI.PlayerHealthbar
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class PlayerHealthbarProvider : MonoProvider<PlayerHealthbarComponent>
    {
        
    }
}