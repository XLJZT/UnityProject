using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private Dictionary<ItemName, bool> itemAvailableDict = new Dictionary<ItemName, bool>();
    private Dictionary<string, bool> interactiveStateDict = new Dictionary<string, bool>();
    private void OnEnable()
    {
        EventHandler.BeforScenceUnloadEvent += OnBeforScenceUnloadEvent;
        EventHandler.AfterScenceloadEvent += OnAfterScenceloadEvent;
        EventHandler.UpdateUiEvent += OnUpdateUiEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;

    }



    private void OnDisable()
    {
        EventHandler.BeforScenceUnloadEvent -= OnBeforScenceUnloadEvent;
        EventHandler.AfterScenceloadEvent -= OnAfterScenceloadEvent;
        EventHandler.UpdateUiEvent -= OnUpdateUiEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;

    }

    private void OnStartNewGameEvent(int obj)
    {
        itemAvailableDict.Clear();
        interactiveStateDict.Clear();
    }

    private void OnBeforScenceUnloadEvent()
    {
        //保存场景信息时，遍历没有存在字典中的物品
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
                itemAvailableDict.Add(item.itemName, true);
        }
        //保存遍历交互
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (!interactiveStateDict.ContainsKey(item.name))
                interactiveStateDict.Add(item.name, true);
            else
                interactiveStateDict[item.name] = item.isDone;
        }
    }

    private void OnAfterScenceloadEvent()
    {
        //加载遍历场景中的item，如果没有则添加进字典，如果有则设置值为字典中item的值
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
                itemAvailableDict.Add(item.itemName, true);
            else
                item.gameObject.SetActive(itemAvailableDict[item.itemName]);
        }

        //加载遍历交互
        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (!interactiveStateDict.ContainsKey(item.name))
                interactiveStateDict.Add(item.name, true);
            else
                item.isDone = interactiveStateDict[item.name];
        }
    }

    private void OnUpdateUiEvent(ItemDetails itemDetails, int arg2)
    {
        //拾取加载Ui时，同时更新字典中的值
        if(itemDetails!=null)
            itemAvailableDict[itemDetails.itemName] = false;
    }
}
