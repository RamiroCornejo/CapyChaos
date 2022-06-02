using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    public int cost;
    public Sprite shopSprite;
    public string itemName;

    public abstract void RefreshItem(CustomAssingment assign);
}
