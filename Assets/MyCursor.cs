using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCursor : MonoBehaviour
{

    public static MyCursor instance;
    private void Awake() => instance = this;

    public Texture2D cursor_normal;
    public Texture2D cursor_over;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;


    public static void Normal()
    {
        Vector3 spot = new Vector2(instance.cursor_normal.width / 2, instance.cursor_normal.height / 2);
        Cursor.SetCursor(instance.cursor_normal, spot, instance.cursorMode);
    }
    public static void HOver()
    {
        Vector3 spot = new Vector2(instance.cursor_normal.width / 2, instance.cursor_normal.height / 2);
        Cursor.SetCursor(instance.cursor_over, spot, instance.cursorMode);
    }

}
