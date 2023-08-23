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

        foreach ()
        {

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
            // so sanh type item == slot
            _movingItem.transform.SetParent(_parent);
            _movingItem.transform.localPosition = Vector3.zero;
        }
        _movingItem = null;


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
