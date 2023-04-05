using System;
using System.Linq;
using System.Collections.Generic;

using Data.IO;

using Wallet;

using Shop.Model;
using Shop.View;

namespace Shop.Controller
{
    public class ShopController : IDisposable
    {
        private ShopModel _shopModel;

        private ShopDataIO _shopDataIO;

        private List<ShopItemView> _shopItemViews = new List<ShopItemView>();

        public ShopController(ShopModel shopModel, ShopDataIO shopDataIO, List<ShopItemView> shopItemViews)
        {
            _shopModel = shopModel;

            _shopDataIO = shopDataIO;

            _shopItemViews = shopItemViews;
        }

        void IDisposable.Dispose()
        {
            for (int i = 0; i < _shopItemViews.Count; i++)
            {
                ShopItemView view = _shopItemViews[i];

                view.CoinsBuyButton.BuyItem -= Buy;

                view.GemsBuyButton.BuyItem -= Buy;

                view.Activate -= Activate;

                _shopModel.ItemBought -= view.UpdateView;

                _shopModel.ItemActivated -= view.UpdateView;
            }

            _shopModel.ItemActivated -= SetTimer;
        }

        public void Initialize()
        {
            for (int i = 0; i < _shopItemViews.Count; i++)
            {
                ShopItemView view = _shopItemViews[i];

                view.CoinsBuyButton.BuyItem += Buy;

                view.GemsBuyButton.BuyItem += Buy;

                view.Activate += Activate;

                _shopModel.ItemBought += view.UpdateView;

                _shopModel.ItemActivated += view.UpdateView;

                Item item = _shopDataIO.ShopData.Items[i];

                view.UpdateView(item);

                if (item.Activated)
                {
                    SetTimer(item);
                }
            }

            _shopModel.ItemActivated += SetTimer;
        }

        private void Buy(int id, WalletBase wallet)
        {
            _shopModel.BuyItem(wallet, id);

            _shopDataIO.SaveData(_shopModel.Data);
        }

        private void Activate(int id, ShopItemView itemView)
        {
            _shopModel.ActivateItem(id);

            _shopDataIO.SaveData(_shopModel.Data);
        }

        private void SetTimer(Item item)
        {
            _shopItemViews.First(x => x.ID == item.ID).Timer.StartTimer(item);
        }
    }
}