using System.Collections.Generic;
using HOG.Installers;
using HOG.Models;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HOG.Factories
{
    public class LevelModelFactory// : ITickable
    {
        // [Inject]
        // public IRepositoryDataProvider repositoryDataProvider;
        public LevelModelFactory(IRepositoryDataProvider repositoryDataProvider)
        {
            Debug.LogError(repositoryDataProvider.GetType().Name);
            //this.repositoryDataProvider = repositoryDataProvider;
        }
        private IRepositoryDataProvider _dataProvider;
        // public LevelModelFactory()
        // {
        //     Debug.LogError(repositoryDataProvider == null);
        // } 
        
        public LevelModel[] GetAllLevelModels()
        {
            return new LevelModel[] { };
        }

        public void Check()
        {
            // Debug.LogError(repositoryDataProvider == null);
        }

        // void ITickable.Tick()
        // {
        //     Debug.LogError(repositoryDataProvider == null);
        // }
    }
}
