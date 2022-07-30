using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventHandler
{
    public static event Action<ItemDetails, int> UpdateUiEvent;
    public static void CallUpdateUiEvent(ItemDetails itemDetails,int index)
    {
        UpdateUiEvent?.Invoke(itemDetails,index);
    }

    public static event Action BeforScenceUnloadEvent;
    public static void CallBeforScenceUnloadEvent()
    {
        BeforScenceUnloadEvent?.Invoke();
    }

    public static event Action AfterScenceloadEvent;
    public static void CallAfterScenceloadEvent()
    {
        AfterScenceloadEvent?.Invoke();
    }

    public static event Action<ItemDetails, bool> ItemSelectEvent;
    public static void CallItemSelectEvent(ItemDetails itemDetails,bool isSelected)
    {
        ItemSelectEvent?.Invoke(itemDetails, isSelected);
    }

    public static event Action<ItemName> ItemUsedEvent;
    public static void CallItemUsedEvent(ItemName itemName)
    {
        ItemUsedEvent?.Invoke(itemName);
    }

    public static event Action<int> ChangeItemEvent;
    public static void CallChangItemEvent(int index)
    {
        ChangeItemEvent?.Invoke(index);
    }

    public static event Action<string> ShowDialogEvent;

    public static void CallShowDialogEvent(string dialog)
    {
        ShowDialogEvent?.Invoke(dialog);
    }

    public static event Action<GameState> GameStateChangeEvent;
    public static void CallGameStateChangeEvent(GameState gameState)
    {
        GameStateChangeEvent?.Invoke(gameState);
    }

    public static event Action CheckGameStateEvent;
    public static void CallCheckGameStateEvent()
    {
        CheckGameStateEvent?.Invoke();
    }

    public static event Action<string> GamePassEvent;
    public static void CallGamePassEvent(string gameName)
    {
        GamePassEvent?.Invoke(gameName);
    }

    public static event Action<int> StartNewGameEvent;
    public static void CallStartNewGameEvent(int gameWeek)
    {
        StartNewGameEvent?.Invoke(gameWeek);
    }
}
