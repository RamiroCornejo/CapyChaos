using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectItem", menuName = "Shop/Collectable")]
public class CollectItm : ShopItem
{
    public GameObject collectableGO;

    public Sprite spriteOK;
    public Sprite spriteNull;

    public ParticleSystem particle;

    public override void RefreshItem(CustomAssingment assign)
    {
    }
}
