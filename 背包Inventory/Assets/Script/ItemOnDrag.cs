using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //�ƶ�������ĳ�ʼ����
    public Transform originParent;

    public Bag bag;
    int currentItemId;
    public void OnBeginDrag(PointerEventData eventData)
    {
        originParent = transform.parent;
        currentItemId = originParent.GetComponent<Slot>().slotId;
        //����������ʹ�������ϲ㲻���ڵ�
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
        //�ж��Ƿ��������õĽ�����
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            //���������ײ��������,��������ͼƬ������
            if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage" || eventData.pointerCurrentRaycast.gameObject.name == "Number")
            {
                //��������ƶ������λ�ú͸���
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;
                //��������������Ϣ
                var temp = bag.list[currentItemId];
                bag.list[currentItemId] = bag.list[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId];
                bag.list[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId] = temp;
                //������ǰ���λ�������λ�ú͸���Ϊԭ�����λ���븸��
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originParent);

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
            //����ǿ�ֵ���򽻻���ֵ��λ��
            //������ǰ���λ�������λ�ú͸���Ϊԭ�����λ���븸��
            //eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).SetParent(originParent);
            //eventData.pointerCurrentRaycast.gameObject.transform.GetChild(0).position = originParent.position;
            if (eventData.pointerCurrentRaycast.gameObject.name == "Slot(Clone)")
            {
                transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
                transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;

                bag.list[eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId] = bag.list[currentItemId];
                //�ж��ǲ���ͬһ��λ��
                //ͬһλ����������Ϊnull
                if (eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<Slot>().slotId != currentItemId)
                    bag.list[currentItemId] = null;

                GetComponent<CanvasGroup>().blocksRaycasts = true;
                return;
            }
        }
        //������򷵻�ԭλ��
        transform.SetParent(originParent);
        transform.position = originParent.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;


    }
}
