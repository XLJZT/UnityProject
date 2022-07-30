using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SlotUI : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Image itemImage;
    public UIToolTip uIToolTip;

    private ItemDetails currentItem;
    private bool isSelected;
    

    public void SetItem(ItemDetails item)
    {
        currentItem = item;
        gameObject.SetActive(true);
        itemImage.sprite = item.itemSprite;
        itemImage.SetNativeSize();
    }

    public void SetEmpty()
    {
        gameObject.SetActive(false);

    }


    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = !isSelected;
        EventHandler.CallItemSelectEvent(currentItem, isSelected);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.activeInHierarchy)
        {
            uIToolTip.gameObject.SetActive(true);
            uIToolTip.UpdateItemName(currentItem.itemName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uIToolTip.gameObject.SetActive(false);
    }
}
