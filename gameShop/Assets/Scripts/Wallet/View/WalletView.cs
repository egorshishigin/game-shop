using TMPro;

using UnityEngine;

namespace Wallet.View
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _money;

        public void UpdateMoneyValue(int value)
        {
            _money.text = value.ToString();
        }
    }
}