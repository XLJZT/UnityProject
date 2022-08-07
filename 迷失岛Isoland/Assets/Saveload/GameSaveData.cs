using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveData
{
    public int gameWeek;
    public string currentScence;
    public Dictionary<string, bool> miniGameDict;
    public  Dictionary<ItemName, bool> itemAvailableDict;
    public  Dictionary<string, bool> interactiveStateDict;
    public List<ItemName> itemList;

}
