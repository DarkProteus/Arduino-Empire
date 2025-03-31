using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isPlayerNextTurn;
    public int lastIndex;
    private int _playerInt;
    public int playerBalance;
    private Rigidbody _rb;
    private GameObject curTile;
    private void Start()
    {
        _playerInt = (int)Players.PLAYER1;
        _rb = GetComponent<Rigidbody>();
        if (gameObject.tag ==_playerInt.ToString())
        {
            isPlayerNextTurn = true;
            print($"TurnSet: {gameObject.name}");
        }
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