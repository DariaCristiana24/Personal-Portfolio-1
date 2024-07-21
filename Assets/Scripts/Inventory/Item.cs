using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create a new Item")]
public class Item : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite itemIcon;
    public GameObject itemGameObject;
}
