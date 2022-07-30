using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagManager : MonoBehaviour
{
    public static BagManager instance;

    public Bag bag;
    public GameObject slotGrid;
    //public Slot slot;
    public GameObject emptySlot;

    public Text info;

    //显示当前物品的列表，从bag里获取数据
    public List<GameObject> slots = new List<GameObject>();

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable()
    {
        Refresh();
        info.text = "";
    }
    public void UpdateInfo(string itemInfo)
    {
        info.text = itemInfo;
    }

    /*生成格子，有多少物品生成多少格子
    public void CreateSolt(Item item)
    {
        Slot newslot = Instantiate(slot, instance.slotGrid.transform.position, Quaternion.identity);

        newslot.gameObject.transform.SetParent(instance.slotGrid.transform,false);

        newslot.image.sprite = item.sprite;
        newslot.item = item;
        newslot.num.text = item.holdNum.ToString();
    }
    */
    public void Refresh()
    {
        for (int i = 0; i < slotGrid.transform.childCount; i++)
        {
            if (slotGrid.transform.childCount == 0)
                break;
            //删除子集的空物体
            Destroy(slotGrid.transform.GetChild(i).gameObject);
            //清空列表
            slots.Clear();
        }
        for (int i = 0; i < bag.list.Count; i++)
        {
            //CreateSolt(bag.list[i]);

            //添加空白格并设置父级
            slots.Add(Instantiate(emptySlot));
            //设置false会使得加载的时候不被缩放
            slots[i].transform.SetParent(slotGrid.transform,false);
            //设置图片 数量
            slots[i].GetComponent<Slot>().SetItem(bag.list[i]);
            slots[i].GetComponent<Slot>().slotId = i;
        }
    }
}
