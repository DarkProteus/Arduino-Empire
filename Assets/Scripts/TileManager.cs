using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileManager : MonoBehaviour
{
    [SerializeField]private Tile tile;
    private TMP_Text _text;
    public string _name;
    public Sprite _sprite;
    public string _desc;
    public int _price;
    public string _type;
    public bool _alreadyBought;
    void Start()
    {
        _name = tile.nameOfTile;
        _desc = tile.descOfTile;
        _price = tile.priceOfTile;
        _type = tile.typeOfTile;
        _alreadyBought = tile.alreadyBought;
        _sprite = tile.spriteOfTile;
        if (_type != "Event")
        {
            _text = gameObject.GetComponentInChildren<TMP_Text>(true);
            _text.gameObject.SetActive(true);
            _text.text = _name;
        }
    }

}
