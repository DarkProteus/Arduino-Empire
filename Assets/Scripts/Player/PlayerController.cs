using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isPlayerNextTurn;
    public short lastIndex;
    public short passTurnCounter = -1;
    private short _playerInt;
    public int playerBalance;
    private Rigidbody _rb;
    private GameObject _curTile;
    private void Start()
    {
        _playerInt = (int)Players.PLAYER1;
        _rb = GetComponent<Rigidbody>();
        if (!gameObject.CompareTag(_playerInt.ToString())) return;
        isPlayerNextTurn = true;
        print($"TurnSet: {gameObject.name}");
    }
}