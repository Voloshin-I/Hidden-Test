using HOG.Factories;
using HOG.Interfaces.Factories;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HOG.Installers
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private RepositoryLoader _repositoryLoader;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_repositoryLoader).As<IRepositoryDataProvider>();
            builder.RegisterEntryPoint<LevelModelFactory>();
            // LevelModelFactory factory = new LevelModelFactory();
            // factory.Check();
        }
   }
}
