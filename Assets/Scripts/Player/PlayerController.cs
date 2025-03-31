using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isPlayerNextTurn = false;
    public int lastIndex;
    public int playerBalance;
    private Rigidbody _rb;
    private GameObject curTile;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (gameObject.tag == Players.PLAYER1.ToString())
            isPlayerNextTurn = true;
    }

 

    public int Buy(Tile tile)
    {
        return 0;
    }
    public int Sell(Tile tile)
    {
        return 0;
    }
    public int Penalty(Tile tile)
    {
        return 0;
    }
}








//public void PlayerTurn()
//{
//    RaycastHit hit;
//    if (_rb.velocity.magnitude == 0)
//    {
//        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
//        {
//            print(hit.collider.name);
//            //next actions              
//        }
//    }
//}