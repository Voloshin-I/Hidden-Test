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
        public event Action<string> onLevelStarted;
        public event Action<LevelFinishResultType> onLevelFinished;
        public string levelID { get; private set; }
        
        public GameplayCurrentLevel(
            IDataProvider<LevelModel> levelDataProvider
            )
        {
            _levelDataProvider = levelDataProvider;
        }

        public void StartLevel(string levelId)
        {
            if (!_levelDataProvider.modelsByID.TryGetValue(levelId, out LevelModel levelModel))
            {
                throw new ArgumentException($"Level with ID {levelId} not found.");
                return;
            }

            this.levelID = levelId;
            
            GameObject.Instantiate(levelModel.prefab);
            
            onLevelStarted?.Invoke(levelId);
        }

        private IDataProvider<LevelModel> _levelDataProvider;
        
        
    }
}