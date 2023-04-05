using System;

using TMPro;

using UnityEngine;

namespace Timer.View
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private ItemCooldownTimer _cooldownTimer;

        [SerializeField] private TMP_Text _timeText;

        private void OnEnable()
        {
            _cooldownTimer.TimerTick += UpdateTimeValue;

            _cooldownTimer.TimerStopped += ResetTimerView;
        }

        private void OnDisable()
        {
            _cooldownTimer.TimerTick -= UpdateTimeValue;

            _cooldownTimer.TimerStopped -= ResetTimerView;
        }

        private void UpdateTimeValue(float value)
        {
            TimeSpan time = TimeSpan.FromSeconds(value);

            _timeText.text = time.ToString(@"hh\:mm\:ss");
        }

        private void ResetTimerView()
        {
            _timeText.text = string.Empty;
        }
    }
}