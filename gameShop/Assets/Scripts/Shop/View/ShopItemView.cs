using System;

using TMPro;

using Timer;

using UnityEngine;
using UnityEngine.UI;

namespace Shop.View
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private Image _image;

        [SerializeField] private Image _lockImage;

        [SerializeField] private BuyButtonView _coinsBuyButton;

        [SerializeField] private BuyButtonView _gemsButButton;

        [SerializeField] private Button _activateButton;

        [SerializeField] private ItemCooldownTimer _timer;

        [SerializeField] private TMP_Text _coinsPrice;

        [SerializeField] private TMP_Text _gemsPrice;

        private int _id;

        public event Action<int, ShopItemView> Activate = delegate { };

        public ItemCooldownTimer Timer => _timer;

        public int ID => _id;

        public BuyButtonView CoinsBuyButton => _coinsBuyButton;

        public BuyButtonView GemsBuyButton => _gemsButButton;

        private void OnDisable()
        {
            _activateButton?.onClick.RemoveListener(OnActivateClick);
        }

        public void Initialize(Item item)
        {
            _image.sprite = item.Icon;

            _id = item.ID;

            _coinsPrice.text = $"{item.Price} coins";

            _gemsPrice.text = $"{item.Price / 2} gems";

            _activateButton.onClick.AddListener(OnActivateClick);
        }

        public void UpdateView(Item item)
        {
            if (item.ID != _id)
                return;

            UpdateButtons(item);
        }

        private void UpdateButtons(Item item)
        {
            if (item.Bought || item.Activated)
            {
                _coinsBuyButton.gameObject.SetActive(false);

                _gemsButButton.gameObject.SetActive(false);

                _activateButton.gameObject.SetActive(false);

                _lockImage.gameObject.SetActive(false);
            }
            else
            {
                _coinsBuyButton.gameObject.SetActive(true);

                _gemsButButton.gameObject.SetActive(true);

                _activateButton.gameObject.SetActive(true);

                _lockImage.gameObject.SetActive(true);
            }
        }

        private void OnActivateClick()
        {
            Activate.Invoke(_id, this);
        }
    }
}