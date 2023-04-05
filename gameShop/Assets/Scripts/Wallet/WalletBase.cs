using System;

using Data;

namespace Wallet
{
    public abstract class WalletBase : IWallet
    {
        private int _money;

        private ShopData _shopData;

        public WalletBase(int money, ShopData shopData)
        {
            Money = money;

            ShopData = shopData;
        }

        public int Money { get => _money; set => _money = value; }

        public ShopData ShopData { get => _shopData; set => _shopData = value; }

        public event Action<int> MoneyChanged = delegate { };

        public void SpendMoney(int amount)
        {
            OnMoneySpent(amount);

            MoneyChanged.Invoke(Money);
        }

        protected abstract void OnMoneySpent(int amount);
    }
}