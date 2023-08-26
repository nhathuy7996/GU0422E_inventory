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
        if (string.IsNullOrEmpty(data))
            return;

        var dataParsed = JSON.Parse(Security.Decrypt(data, "Huynn"));

      
        for (int i = 0; i < dataParsed.Count; i++)
        {
            int itemDataID = dataParsed[i]["ID"].AsInt;
            Debug.LogError(itemDataID +"--"+ dataParsed[i]["quantity"].AsInt);

            this.createItemOnInventory(itemDataID, dataParsed[i]["quantity"].AsInt);
        }
    }

    public void createItemOnInventory(int ID, int quantiy)
    {
        ItemDataSO itemData = this.getDataItemByID(ID);
        if (itemData == null)
        {
            return;
        }


        InventoryManager.Instant.setItemOnInventory(
            Instantiate(_prefabItemInventory)
            .UpdateInfo(itemData)
            .UpdateQuantity(quantiy));
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

    public void saveDataInventory()
    {
        PlayerPrefs.SetString("inventoryDatas", InventoryManager.Instant.ItemsDataToJSON());
    }

    private void OnDrawGizmosSelected()
    {
        _generalDataItems = Resources.LoadAll<ItemDataSO>("Items").ToList();
    }
}
