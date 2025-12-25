using System.Collections.Generic;

namespace HOG.Interfaces.Models
{
    public interface ILevelSettings
    {
        IReadOnlyList<string> disabledItems { get; }
        bool imageMode { get; }
        int timerDurationSeconds { get; }
        IReadOnlyList<string> firstItems { get; }
    }
}