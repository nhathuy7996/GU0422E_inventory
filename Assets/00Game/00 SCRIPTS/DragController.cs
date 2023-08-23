using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DVAH;

public class DragController : Singleton<DragController>
{
    [SerializeField] GameObject _movingItem;
    [SerializeField]
    Transform _parent;

    Transform _targetSlot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_movingItem == null)
            return;
        Vector2 mousePos = Input.mousePosition;
        _movingItem.transform.position = mousePos;

        _targetSlot = null;
        foreach (Transform t in InventoryManager.Instant.itemSlot)
        {
            if(Vector2.Distance(_movingItem.transform.position,t.position)<= 50f)
            {
                _targetSlot = t;
            }
        }
    }

    public void setMovingItem(GameObject g)
    {
        Debug.Log("set moving item");
        _movingItem = g;
        _parent = g.transform.parent;

        g.transform.SetParent(this.transform);
    }

    public void removeMovingItem()
    {
        if(_movingItem != null)
        {
            if (_targetSlot == null || _targetSlot.childCount != 0)
            {
                _movingItem.transform.SetParent(_parent);
                _movingItem.transform.localPosition = Vector3.zero;
            }
            else
            {
                _movingItem.transform.SetParent(_targetSlot);
                _movingItem.transform.localPosition = Vector3.zero;
            }
        }
        _movingItem = null;
        _targetSlot = null;

    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
