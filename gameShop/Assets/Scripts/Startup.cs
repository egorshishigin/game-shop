using System.Collections.Generic;

using Data.IO;

using Wallet;
using Wallet.View;

using Shop.Config;
using Shop.Model;
using Shop.View;
using Shop.Controller;

using UnityEngine;
using UnityEngine.UI;
using System;

public class Startup : MonoBehaviour
{
    [Header("Shop item view")]
    [SerializeField] private ShopItemView _shopItemViewPrefab;

    [Header("Wallets view")]
    [SerializeField] private WalletView _coinsWalletView;

    [SerializeField] private WalletView _gemsWalletView;

    [Header("Shop UI")]
    [SerializeField] private RectTransform _shopHolder;

    [SerializeField] private GridLayoutGroup _gridLayoutGroup;

    [Header("Config")]
    [SerializeField] private ItemsConfig _itemsConfig;

    private List<ShopItemView> _shopItemViews = new List<ShopItemView>();

    private ShopModel _shopModel;

    private WalletBase _coinsWallet;

    private WalletBase _gemsWallet;

    private ShopController _shopController;

    private ShopDataIO _dataIO;

    private void Start()
    {
        LoadData();

        CreateWallets();

        CreateModel();

        CreateView();

        CreateController();
    }

    private void OnDisable()
    {
        _coinsWallet.MoneyChanged -= _coinsWalletView.UpdateMoneyValue;

        _gemsWallet.MoneyChanged -= _gemsWalletView.UpdateMoneyValue;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            _dataIO.SaveData(_shopModel.Data);
        }
        else return;
    }

    private void LoadData()
    {
        _dataIO = new ShopDataIO(_itemsConfig);

        _dataIO.LoadData();
    }

    private void CreateView()
    {
        SetViewGridSize();

        foreach (var item in _itemsConfig.Items)
        {
            ShopItemView view = Instantiate(_shopItemViewPrefab);

            view.transform.position = _shopHolder.transform.position;

            view.transform.SetParent(_shopHolder, false);

            view.Initialize(item);

            view.CoinsBuyButton.Initialize(item.ID, _coinsWallet);

            view.GemsBuyButton.Initialize(item.ID, _gemsWallet);

            _shopItemViews.Add(view);
        }
    }

    private void SetViewGridSize()
    {
        float width = _gridLayoutGroup.cellSize.x * 2 * (_itemsConfig.Items.Count - 1);

        _shopHolder.sizeDelta = new Vector2(width, _shopHolder.rect.y);
    }

    private void CreateModel()
    {
        _shopModel = new ShopModel(_itemsConfig, _dataIO.ShopData);

        _shopModel.Initialize();
    }

    private void CreateController()
    {
        _shopController = new ShopController(_shopModel, _dataIO, _shopItemViews);

        _shopController.Initialize();
    }

    private void CreateWallets()
    {
        _coinsWallet = new CoinsWallet(_dataIO.ShopData.Coins, _dataIO.ShopData);

        _gemsWallet = new GemsWallet(_dataIO.ShopData.Gems, _dataIO.ShopData);

        _coinsWallet.MoneyChanged += _coinsWalletView.UpdateMoneyValue;

        _gemsWallet.MoneyChanged += _gemsWalletView.UpdateMoneyValue;

        _coinsWalletView.UpdateMoneyValue(_coinsWallet.Money);

        _gemsWalletView.UpdateMoneyValue(_gemsWallet.Money);
    }
}