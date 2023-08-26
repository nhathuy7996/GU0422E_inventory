using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public abstract class ItemInventoryBase : MonoBehaviour
{
    [SerializeField]
    protected ItemDataSO _info;
    [SerializeField]
    protected int _quantity;
    public int quantity => _quantity;
    [SerializeField]
    protected int _maxCapacity;
    public int maxCapacity => _maxCapacity;

    public virtual ItemInventoryBase UpdateInfo(ItemDataSO itemInfo)
    {
        //valid
        _info = itemInfo;
        return this;
    }

    public virtual ItemInventoryBase UpdateQuantity(int newQuantity)
    {
        _quantity = newQuantity;
        return this;
    }


}


