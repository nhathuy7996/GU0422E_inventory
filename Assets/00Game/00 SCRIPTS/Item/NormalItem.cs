using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalItem : ItemInventoryBase
{
    [SerializeField] Image _imageItem;
    [SerializeField] Text _nameItem;
    [SerializeField] Text _quantityItem;
  
    // Start is called before the first frame update
    void Start()
    {
        if (_info == null)
            return;
        _imageItem.sprite = _info._image;
        _nameItem.text = _info._name;
        _quantityItem.text = this._quantity.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override ItemInventoryBase UpdateInfo(ItemDataSO itemInfo)
    {
        base.UpdateInfo(itemInfo);
        updateView();

        return this;
    }

    public override ItemInventoryBase UpdateQuantity(int newQuantity)
    {
        base.UpdateQuantity(newQuantity);
        updateView();

        return this;
    }

    void updateView()
    {
        if (_info == null)
            return;
        _imageItem.sprite = _info._image;
        _nameItem.text = _info._name;
        _quantityItem.text = this._quantity.ToString();
    }

    private void OnDrawGizmosSelected()
    {
        this.updateView();
    }
}
