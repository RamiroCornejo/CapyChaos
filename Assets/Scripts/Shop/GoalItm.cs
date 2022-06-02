using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoalItem", menuName = "Shop/Goal")]
public class GoalItm : ShopItem
{
    public GameObject goal;

    public override void RefreshItem(CustomAssingment assign)
    {
    }
}
