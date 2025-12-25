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
            IDataProvider<LevelModel> levelDataProvider,
            IDataProvider<LevelFilteredDataModel> levelFilteredDataProvider,
            ITimer timer)
        {
            _levelDataProvider = levelDataProvider;
            _levelFilteredDataProvider = levelFilteredDataProvider;
            _timer = timer;
            _timer.onExpired += OnTimerExpired;
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
            IReadOnlyList<string> sortedIDs = _levelFilteredDataProvider.modelsByID[levelId].sortedGameplayItemIDs;
            SpriteRenderer[] children = _levelObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer child in children)
            {
                if (child.name != "Background")
                    child.gameObject.SetActive(false);
            }
            foreach (string id in sortedIDs)
            {
                foreach (SpriteRenderer child in children)
                {
                    if (child.name == id)
                    {
                        child.gameObject.AddComponent<GameplayItemView>();
                        _remainingItems.Add(child.name);
                        child.gameObject.SetActive(true);
                    }
                }
            }

            if (levelModel.settings.timerDurationSeconds > 0)
            {
                _timer.Set(levelModel.settings.timerDurationSeconds);
                _timer.Start();
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
                        _timer.Cancel();
                        onLevelFinished?.Invoke(LevelFinishResultType.Completed);
                    }
                    return;
                }
            }
        }

        private void OnTimerExpired()
        {
            onLevelFinished?.Invoke(LevelFinishResultType.Failed);
            _remainingItems.Clear();
        }

        private IDataProvider<LevelModel> _levelDataProvider;
        private IDataProvider<LevelFilteredDataModel> _levelFilteredDataProvider;
        private ITimer _timer;

        private GameObject _levelObject;
        private List<string> _remainingItems = new();

        /// <summary>
        /// We can pick up only first 3 items
        /// </summary>
        private const int availableItemCount = 3;
    }
}