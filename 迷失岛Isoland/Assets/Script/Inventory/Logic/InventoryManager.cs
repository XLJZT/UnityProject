using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>,ISaveable
{
    public ItemDataList_SO itemData;

    [SerializeField]
    private List<ItemName> itemList = new List<ItemName>();

    private void Start()
    {
        ISaveable saveable = this;
        saveable.SaveableRegister();
    }
    private void OnEnable()
    {
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
        EventHandler.ChangeItemEvent += OnChangeItemEvent;
        EventHandler.AfterScenceloadEvent += OnAfterScenceloadEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;

    }
    private void OnDisable()
    {
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
        EventHandler.ChangeItemEvent -= OnChangeItemEvent;
        EventHandler.AfterScenceloadEvent -= OnAfterScenceloadEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;


    }

    private void OnStartNewGameEvent(int obj)
    {
        itemList.Clear();
    }

    private void OnAfterScenceloadEvent()
    {
        if (itemList.Count == 0)
        {
            EventHandler.CallUpdateUiEvent(null, -1);
        }
        else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                EventHandler.CallUpdateUiEvent(itemData.GetItemDetails(itemList[i]), i);
            }
        }   
    }

    private void OnChangeItemEvent(int index)
    {
        if (index >= 0 && index < itemList.Count)
        {
            ItemDetails item = itemData.GetItemDetails(itemList[index]);
            EventHandler.CallUpdateUiEvent(item,index);
        }
    }

    private void OnItemUsedEvent(ItemName itemName)
    {
        //根据下标删除已经使用的物品
        var index = GetIndex(itemName);
        itemList.RemoveAt(index);
        
        //如果使用之后背包里没有物品了
        if (itemList.Count == 0)
            EventHandler.CallUpdateUiEvent(null, -1);
        
    }

    public void AddItem(ItemName itemName)
    {
        if (!itemList.Contains(itemName))
        {
            itemList.Add(itemName);
            //UI显示
            EventHandler.CallUpdateUiEvent(itemData.GetItemDetails(itemName), itemList.Count - 1);
        }

    }

    /// <summary>
    /// 获取itemname在itemList里的下标
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    private int GetIndex(ItemName itemName)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == itemName)
            {
                return i;
            }
        }
        return -1;
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData= new GameSaveData();
        saveData.itemList = this.itemList;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.itemList = saveData.itemList;
    }
}
