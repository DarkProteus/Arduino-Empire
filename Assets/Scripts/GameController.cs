using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController[] _players;
    [SerializeField] private int playersBalance;
    private GameObject _curPlayer;
    private void Start()
    {
        FindObjectOfType<ModelLoader>().LoadModels(Players.PLAYER1.ToString());
        FindObjectOfType<ModelLoader>().LoadModels(Players.PLAYER2.ToString());

        _players = FindObjectsOfType<PlayerController>();
        foreach (PlayerController plr in _players)
        {
            plr.playerBalance = playersBalance;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayersTurn();
            FindObjectOfType<CubeRandomizer>()
                .GetComponent<CubeRandomizer>()
                .RollDice(_curPlayer);
        }
    }
    private int _currentPlayerIndex = 0;
    private void PlayersTurn()
    {
        //foreach(PlayerController player in _players)
        //{
        //    if (player.isPlayerNextTurn)
        //    {
        //        _curPlayer = player.gameObject;
        //        player.isPlayerNextTurn = false;
        //    }
        //    else player.isPlayerNextTurn = true; //temp
        //}
        //TODO
        _players[_currentPlayerIndex].isPlayerNextTurn = false;

     
        _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Length;

       
        _players[_currentPlayerIndex].isPlayerNextTurn = true;

        
        _curPlayer = _players[_currentPlayerIndex].gameObject;
        print(_curPlayer.name);
    }
}
