using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GrayscaleController : MonoBehaviour
{
    public Shader shader;
    Material _m;

    public Color color;

    private void Awake()
    {
        _m = new Material(shader);
    }

    private void Update()
    {
        Shader.SetGlobalColor("FilterColor", color);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _m);
    }
}
