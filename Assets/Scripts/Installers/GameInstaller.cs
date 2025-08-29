using Core.Dao.Impl;
using Core.Utils;
using Game.Services.Factory.Enemy.Impl;
using Game.Services.OverlapService.Impl;
using Game.Services.Pool.Enemy.Impls;
using Game.Services.Savings.Impl;
using Game.Utils.UpgradeData;
using Game.Views;
using Scellecs.Morpeh;
using UnityEngine;
using Utils.Providers.GameField.Impl;
using VContainer;
using VContainer.Unity;

namespace Installers
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] private GameFieldView _gameField;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterDao(builder);
            RegisterWorld(builder);
            RegisterProviders(builder);
            RegisterServices(builder);
            RegisterInstances(builder);
        }

        private void RegisterDao(IContainerBuilder builder)
        {
            builder.Register<LocalStorageDao<UpgradeSaveData>>(Lifetime.Singleton).AsImplementedInterfaces().WithParameter(DaoSavingPathKeys.UPGRADES);
        }

        private void RegisterWorld(IContainerBuilder builder)
        {
            var world = World.Default;
            
            builder.RegisterInstance(world);
        }

        private void RegisterProviders(IContainerBuilder builder)
        {
            builder.Register<GameFieldProvider>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<PlayerInputAction>(Lifetime.Singleton);
            builder.Register<EnemyPool>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<OverlapService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemyFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SaveUpgradesService>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void RegisterInstances(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameField);
        }
    }
}