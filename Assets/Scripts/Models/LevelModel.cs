using System;
using System.Collections.Generic;
using HOG.Interfaces.Models;
using UnityEngine;

namespace HOG.Models
{
    public class LevelModel: IModel
    {
        public string id => name;
        public string name { get; }
        public GameObject prefab { get; }
        public ILevelSettings settings { get; }
        public  LevelModel(string name, GameObject prefab, ILevelSettings settings)
        {
            this.name = name;
            this.prefab = prefab;
            this.settings = settings;
        }
    }

    [Serializable]
    public class LevelSettings : ILevelSettings
    {
        public IReadOnlyList<string> disabledItems => _disabledItems;
        [SerializeField]
        private List<string> _disabledItems;
        [field: SerializeField]
        public bool imageMode { get; private set; }
        [field: SerializeField]
        public int timerDurationSeconds { get; private set; }
        public IReadOnlyList<string> firstItems => _firstItems;
        [SerializeField]
        private List<string> _firstItems;
    }

    public class LevelFilteredDataModel : IModel
    {
        public string id { get; }
        public IReadOnlyList<string> sortedGameplayItemIDs { get; }
        public IReadOnlyList<string> sortedViewItemIDs { get; }

        public LevelFilteredDataModel(LevelModel levelModel)
        {
            List<string> gameplayItemIDs = new List<string>();
            List<string> viewItemIDs = new List<string>();
            List<string> unprocessedGameplayIDs = new List<string>();

            // Select items that are not ignored
            foreach (SpriteRenderer child in levelModel.prefab.GetComponentsInChildren<SpriteRenderer>())
            {
                if (child.name == "Background")
                {
                    continue;
                }
                
                if (child.name.EndsWith("_1"))
                {
                    continue;
                }

                bool disabled = false;
                foreach (string disabledItem in levelModel.settings.disabledItems)
                {
                    if (child.name == disabledItem)
                    {
                        disabled = true;
                    }
                }
                if (disabled)
                    continue;

                unprocessedGameplayIDs.Add(child.name);
            }

            foreach (string firstItem in levelModel.settings.firstItems)
            {
                int index = unprocessedGameplayIDs.FindIndex(x => x == firstItem);
                if (index != -1)
                {
                    gameplayItemIDs.Add(firstItem);
                    viewItemIDs.Add(firstItem + "_1");
                    unprocessedGameplayIDs.RemoveAt(index);
                }
            }

            foreach (string itemID in unprocessedGameplayIDs)
            {
                gameplayItemIDs.Add(itemID);
                viewItemIDs.Add(itemID + "_1");
            }

            sortedGameplayItemIDs = gameplayItemIDs;
            sortedViewItemIDs = viewItemIDs;
            id = levelModel.id;
        }
    }
}