using HOG.Interfaces.Models;
using UnityEngine;

namespace HOG.Models
{
    public class LevelModel
    {
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

    public class LevelSettings : ILevelSettings
    {
        public LevelSettings(ILevelSettings copyFrom)
        {
            
        }
    }
}