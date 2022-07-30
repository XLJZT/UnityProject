using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemDatalist",menuName ="Inventory/ItemDatalist_SO")]
public class ItemDataList_SO : ScriptableObject
{
    //存放的是所有item的数据，并不是背包里item；
    public List<ItemDetails> itemDetailsList;

    //查询所有item数据，找到匹配的itemName数据并将其返回
    public ItemDetails GetItemDetails(ItemName itemName)
    {
        return itemDetailsList.Find(i => i.itemName == itemName);
    }


}
//每个item都会存储的数据项，进行序列化
[System.Serializable]
public class ItemDetails
{
    public ItemName itemName;
    public Sprite itemSprite;
}