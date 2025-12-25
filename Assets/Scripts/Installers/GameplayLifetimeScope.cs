using HOG.Factories;
using HOG.Gameplay;
using HOG.Interfaces.Factories;
using HOG.Interfaces.Gameplay;
using HOG.Interfaces.Models;
using HOG.Models;
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
            LevelProvider levelProvider = new LevelProvider(levelModelFactory.GetAllLevelModels());
            builder.RegisterInstance(levelProvider).As<IDataProvider<LevelModel>>();
            builder.Register<IGameplayCurrentLevel, GameplayCurrentLevel>(Lifetime.Singleton);
            builder.RegisterEntryPoint<GameplayRoot>();
        }
   }
}
