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


    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
