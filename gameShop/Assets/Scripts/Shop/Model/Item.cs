using System;

using UnityEngine;

[Serializable]
public class Item
{
    [SerializeField] private int _id;

    [SerializeField] private int _price;

    [SerializeField] private float _activeTime;

    [SerializeField] private Sprite _icon;

    [SerializeField] private bool _active;

    [HideInInspector] [SerializeField] private float _cooldown;

    [SerializeField] private bool _bought;

    public int ID => _id;

    public int Price => _price;

    public float ActiveTime => _activeTime;

    public Sprite Icon => _icon;

    public bool Activated => _active;

    public float CooldownTime => _cooldown;

    public bool Bought { get => _bought; set => _bought = value; }

    public Item(int id, int coinsPrice, float activeTime, Sprite icon)
    {
        _id = id;

        _price = coinsPrice;

        _activeTime = activeTime;

        _icon = icon;
    }

    public void Activate()
    {
        _active = true;
    }

    public void Deactivate()
    {
        _active = false;
    }

    public void SetCooldown(float value)
    {
        _cooldown = value;
    }
}