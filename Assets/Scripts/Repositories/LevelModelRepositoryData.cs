using HOG.Interfaces.Models;
using UnityEngine;

namespace HOG.Repositories
{
    [CreateAssetMenu(fileName = "LevelModelRepositoryData", menuName = "Repositories/Level Model")]
    public class LevelModelRepositoryData : ScriptableObject
    {
        public GameObject levelAsset;
        public ILevelSettings levelSettings;
    }
}