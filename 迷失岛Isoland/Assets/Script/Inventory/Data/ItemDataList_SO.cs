using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemDatalist",menuName ="Inventory/ItemDatalist_SO")]
public class ItemDataList_SO : ScriptableObject
{
    //��ŵ�������item�����ݣ������Ǳ�����item��
    public List<ItemDetails> itemDetailsList;

    //��ѯ����item���ݣ��ҵ�ƥ���itemName���ݲ����䷵��
    public ItemDetails GetItemDetails(ItemName itemName)
    {
        return itemDetailsList.Find(i => i.itemName == itemName);
    }


}
//ÿ��item����洢��������������л�
[System.Serializable]
public class ItemDetails
{
    public ItemName itemName;
    public Sprite itemSprite;
}