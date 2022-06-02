using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHoverFeedback : MonoBehaviour
{
    public GameObject[] obj;
    List<SimpleShaderHightLights> shaders = new List<SimpleShaderHightLights>();

    private void Start()
    {
        for (int i = 0; i < obj.Length; i++)
        {
            var shader = obj[i].GetComponentInChildren<SimpleShaderHightLights>();
            if (shader)
            {
                shaders.Add(shader);
            }
        }
        
    }

    private void OnMouseEnter()
    {
        for (int i = 0; i < shaders.Count; i++)
        {
            shaders[i].UE_Enter(true);
        }
    }
    private void OnMouseExit()
    {
        for (int i = 0; i < shaders.Count; i++)
        {
            shaders[i].UE_Exit();
        }
    }
}
