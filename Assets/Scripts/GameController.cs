using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    [SerializeField] public PlayerController[] _players;
    [SerializeField] private int _playersBalance;
    [SerializeField] private TMP_Text[] _balanceText;
    [SerializeField] private TMP_Text rollInfoText;
    [SerializeField] public Button[] playersFinishTurn;
    private GameObject _curPlayer;
    public static bool isReadyToRoll = true;
    private void Start()
    {
        FindObjectOfType<ModelLoader>().LoadModels(Players.PLAYER1.ToString());
        FindObjectOfType<ModelLoader>().LoadModels(Players.PLAYER2.ToString());

        _players = FindObjectsOfType<PlayerController>();
        for(int i=0; i<_players.Length;i++)
        {
            _players[i].playerBalance = _playersBalance;
            _balanceText[i].text = _playersBalance.ToString();
        }
        _curPlayer = _players[0].gameObject;
        rollInfoText.gameObject.SetActive(false);

        playersFinishTurn[0].onClick.AddListener(() =>
        {
            FinishTurn((int)Players.PLAYER1);
        });

        playersFinishTurn[1].onClick.AddListener(() =>
        {
            FinishTurn((int)Players.PLAYER2);
        });

        foreach (var p in playersFinishTurn) p.gameObject.SetActive(false);
    }
    private void Update()
    {
        UpdatePlayerBalance();

        if (isReadyToRoll)
        {
            TextRollController(); //??????
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FindObjectOfType<CubeRandomizer>()
                    .GetComponent<CubeRandomizer>()
                    .RollDice(_curPlayer);
            }
        }
        else
        {
            rollInfoText.gameObject.SetActive(false);
        }
    }

   
    private void TextRollController()
    {
        rollInfoText.gameObject.SetActive(true);
        rollInfoText.text = $"Гравець {int.Parse(_curPlayer.tag)} кидає кубик";
    }

    private void UpdatePlayerBalance()
    {
        for (int i = 0; i < _players.Length; i++)
        {
            _balanceText[i].text = _players[i].playerBalance.ToString();
        }
    }

    public void FinishTurn(int index)
    {
        isReadyToRoll = true;
        _players[index].isPlayerNextTurn = false;
        _players[index == 1 ? 0 : 1].isPlayerNextTurn = true;
        playersFinishTurn[index].gameObject.SetActive(false);
    }
}



//private int _currentPlayerIndex = 0;
//private void SetWhoIsNext()
//{
//    _players[_currentPlayerIndex].isPlayerNextTurn = false;


//    _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Length;


//    _players[_currentPlayerIndex].isPlayerNextTurn = true;


//    _curPlayer = _players[_currentPlayerIndex].gameObject;
//    //print($"Player: {_curPlayer.name}, Balance: {_curPlayer.GetComponent<PlayerController>().playerBalance}");
//}