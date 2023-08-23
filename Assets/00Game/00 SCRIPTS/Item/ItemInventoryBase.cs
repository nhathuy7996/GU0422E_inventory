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
    [SerializeField]
    protected int _maxCapacity;
}


