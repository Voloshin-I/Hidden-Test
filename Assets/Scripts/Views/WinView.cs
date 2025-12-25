using HOG.Interfaces.Gameplay;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HOG.Views
{
    public class WinView : MonoBehaviour, IInitializable
    {
        [Inject]
        private IGameplayCurrentLevel _currentLevel;

        public void Initialize()
        {
            _currentLevel.onLevelFinished += OnLevelFinished;
        }

        private void OnLevelFinished(LevelFinishResultType resultType)
        {
            if (resultType == LevelFinishResultType.Completed)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
