using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DVAH;
using System.Linq;
using SimpleJSON;

public class DataManager : Singleton<DataManager>
{
    [Header("----------------Items------------")]
    [SerializeField] List<ItemDataSO> _genaralDataItems = new List<ItemDataSO>();
    public List<ItemDataSO> genaralDataItems => _genaralDataItems;

    void Init()
    {
        string data= PlayerPrefs.GetString("inventoryDatas");
       

        var dataParsed = JSON.Parse(data);

        for (int i = 0; i < dataParsed.Count; i++)
        {
            Debug.LogError(dataParsed[i]["ID"].AsInt +"--"+ dataParsed[i]["quantity"].AsInt);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        _genaralDataItems = Resources.LoadAll<ItemDataSO>("Items").ToList();
    }
}
