using HOG.Interfaces.Models;
using HOG.Models;
using UnityEngine;

namespace HOG.Repositories
{
    [CreateAssetMenu(fileName = "LevelModelRepositoryData", menuName = "Repositories/Level Model")]
    public class LevelModelRepositoryData : ScriptableObject
    {
        public GameObject levelAsset;
        public LevelSettings levelSettings;
    }
}