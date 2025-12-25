using System;
using System.Collections.Generic;
using HOG.Interfaces.Gameplay;
using HOG.Interfaces.Models;
using HOG.Models;
using UnityEngine;

namespace HOG.Gameplay
{
    public class GameplayCurrentLevel : IGameplayCurrentLevel
    {
        public GameplayCurrentLevel(
            IDataProvider<LevelModel> levelDataProvider
            )
        {
            _levelDataProvider = levelDataProvider;
        }

        public event Action<LevelFinishResultType> OnLevelFinished;
        public void StartLevel(string levelId)
        {
            if (!_levelDataProvider.modelsByID.TryGetValue(levelId, out LevelModel levelModel))
            {
                throw new ArgumentException($"Level with ID {levelId} not found.");
                return;
            }

            GameObject.Instantiate(levelModel.prefab);
        }

        private IDataProvider<LevelModel> _levelDataProvider;
        
        
    }
}