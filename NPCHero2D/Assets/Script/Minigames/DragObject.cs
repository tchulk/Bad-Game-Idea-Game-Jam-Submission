using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class DragObject : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private DragManager manager = null;

    private Vector2 centerPoint;
    private Vector2 WorldCenterPoint => transform.TransformPoint(centerPoint);

    private void Awake()
    {
        manager = GetComponentInParent<DragManager>();
        centerPoint = (transform as RectTransform).rect.center;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        manager.RegisterDraggedObject(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (manager.IsWithinBounds(WorldCenterPoint + eventData.delta))
        {
            transform.Translate(eventData.delta);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        manager.UnregisterDraggedObject(this);
    }
}
