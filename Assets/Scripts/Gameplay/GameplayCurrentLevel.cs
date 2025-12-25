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
        public event Action<string> onItemPickedUp;
        public string levelID { get; private set; }
        
        public GameplayCurrentLevel(
            IDataProvider<LevelModel> levelDataProvider
            )
        {
            _levelDataProvider = levelDataProvider;
            GameplayItemView.onClick += OnItemPickedUp;
        }

        public void StartLevel(string levelId)
        {
            if (!_levelDataProvider.modelsByID.TryGetValue(levelId, out LevelModel levelModel))
            {
                throw new ArgumentException($"Level with ID {levelId} not found.");
                return;
            }

            if (_levelObject != null)
            {
                GameObject.Destroy(_levelObject);
            }
            _remainingItems.Clear();

            this.levelID = levelId;
            
            _levelObject = GameObject.Instantiate(levelModel.prefab);
            foreach (SpriteRenderer child in _levelObject.GetComponentsInChildren<SpriteRenderer>())
            {
                if (child.name.Contains("_1"))
                {
                    child.gameObject.SetActive(false);
                    continue;
                }
                
                if (child.name == "Background")
                {
                    child.gameObject.SetActive(true);
                    continue;
                }

                child.gameObject.AddComponent<GameplayItemView>();
                _remainingItems.Add(child.name);
            }
            
            onLevelStarted?.Invoke(levelId);
        }

        private void OnItemPickedUp(GameplayItemView itemView)
        {
            for (int i = 0; i < availableItemCount && i < _remainingItems.Count; i++)
            {
                string itemName = itemView.name;
                if (_remainingItems[i] == itemName)
                {
                    _remainingItems.RemoveAt(i);
                    itemView.Hide();
                    onItemPickedUp?.Invoke(itemView.name);

                    if (_remainingItems.Count == 0)
                    {
                        onLevelFinished?.Invoke(LevelFinishResultType.Completed);
                    }
                    return;
                }
            }
        }
        
        private IDataProvider<LevelModel> _levelDataProvider;

        private GameObject _levelObject;
        private List<string> _remainingItems = new();

        /// <summary>
        /// We can pick up only first 3 items
        /// </summary>
        private const int availableItemCount = 3;
    }
}