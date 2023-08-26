using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DVAH;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems; 
using System.Security.Cryptography;


public class InventoryManager : DVAH.Singleton<InventoryManager>
{
    string PassParse = "Huynn";
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

    public void setItemOnInventory(ItemInventoryBase item)
    {
        if (_items.IndexOf(item) != -1)
            return;

        foreach (Transform slot in _itemSlots)
        {
            if (slot.childCount > 0)
                continue;

            item.transform.SetParent(slot);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;
            item.GetComponent<RectTransform>().localScale = Vector3.one;


            EventTrigger g = item.GetComponent<EventTrigger>();
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

            _items.Add(item);
            break;
        }
    }

    public void clearSlot(ItemInventoryBase item)
    {
        if (!_items.Contains(item))
            return;


        _items.Remove(item);
        Destroy(item.gameObject);
    }


    public void autoMerge()
    {
        int i = 0;
        foreach (ItemInventoryBase item in _items)
        {
            
            bool keepSearching = true;
            while (_itemSlots[i].childCount > 0)
            {
                i++;
                if (i >= _items.Count)
                {
                    keepSearching = false;
                    break;
                }
            }
            if (!keepSearching)
            {
                i = 0;
                continue;
            }
            item.transform.SetParent(_itemSlots[i]);
            item.transform.localPosition = Vector3.zero;
            i = 0;
        }
    }

    Dictionary<int, int> tmpItems = new Dictionary<int, int>();
    public void autoMerge2()
    {

        tmpItems.Clear();
        while (_items.Count > 0)
        {
            ItemInventoryBase item = _items[0];
            if (!tmpItems.ContainsKey(item.info._ID))
            {
                tmpItems.Add(item.info._ID, item.quantity);
                clearSlot(item);
                continue;
            }

            tmpItems[item.info._ID] += item.quantity;
            clearSlot(item);
        }



        StartCoroutine(waittCanvasUpdate());
        

        
    }

    IEnumerator waittCanvasUpdate()
    {
        yield return new WaitForEndOfFrame();
        foreach (KeyValuePair<int, int> tmpItem in tmpItems)
        {
            int numSlot = tmpItem.Value / 10 + (tmpItem.Value % 10 != 0 ? 1 : 0);
            for (int i = 0; i < numSlot - 1; i++)
            {
                DataManager.Instant.createItemOnInventory(tmpItem.Key, 10);
            }
            if (numSlot >= 1)
            {
                DataManager.Instant.createItemOnInventory(tmpItem.Key, tmpItem.Value % 10);
                continue;
            }

            DataManager.Instant.createItemOnInventory(tmpItem.Key, tmpItem.Value);
        }

       
    }

    
    public string ItemsDataToJSON()
    {
        string data = "[";

        foreach (ItemInventoryBase item in _items)
        {
            if (item == null)
                continue;
            data += item.dataToString() +",";
        }

       // if (data.EndsWith(","))
            data.Remove(data.Length - 1, 1);

        data += "]";

        return Security.Encrypt(data, PassParse);
    }
}
