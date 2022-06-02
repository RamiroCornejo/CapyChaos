using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShaderOutline : MonoBehaviour
{
    public Transform parent;
    Renderer[] myRenders;
    public string name_value = "_OutlineColor";
    [SerializeField] Color SelectColor = Color.blue;
    [SerializeField] Color EnterColor = Color.cyan;
    [SerializeField] Color ExitColor = Color.black;
    [SerializeField] Color auxiliar = Color.white;

    private void Start()
    {
        myRenders = parent.GetComponentsInChildren<Renderer>();
        UE_Exit();
    }

    public void UE_Select() => SetColor(SelectColor);
    public void UE_Enter() => SetColor(EnterColor);
    public void UE_Exit() => SetColor(ExitColor);
    public void UE_Auxiliar() => SetColor(auxiliar);

    void SetColor(Color color)
    {
        foreach(var m in myRenders) m.material.SetColor(name_value, color);
    }
}
