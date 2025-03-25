using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileManager : MonoBehaviour
{
    [SerializeField]private Tile tile;
    private TMP_Text _text;
    public string name;
    public string owner;
    public Sprite sprite;
    public string desc;
    public int price;
    public string type;
    public bool alreadyBought;
    void Start()
    {
        name = tile.nameOfTile;
        owner = tile.ownerOfTile;
        desc = tile.descOfTile;
        price = tile.priceOfTile;
        type = tile.typeOfTile;
        alreadyBought = tile.alreadyBought;
        sprite = tile.spriteOfTile;
        if (type != "Event")
        {
            _text = gameObject.GetComponentInChildren<TMP_Text>(true);
            _text.gameObject.SetActive(true);
            _text.text = name;
        }
    }

}
