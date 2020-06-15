using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Test : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Dictionary<int, GameObject> _draggingIcons = new Dictionary<int, GameObject>();
    private Dictionary<int, RectTransform> _draggingPlanes = new Dictionary<int, RectTransform>();

    public void OnBeginDrag(PointerEventData eventData)
    {
        Canvas _canvas = Helper.FindInParents<Canvas>(gameObject);

        _draggingIcons[eventData.pointerId] = new GameObject("draggingIcons");
        _draggingIcons[eventData.pointerId].transform.SetParent(_canvas.transform, false);
        _draggingIcons[eventData.pointerId].transform.SetAsLastSibling();

        var image = _draggingIcons[eventData.pointerId].AddComponent<Image>();
        image.sprite = GetComponent<Image>().sprite;

        _draggingPlanes[eventData.pointerId] = transform as RectTransform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var rt = _draggingIcons[eventData.pointerId].GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_draggingPlanes[eventData.pointerId], eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = _draggingPlanes[eventData.pointerId].rotation;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_draggingIcons[eventData.pointerId] != null)
            Destroy(_draggingIcons[eventData.pointerId]);

        _draggingIcons[eventData.pointerId] = null;
    }
}
