using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopPage", menuName = "Shop/Page")]
public class ShopPageData : ScriptableObject
{
    public string pageName = "";
    public ShopItem[] items = new ShopItem[0];
}
