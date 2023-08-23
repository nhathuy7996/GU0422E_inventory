using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DVAH;
using System.Linq;

public class DataManager : Singleton<DataManager>
{
    [Header("----------------Items------------")]
    [SerializeField] List<ItemDataSO> _genaralDataItems = new List<ItemDataSO>();
    public List<ItemDataSO> genaralDataItems => _genaralDataItems;


    // Start is called before the first frame update
    void Start()
    {
        
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
