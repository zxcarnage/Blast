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
            builder.RegisterInstance(_gameField);
        }

        private void RegisterProviders(IContainerBuilder builder)
        {
            builder.Register<GameFieldProvider>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}