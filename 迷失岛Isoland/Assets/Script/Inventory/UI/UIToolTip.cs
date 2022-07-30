using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIToolTip : MonoBehaviour
{
    public Text itemNameText;
    public void UpdateItemName(ItemName itemName)
    {
        itemNameText.text = itemName switch
        {
            ItemName.Key => "һ��Կ��",
            ItemName.Ticket => "һ�Ŵ�Ʊ",
            _ => ""
        };
    }
}
