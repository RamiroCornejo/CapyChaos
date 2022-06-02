using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Destructible : MonoBehaviour
{
    [System.NonSerialized] public Arrow myArrow;
    private void Start() => myArrow = GetComponentInParent<Arrow>();
    public void DestroyArrow() => myArrow.Destroy();
}
