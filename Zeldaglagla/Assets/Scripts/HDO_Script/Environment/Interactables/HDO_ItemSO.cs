using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "new item", order = 1)]
public class HDO_ItemSO : ScriptableObject
{
    public Sprite sprite;
    public string itemName;
    public string itemDescription;
}
