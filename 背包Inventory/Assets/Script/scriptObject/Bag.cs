using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Bag",menuName ="Item/bag")]
public class Bag : ScriptableObject
{
    public List<Item> list = new List<Item>();
}
