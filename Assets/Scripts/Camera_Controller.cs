using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Camera_Controller : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float zoomSpeed = 50f;

    public float limitedMinY = 5f;
    public float limitedMaxY = 300f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMove(Input.mousePosition);

        CameraZomm(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void CameraZomm(float zoom)
    {
        if (!IsErreichenLitimedHeight(zoom))
        {
            transform.position += Vector3.down * zoom * zoomSpeed * Time.deltaTime;
        }
    }

    private bool IsErreichenLitimedHeight(float zoom)
    {
        if (zoom > 0)
        {
            return IsErreichenLititedMinY();
        }
        else
            return IsErreichenLititedMaxY();
    }

    private bool IsErreichenLititedMinY()
    {
        return transform.position.y < limitedMinY;
    }

    private bool IsErreichenLititedMaxY()
    {
        return transform.position.y > limitedMaxY;
    }

    private void CameraMove(Vector3 mousePosition)
    {
        if (MouseAtUpEdge(mousePosition))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        } else if (MouseAtDownEdge(mousePosition))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        } else if (MouseAtRightEdge(mousePosition))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        } else if (MouseAtLeftEdge(mousePosition))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }

    private bool MouseAtLeftEdge(Vector3 mousePosition)
    {
        return mousePosition.x == 0;
    }

    private bool MouseAtRightEdge(Vector3 mousePosition)
    {
        return mousePosition.x == Screen.width;
    }

    private bool MouseAtDownEdge(Vector3 mousePosition)
    {
        return mousePosition.y == 0;
    }

    private bool MouseAtUpEdge(Vector3 mousePosition)
    {
        return mousePosition.y == Screen.height;
    }
}
