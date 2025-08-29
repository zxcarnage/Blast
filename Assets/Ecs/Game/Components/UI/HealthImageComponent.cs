using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.UI;

namespace Ecs.Game.Components.UI
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct HealthImageComponent : IComponent
    {
        public Image Value;
    }
}