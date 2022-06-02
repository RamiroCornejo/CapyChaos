using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerItem", menuName = "Shop/Spawner")]
public class SpawnerItm : ShopItem
{
    public GameObject spawner;

    public override void RefreshItem(CustomAssingment assign)
    {
    }
}
