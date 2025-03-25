using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Scriptable tiles",
                 menuName = "Tile",
                 order = 51)]
public class Tile : ScriptableObject
{
    public string nameOfTile;
    public string descOfTile;
    public string typeOfTile;
    public Sprite spriteOfTile;
    public int priceOfTile;
    public bool alreadyBought;
    public string ownerOfTile="None";
}
