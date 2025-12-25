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
    }

    public class GameplayLevelModel : IModel
    {
        public string id { get; }
        public GameObject levelInstance;
        public Dictionary<string, Sprite> uiSprites;
        public Dictionary<string, GameObject> uiBackgroundSprites;
    }
}