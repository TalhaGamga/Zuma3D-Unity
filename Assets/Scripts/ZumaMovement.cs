using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZumaMovement : MonoBehaviour
{
    Vector2 lookInput, screenCenter, mousePos;

    void Start()
    {
        screenCenter.x = Screen.width / 2;
        screenCenter.y = Screen.height / 2;

        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        FollowCursor();
    }

    void FollowCursor()
    {
        transform.forward = Vector3.Lerp(transform.forward, CursorPos(), 0.1f);
    }

    Vector3 CursorPos()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mousePos.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mousePos.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        Vector3 lookPosition = new Vector3(mousePos.x, 0, mousePos.y);

        return lookPosition;
    }
}
