using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DVAH;
using System.Linq;
using SimpleJSON;

public class DataManager : Singleton<DataManager>
{
    [Header("----------------Items------------")]
    [SerializeField] ItemInventoryBase _prefabItemInventory;

    [SerializeField] List<ItemDataSO> _generalDataItems = new List<ItemDataSO>();
    public List<ItemDataSO> genaralDataItems => _generalDataItems;
   
    void Init()
    {
        string data= PlayerPrefs.GetString("inventoryDatas");
       

        var dataParsed = JSON.Parse(data);

      
        for (int i = 0; i < dataParsed.Count; i++)
        {
            int itemDataID = dataParsed[i]["ID"].AsInt;
            Debug.LogError(itemDataID +"--"+ dataParsed[i]["quantity"].AsInt);
            ItemDataSO itemData = this.getDataItemByID(itemDataID);
            if (itemData == null)
            {
                continue;
            }


            InventoryManager.Instant.setItemOnInventory(
                Instantiate(_prefabItemInventory)
                .UpdateInfo(itemData)
                .UpdateQuantity(dataParsed[i]["quantity"].AsInt));
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public ItemDataSO getDataItemByID(int ID)
    {
        foreach (ItemDataSO item in _generalDataItems)
        {
            if (ID == item._ID)
                return item;
        }

        Debug.LogError($"ID {ID} not exist!");
        return null;
    }

    private void OnDrawGizmosSelected()
    {
        _generalDataItems = Resources.LoadAll<ItemDataSO>("Items").ToList();
    }
}
