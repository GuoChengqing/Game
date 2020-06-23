using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class DragCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
{
    public Vector2 range = new Vector2(5f, 3f);
    private RectTransform rectTransform;
    private Vector2 mRot = Vector2.zero;
    private Quaternion startQuaternion;
    private Vector2 startPosition;
    private Quaternion beforeDragQuaternion;
    private Vector2 beforeDragPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startQuaternion = rectTransform.localRotation;
        startPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        Vector3 pos = Input.mousePosition;

        float halfWidth = Screen.width * 0.5f;
        float halfHeight = Screen.height * 0.5f;
        float x = Mathf.Clamp((pos.x - halfWidth) / halfWidth, -1f, 1f);
        float y = Mathf.Clamp((pos.y - halfHeight) / halfHeight, -1f, 1f);
        mRot = Vector2.Lerp(mRot, new Vector2(x, y), Time.deltaTime * 5f);

        rectTransform.localRotation = startQuaternion * Quaternion.Euler(-mRot.y * range.y, mRot.x * range.x, 0f);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (Input.mousePosition.y > Screen.height * 0.3)
            Debug.Log("nice");

        rectTransform.localRotation = beforeDragQuaternion;
        rectTransform.anchoredPosition = beforeDragPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        beforeDragQuaternion = rectTransform.localRotation;
        beforeDragPosition = rectTransform.anchoredPosition;
    }
}
