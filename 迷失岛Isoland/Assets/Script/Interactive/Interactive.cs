using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requireItem;

    public bool isDone;

    public void CheckItem(ItemName itemName)
    {
        if (!isDone && requireItem == itemName)
        {
            isDone = true;
            OnClickAction();
            //�����ȷʹ�ú�����������Ʒ
            EventHandler.CallItemUsedEvent(itemName);
        }
        else
        {
            EmptyClick();
        }
    }

    protected virtual void OnClickAction()
    {

    }

    public virtual void EmptyClick()
    {

    }
}
