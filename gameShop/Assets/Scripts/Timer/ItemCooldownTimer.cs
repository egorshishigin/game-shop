using System;
using System.Collections;

using Shop.View;

using UnityEngine;

namespace Timer
{
    public class ItemCooldownTimer : MonoBehaviour
    {
        [SerializeField] private ShopItemView _shopItemView;

        private Item _item;

        public event Action<float> TimerTick = delegate { };

        public event Action TimerStopped = delegate { };

        public void StartTimer(Item item)
        {
            _item = item;

            StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            float time = _item.CooldownTime;

            while (time > 0)
            {
                time -= Time.deltaTime;

                TimerTick.Invoke(time);

                _item.SetCooldown(time);

                yield return null;
            }

            _item.Deactivate();

            _item.SetCooldown(_item.ActiveTime);

            _shopItemView.UpdateView(_item);

            TimerStopped.Invoke();
        }
    }
}