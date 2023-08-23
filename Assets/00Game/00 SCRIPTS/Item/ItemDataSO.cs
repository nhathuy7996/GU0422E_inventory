using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="itemData",menuName ="Item")]
public class ItemDataSO : ScriptableObject
{
    public int _ID;
    public string _name;
    public Sprite _image;
}

