using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChange : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Texture2D cursotImage;
    [SerializeField] Texture2D cursotImageClick;
    bool cursorSwitch = false;
    void Start()
    {
        Cursor.SetCursor(cursotImage, Vector2.zero, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !cursorSwitch)
        {
            Cursor.SetCursor(cursotImageClick, Vector2.zero, CursorMode.ForceSoftware);
            cursorSwitch = true;
        }
        else if (!Input.GetMouseButton(0) && cursorSwitch)
        {
            Cursor.SetCursor(cursotImage, Vector2.zero, CursorMode.ForceSoftware);
            cursorSwitch = false;
        }
    }
}
