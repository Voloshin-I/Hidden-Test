using System.Collections.Generic;
using HOG.Factories;
using HOG.Gameplay;
using HOG.Interfaces.Factories;
using HOG.Interfaces.Gameplay;
using HOG.Interfaces.Models;
using HOG.Models;
using HOG.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HOG.Installers
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private RepositoryLoader _repositoryLoader;
        [SerializeField]
        private GameplayRoot _gameplayRoot;

        protected override void Configure(IContainerBuilder builder)
        {
            // We create DataProvider manually, because we need the factory only once
            LevelModelFactory levelModelFactory = new LevelModelFactory(_repositoryLoader);
            IEnumerable<LevelModel> levelModels = levelModelFactory.GetAllLevelModels();

            LevelProvider levelProvider = new LevelProvider(levelModels);
            builder.RegisterInstance(levelProvider).As<IDataProvider<LevelModel>>();
            
            LevelFilteredProvider levelFilteredProvider = new LevelFilteredProvider(levelModels);
            builder.RegisterInstance(levelFilteredProvider).As<IDataProvider<LevelFilteredDataModel>>();

            builder.Register<IGameplayCurrentLevel, GameplayCurrentLevel>(Lifetime.Singleton);
            builder.Register<ITimer, Timer>(Lifetime.Singleton).As<ITickable>();
            
            builder.RegisterComponentInHierarchy<LevelView>().As<IInitializable>();
            builder.RegisterComponentInHierarchy<WinView>().As<IInitializable>();
            builder.RegisterComponentInHierarchy<LoseView>().As<IInitializable>();
            builder.RegisterComponentInHierarchy<TimerView>().As<IInitializable>();
            
            builder.RegisterEntryPoint<GameplayRoot>();
        }
   }
}
