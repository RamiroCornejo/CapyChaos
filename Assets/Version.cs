using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class Version : MonoBehaviour
{
    TextMeshProUGUI mytext;
    private void Start()
    {
        mytext = GetComponent<TextMeshProUGUI>();
        mytext.text = " V. " + Application.version;
        ;
    }
    
}
