using Config.Base.Impl;
using Config.Camera.Impl;
using Config.Enemy.EnemyBase.Impl;
using Config.Enemy.EnemySpawner.Impl;
using Config.Player.Impl;

namespace Config.Base
{
    public interface IGeneralBase
    {
        PrefabsBase PrefabsBase { get; }
        PlayerMovementParameters PlayerMovementParameters { get; }
        CameraParameters CameraParameters { get; }
        EnemyBase EnemyBase { get; }
        EnemySpawnerParameters EnemySpawnerParameters { get; }
    }
}