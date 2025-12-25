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
        event Action<string> onLevelStarted;
        event Action<LevelFinishResultType> onLevelFinished;
        string levelID { get; }
        void StartLevel(string levelId);
    }
}