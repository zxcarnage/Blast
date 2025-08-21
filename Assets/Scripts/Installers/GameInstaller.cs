using UnityEngine;
using Utils.Providers.GameField.Impl;
using VContainer;
using VContainer.Unity;
using Views;

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
        }

        private void RegisterInstances(IContainerBuilder builder)
        {
            builder.RegisterInstance(_gameField);
        }
    }
}