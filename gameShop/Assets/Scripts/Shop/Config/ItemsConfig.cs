using System.Collections.Generic;

using UnityEngine;

namespace Shop.Config
{
    [CreateAssetMenu(fileName = "ItemsConfig", menuName = "Configs/ItemsConfig")]
    public class ItemsConfig : ScriptableObject
    {
        [SerializeField] private List<Item> _items = new List<Item>();

        public List<Item> Items => _items;
    }
}