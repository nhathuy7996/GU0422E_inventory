using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DVAH;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] List<ItemInventoryBase> _items = new List<ItemInventoryBase>();
    public List<ItemInventoryBase> items => _items;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
