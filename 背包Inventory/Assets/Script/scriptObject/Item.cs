using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Item/item")]
public class Item : ScriptableObject
{
    public string _name;
    public Sprite sprite;
    public int holdNum;
    [TextArea]
    public string  info;
}
