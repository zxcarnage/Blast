using Config.Base.Impl;
using Config.Player.Impl;

namespace Config.Base
{
    public interface IGeneralBase
    {
        PrefabsBase PrefabsBase { get; }
        PlayerMovementParameters PlayerMovementParameters { get; }
    }
}