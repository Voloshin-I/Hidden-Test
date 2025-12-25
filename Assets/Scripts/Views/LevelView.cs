using System;
using System.Collections.Generic;
using HOG.Interfaces.Gameplay;
using HOG.Interfaces.Models;
using HOG.Models;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HOG.Views
{
    public class LevelView : MonoBehaviour, IStartable
    {
        [SerializeField]
        private Transform _itemsToFindRoot;
        
        [SerializeField]
        private LevelItemView _prefab;

        [Inject]
        private IDataProvider<LevelModel> _dataProvider;
        
        [Inject]
        private IGameplayCurrentLevel _currentLevel;

        public void Start()
        {
            if (_currentLevel == null)
            {
                return;
            }

            _currentLevel.onLevelStarted += OnLevelStarted;
            _currentLevel.onItemPickedUp += OnItemPickedUp;
            OnLevelStarted(_currentLevel.levelID);
        }

        private void OnItemPickedUp(string itemID)
        {
            if (_activeLevelItems.TryGetValue(itemID, out LevelItemView itemView))
            {
                itemView.gameObject.SetActive(false);
            }
        }

        private void OnLevelStarted(string levelId)
        {
            if (string.IsNullOrEmpty(levelId))
            {
                Debug.LogError($"Level with ID {levelId} not found.");
                return;
            }
            
            if (!_dataProvider.modelsByID.TryGetValue(levelId, out LevelModel levelModel))
            {
                Debug.LogError($"Level with ID {levelId} not found in data provider.");
                return;
            }

            ILevelSettings settings = levelModel.settings;

            foreach (LevelItemView levelItemView in _allLevelItems)
            {
                levelItemView.gameObject.SetActive(false);
            }
            _activeLevelItems.Clear();

            int i = 0;
            foreach (SpriteRenderer sprite in levelModel.prefab.GetComponentsInChildren<SpriteRenderer>())
            {
                if (sprite.name.Length > 2 && sprite.name.Contains("_1"))
                {
                    string key = sprite.name.Remove(sprite.name.Length - 2);
                    LevelItemView view;
                    if (i >= _allLevelItems.Count)
                    {
                        view = Instantiate(_prefab, _itemsToFindRoot).GetComponent<LevelItemView>();
                        _allLevelItems.Add(view);
                    }
                    else
                    {
                        view = _allLevelItems[i];
                    }
                    view.SetSprite(sprite.sprite);
                    view.gameObject.SetActive(true);
                    _activeLevelItems[key] = view;
                    i++;
                }
            }
        }
        
        private List<LevelItemView> _allLevelItems = new();
        private Dictionary<string, LevelItemView> _activeLevelItems = new();
    }
}
