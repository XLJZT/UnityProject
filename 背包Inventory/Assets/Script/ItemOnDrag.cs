using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //移动的物体的初始父级
    public Transform originParent;

    public Bag bag;
    int currentItemId;
    public void OnBeginDrag(PointerEventData eventData)
    {
        originParent = transform.parent;
        currentItemId = originParent.GetComponent<Slot>().slotId;
        //更换父级，使其置于上层不被遮挡
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //判断是否属于设置的界面外
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            //鼠标射线碰撞到的物体,可能碰到图片或文字
            if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage" || eventData.pointerCurrentRaycast.gameObject.name == "Number")
            {
                //更换鼠标移动物体的位置和父级
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                //更换两个物体信息
                var temp = bag.list[currentItemId];
                bag.list[currentItemId] = bag.list[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId];
                bag.list[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId] = temp;
                //更换当前鼠标位置物体的位置和父级为原物体的位置与父级
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originParent);

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            //如果是空值，则交换空值的位置
            //更换当前鼠标位置物体的位置和父级为原物体的位置与父级
            //eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).SetParent(originParent);
            //eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).position = originParent.position;
            if (eventData.pointerCurrentRaycast.gameObject.name == "Slot(Clone)")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;

                bag.list[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId] = bag.list[currentItemId];
                //判断是不是同一个位置
                //同一位置则不能设置为null
                if (eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId != currentItemId)
                    bag.list[currentItemId] = null;

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }
        //如果是则返回原位置
        transform.SetParent(originParent);
        transform.position = originParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;


    }
}
