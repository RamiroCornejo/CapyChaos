using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleShaderHightLights : MonoBehaviour
{
    Transform parent;
    Renderer[] myRenders;
    public string name_value = "_Color0";
    string Emmisive = "_EmissionColor";
    [SerializeField] Color EnterColor = Color.cyan;
    [SerializeField] Color EnterColor_Capibara = Color.yellow;

    Color black = new Color(0, 0, 0, 1);

    Color[] initial_values = new Color[0];

    private void Start()
    {
        parent = this.transform;
        myRenders = parent.GetComponentsInChildren<Renderer>();


        initial_values = new Color[myRenders.Length];

        for (int i = 0; i < myRenders.Length; i++)
        {
            if (myRenders[i].material.HasProperty(name_value))
            {
                initial_values[i] = myRenders[i].material.GetColor(name_value);
            }
        }

        UE_Exit();
    }

    public void UE_Enter(bool isCapibara = false)
    {
        SetColor(isCapibara ? EnterColor_Capibara : EnterColor);
    }

    public void UE_Exit()
    {
        for (int i = 0; i < myRenders.Length; i++)
        {
            if (myRenders[i].material.HasProperty(name_value))
            {
                myRenders[i].material.SetColor(name_value, initial_values[i]);
                myRenders[i].material.SetColor(Emmisive, black);
            }
        }
    }

    void SetColor(Color color)
    {
        color.a = 1;

        foreach (var m in myRenders)
        {
            if (m.material.HasProperty(name_value))
            {
                m.material.SetColor(name_value, color * 2f);
                // m.material.SetColor(Emmisive, color *5);
            }
        }
    }
}
