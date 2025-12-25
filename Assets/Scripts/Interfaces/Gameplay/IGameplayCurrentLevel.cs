using System;

namespace HOG.Interfaces.Gameplay
{
    public enum LevelFinishResultType
    {
        Completed,
        Failed
    }
    
    public interface IGameplayCurrentLevel
    {
        event Action<LevelFinishResultType> OnLevelFinished;
        void StartLevel(string levelId);
    }
}