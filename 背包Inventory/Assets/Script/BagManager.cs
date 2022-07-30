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

    //��ʾ��ǰ��Ʒ���б���bag���ȡ����
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

    /*���ɸ��ӣ��ж�����Ʒ���ɶ��ٸ���
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
            //ɾ���Ӽ��Ŀ�����
            Destroy(slotGrid.transform.GetChild(i).gameObject);
            //����б�
            slots.Clear();
        }
        for (int i = 0; i < bag.list.Count; i++)
        {
            //CreateSolt(bag.list[i]);

            //��ӿհ׸����ø���
            slots.Add(Instantiate(emptySlot));
            //����false��ʹ�ü��ص�ʱ�򲻱�����
            slots[i].transform.SetParent(slotGrid.transform,false);
            //����ͼƬ ����
            slots[i].GetComponent<Slot>().SetItem(bag.list[i]);
            slots[i].GetComponent<Slot>().slotId = i;
        }
    }
}
