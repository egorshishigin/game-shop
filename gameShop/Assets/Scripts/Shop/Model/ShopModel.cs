using System;
using System.Linq;
using System.Collections.Generic;

using Shop.Config;

using Data;

using Wallet;

namespace Shop.Model
{
    public class ShopModel
    {
        private ItemsConfig _config;

        private List<Item> _items = new List<Item>();

        private ShopData _data;

        public ShopModel(ItemsConfig config, ShopData data)
        {
            _config = config;

            _data = data;
        }

        public List<Item> Items => _items;

        public ShopData Data => _data;

        public event Action<Item> ItemBought = delegate { };

        public event Action<Item> ItemActivated = delegate { };

        public void Initialize()
        {
            for (int i = 0; i < _config.Items.Count; i++)
            {
                Item itemConfigData = _config.Items[i];

                var item = new Item(itemConfigData.ID, itemConfigData.Price, itemConfigData.ActiveTime, itemConfigData.Icon);

                var data = _data.Items[i];

                if (data.Activated)
                {
                    item.Activate();

                    item.SetCooldown(data.CooldownTime);
                }
                else if (data.Bought)
                {
                    item.Bought = data.Bought;
                }

                _items.Add(item);
            }
        }

        public void BuyItem(WalletBase wallet, int id)
        {
            var item = _items.FirstOrDefault(i => i.ID == id);

            if (wallet.Money >= item.Price)
            {
                item.Bought = true;

                wallet.SpendMoney(item.Price);

                ItemBought.Invoke(item);

                _data.Items = _items;
            }
        }

        public void ActivateItem(int id)
        {
            var item = _items.FirstOrDefault(i => i.ID == id);

            item.Activate();

            item.SetCooldown(item.ActiveTime);

            ItemActivated.Invoke(item);

            _data.Items = _items;
        }
    }
}