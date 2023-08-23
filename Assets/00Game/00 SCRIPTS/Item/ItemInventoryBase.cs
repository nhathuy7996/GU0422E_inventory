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

    public virtual void UpdateQuantity(int newQuantity)
    {
        _quantity = newQuantity;
    }
}


