using HOG.Interfaces.Gameplay;
using HOG.Interfaces.Models;
using HOG.Models;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HOG.Gameplay
{
    public class GameplayRoot : MonoBehaviour, IStartable
    {
        [Inject]
        private IGameplayCurrentLevel _level;
        [Inject]
        private IDataProvider<LevelModel> _dataProvider;

        public void StartGame()
        {
            _level.StartLevel(_dataProvider.models[0].id);
        }

        public void Start()
        {
            if (_level == null)
            {
                Debug.LogError("IGameplayCurrentLevel _level is null");
                return;
            }

            if (_dataProvider == null)
            {
                Debug.LogError("IDataProvider<LevelModel> _dataProvider is null");
                return;
            }

            if (_dataProvider.models == null || _dataProvider.models.Length == 0)
            {
                Debug.LogError("No levels found in _dataProvider");
                return;
            }

            StartGame();
        }
    }
}
