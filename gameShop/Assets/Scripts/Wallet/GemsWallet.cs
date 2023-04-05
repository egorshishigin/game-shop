using Data;

using UnityEngine;

namespace Wallet
{
    public class GemsWallet : WalletBase
    {
        public GemsWallet(int money, ShopData shopData) : base(money, shopData)
        {
        }

        protected override void OnMoneySpent(int amont)
        {
            Money -= amont / 2;

            ShopData.Gems = Money;

            Debug.Log($"{amont / 2} gems spent");
        }
    }
}