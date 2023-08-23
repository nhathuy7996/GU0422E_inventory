using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DVAH;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEditor.Events;

public class InventoryManager : DVAH.Singleton<InventoryManager>
{
    [SerializeField] Transform _gridLayput;

    [SerializeField] List<ItemInventoryBase> _items = new List<ItemInventoryBase>();
    public List<ItemInventoryBase> items => _items;

    [SerializeField]
    List<Transform> _itemSlots = new List<Transform>();
    public List<Transform> itemSlot => _itemSlots;
    // Start is called before the first frame update
    void Start()
    {
        _itemSlots = _gridLayput.GetComponentsInChildren<Transform>().ToList();
        _itemSlots.RemoveAt(0);
        Init();
    }

    void Init()
    {
        int i = 0;
        foreach (ItemInventoryBase item in _items)
        {
            EventTrigger g = Instantiate(item.gameObject, _itemSlots[i]).GetComponent<EventTrigger>();
            var pDown = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };

            pDown.callback.AddListener(eventData => {
                DragController.Instant.setMovingItem(g.gameObject);
            });
            g.triggers.Add(pDown);

            var pUp = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerUp
            };

            pUp.callback.AddListener(eventData => {
                DragController.Instant.removeMovingItem();
            });
            g.triggers.Add(pUp);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
