using System;

using Wallet;

using UnityEngine;
using UnityEngine.UI;

namespace Shop.View
{
    public class BuyButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private int _id;

        private WalletBase _wallet;

        public event Action<int, WalletBase> BuyItem = delegate { };

        private void OnDisable()
        {
            _button?.onClick.RemoveListener(OnBuyClick);
        }

        public void Initialize(int id, WalletBase wallet)
        {
            _id = id;

            _wallet = wallet;

            _button.onClick.AddListener(OnBuyClick);
        }

        private void OnBuyClick()
        {
            BuyItem.Invoke(_id, _wallet);
        }
    }
}