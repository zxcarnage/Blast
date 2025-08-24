using Game.Services.OverlapService.Impl;
using Game.Services.Pool.Enemy.Impls;
using Game.Views;
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
            RegisterProviders(builder);
            RegisterServices(builder);
            RegisterInstances(builder);
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
        }

        private void RegisterInstances(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameField);
        }
    }
}