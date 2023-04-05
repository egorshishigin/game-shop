using System.IO;

using Shop.Config;

using UnityEngine;

namespace Data.IO
{
    public class ShopDataIO
    {
        private const string FileName = "/ShopData";

        private const int StartCoins = 100;

        private const int StartGems = 250;

        private ShopData _shopData;

        private ItemsConfig _itemsConfig;

        public ShopDataIO(ItemsConfig itemsConfig)
        {
            _itemsConfig = itemsConfig;
        }

        public ShopData ShopData => _shopData;

        public void SaveData(ShopData shopData)
        {
            _shopData = shopData;

            var json = JsonUtility.ToJson(_shopData);

            File.WriteAllText(Application.persistentDataPath + FileName, json);
        }

        public void LoadData()
        {
            if (File.Exists(Application.persistentDataPath + FileName))
            {
                var json = File.ReadAllText(Application.persistentDataPath + FileName);

                _shopData = JsonUtility.FromJson<ShopData>(json);
            }
            else
            {
                _shopData = new ShopData();

                foreach (var item in _itemsConfig.Items)
                {
                    _shopData.Items.Add(item);
                }

                _shopData.Coins = StartCoins;

                _shopData.Gems = StartGems;

                SaveData(_shopData);
            }
        }
    }
}