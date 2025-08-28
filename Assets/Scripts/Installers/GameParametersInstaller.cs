using Config.Base.Impl;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installers
{
    public class GameParametersInstaller : LifetimeScope
    {
        [SerializeField]
        private GeneralBase _generalBase;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_generalBase.PrefabsBase).AsImplementedInterfaces();
            builder.RegisterInstance(_generalBase.PlayerMovementParameters).AsImplementedInterfaces();
            builder.RegisterInstance(_generalBase.CameraParameters).AsImplementedInterfaces();
            builder.RegisterInstance(_generalBase.EnemyBase).AsImplementedInterfaces();
            builder.RegisterInstance(_generalBase.EnemySpawnerParameters).AsImplementedInterfaces();
            builder.RegisterInstance(_generalBase.PlayerShootingParameters).AsImplementedInterfaces();
            builder.RegisterInstance(_generalBase.EnemyParameters).AsImplementedInterfaces();
            builder.RegisterInstance(_generalBase.HealthbarParameters).AsImplementedInterfaces();
            builder.RegisterInstance(_generalBase.PlayerBasicParameters).AsImplementedInterfaces();
            builder.RegisterInstance(_generalBase.UpgradeDataParameters).AsImplementedInterfaces();
        }
    }
}