using System;
using HOG.Interfaces.Gameplay;
using UnityEngine;
using VContainer.Unity;

namespace HOG.Gameplay
{
    public class Timer : ITimer, ITickable
    {
        public event Action<int> onStarted;
        public event Action<int> onUpdated;
        public event Action onExpired;
        public event Action onCancelled;

        public void Set(int seconds)
        {
            _secondsLeft = seconds;
        }

        public void Start()
        {
            _active = true;
            onStarted?.Invoke(_secondsLeft);
        }

        public void Cancel()
        {
            onCancelled?.Invoke();
            _active = false;
        }
        
        public void Tick()
        {
            if (!_active)
                return;
            _timePassed += Time.deltaTime;
            if (_timePassed > 1f)
            {
                while (_timePassed >= 1f)
                {
                    _secondsLeft -= 1;
                    _timePassed -= 1f;
                }
                onUpdated?.Invoke(_secondsLeft);
                if (_secondsLeft <= 0)
                {
                    onExpired?.Invoke();
                    _active = false;
                }
            }
        }
        
        private int _secondsLeft;
        private float _timePassed;
        private bool _active;
    }
}
