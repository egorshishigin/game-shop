using Data;

using UnityEngine;

namespace Wallet
{
    public class CoinsWallet : WalletBase
    {
        public CoinsWallet(int money, ShopData shopData) : base(money, shopData)
        {
        }

        protected override void OnMoneySpent(int amont)
        {
            Money -= amont;

            ShopData.Coins = Money;

            Debug.Log($"{amont} coins spent");
        }
    }
}