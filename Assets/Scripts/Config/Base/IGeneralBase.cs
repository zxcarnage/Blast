using Config.Base.Impl;
using Config.Camera.Impl;
using Config.Enemy.EnemyBase.Impl;
using Config.Enemy.EnemySpawner.Impl;
using Config.Enemy.Impl;
using Config.Player.Impl;
using Config.UI.Healthbar.Impl;

namespace Config.Base
{
    public interface IGeneralBase //Замена SO инсталлеру из Zenject
    {
        PrefabsBase PrefabsBase { get; }
        PlayerMovementParameters PlayerMovementParameters { get; }
        CameraParameters CameraParameters { get; }
        EnemyBase EnemyBase { get; }
        EnemySpawnerParameters EnemySpawnerParameters { get; }
        PlayerShootingParameters PlayerShootingParameters { get; }
        EnemyParameters EnemyParameters { get; }
        HealthbarParameters HealthbarParameters { get; }
        PlayerBasicParameters PlayerBasicParameters { get; }
    }
}