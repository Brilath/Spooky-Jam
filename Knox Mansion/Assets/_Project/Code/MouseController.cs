using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;

    private void Awake()
    {
        UpdateMouse();
    }

    private void UpdateMouse()
    {
        CursorMode cursorMode = CursorMode.Auto;
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
