using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class copyAuxiliar : MonoBehaviour
{

    Camera myCamera;
    private void Start()
    {
        myCamera = this.gameObject.GetComponent<Camera>();
        var myparent = this.transform.parent.GetComponent<Camera>();

        myCamera.orthographicSize = myparent.orthographicSize;
    }
}
