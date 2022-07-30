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
        //���泡����Ϣʱ������û�д����ֵ��е���Ʒ
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
                itemAvailableDict.Add(item.itemName, true);
        }
        //�����������
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
        //���ر��������е�item�����û������ӽ��ֵ䣬�����������ֵΪ�ֵ���item��ֵ
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
                itemAvailableDict.Add(item.itemName, true);
            else
                item.gameObject.SetActive(itemAvailableDict[item.itemName]);
        }

        //���ر�������
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
        //ʰȡ����Uiʱ��ͬʱ�����ֵ��е�ֵ
        if(itemDetails!=null)
            itemAvailableDict[itemDetails.itemName] = false;
    }
}
