using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotId;
    public Item item;

    public Image image;

    public Text num;

    public GameObject itemInSlot;

    public string slotInfo;
    public void ItemOnClick()
    {
        BagManager.instance.UpdateInfo(slotInfo);
    }

    public void SetItem(Item item)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }
        //¸³Öµ
        image.sprite = item.sprite;
        num.text = item.holdNum.ToString();
        slotInfo = item.info;
    }
}
