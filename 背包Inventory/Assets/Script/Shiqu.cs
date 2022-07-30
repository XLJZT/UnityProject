using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiqu : MonoBehaviour
{
    public Item item;

    public Bag bag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("item" + item.holdNum);
            if (bag.list.Contains(item))
            {
                item.holdNum += 1;
                //Debug.Log("bag:" + bag.list[0].holdNum);
            }
            else
            {
                //bag.list.Add(item);

                //如果有空位则添加
                for (int i = 0; i < bag.list.Count; i++)
                {
                    if (bag.list[i] == null)
                    {
                        bag.list[i] = item;
                        break;
                    }
                }

            }

            Destroy(gameObject);
        }
        BagManager.instance.Refresh();
    }
}
