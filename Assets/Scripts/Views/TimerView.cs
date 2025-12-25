using UnityEngine;
using VContainer;
using VContainer.Unity;
using HOG.Interfaces.Gameplay;
using TMPro;

namespace HOG.Views
{
    public class TimerView : MonoBehaviour, IInitializable
    {
        [Inject]
        private ITimer _timer;

        public void Initialize()
        {
            _timerText = GetComponentInChildren<TMP_Text>();
            _timer.onUpdated += OnTimerUpdated;
            _timer.onStarted += OnTimerStarted;
            _timer.onCancelled += OnTimerCancelled;
            _timer.onExpired += OnTimerExpired;
        }

        private void OnTimerExpired()
        {
            gameObject.SetActive(false);
        }

        private void OnTimerCancelled()
        {
            gameObject.SetActive(false);
        }

        private void OnTimerStarted(int seconds)
        {
            gameObject.SetActive(true);
            UpdateTimerText(seconds);
        }

        private void OnTimerUpdated(int seconds)
        {
            UpdateTimerText(seconds);
        }
        
        private void UpdateTimerText(int seconds)
        {
            int mm = seconds / 60;
            int ss = seconds % 60;
            _timerText.text = $"{mm:00}:{ss:00}";
        }
        
        private TMP_Text _timerText;
    }
}
