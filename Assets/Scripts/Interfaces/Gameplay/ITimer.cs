using System;

namespace HOG.Interfaces.Gameplay
{
    public interface ITimer
    {
        event Action<int> onStarted;
        event Action<int> onUpdated;
        event Action onExpired;
        event Action onCancelled;

        void Set(int seconds);
        void Start();
        void Cancel();
    }
}