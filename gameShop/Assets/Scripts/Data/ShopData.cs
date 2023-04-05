using System;
using System.Collections.Generic;

using UnityEngine;

namespace Data
{
    [Serializable]
    public class ShopData
    {
        [SerializeField] private int _coins;

        [SerializeField] private int _gems;

        [SerializeField] private List<Item> items = new List<Item>();

        public int Coins { get => _coins; set => _coins = value; }

        public int Gems { get => _gems; set => _gems = value; }

        public List<Item> Items { get => items; set => items = value; }
    }
}