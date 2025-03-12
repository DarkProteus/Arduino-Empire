using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tile tile;
    void Start()
    {
        bool o = tile.alreadyBought;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
