using System.Collections.Generic;
using System.Linq;
using HOG.Installers;
using HOG.Models;
using HOG.Repositories;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HOG.Factories
{
    public class LevelModelFactory// : ITickable
    {
        public LevelModelFactory(IRepositoryDataProvider repositoryDataProvider)
        {
            _dataProvider = repositoryDataProvider;
        }
        
        public IEnumerable<LevelModel> GetAllLevelModels()
        {
            IEnumerable<LevelModelRepositoryData> repositoryLevels =
                _dataProvider.GetAllOfType<LevelModelRepositoryData>();

            LevelModel[] result = new LevelModel[repositoryLevels.Count()];
            int i = 0;
            foreach (LevelModelRepositoryData repositoryLevel in repositoryLevels)
            {
                LevelModel levelModel = new LevelModel(
                    repositoryLevel.levelAsset.name,
                    repositoryLevel.levelAsset,
                    repositoryLevel.levelSettings);
                result[i] = levelModel;
                i++;
            }
            return result;
        }
        private IRepositoryDataProvider _dataProvider;
    }
}
