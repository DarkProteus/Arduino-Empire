using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    [SerializeField] public PlayerController[] players;
    [SerializeField] private int playersBalance;
    [SerializeField] private TMP_Text[] balanceText;
    [SerializeField] private TMP_Text rollInfoText;
    [SerializeField] public Button[] playersFinishTurn;
    private bool _lastTurnFinished;
    private UIController _uc;
    [HideInInspector]public GameObject curPlayer;
    public static bool isReadyToRoll = true;
    private void Start()
    {
        _uc = GetComponent<UIController>();
        _lastTurnFinished = true;
        FindObjectOfType<ModelLoader>().LoadModels(Players.PLAYER1.ToString());
        FindObjectOfType<ModelLoader>().LoadModels(Players.PLAYER2.ToString());

        players = FindObjectsOfType<PlayerController>();
        players = players.OrderByDescending(p => p.name == "PLAYER1").ToArray();
        for (var i=0; i<players.Length;i++)
        {
            players[i].playerBalance = playersBalance;
            balanceText[i].text = playersBalance.ToString();
        }
        curPlayer = players[0].gameObject;
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
            //добавить еще раз метод финиш терн если у игрока пенальти. закончить ход и прервать метод
            TextRollController(); 
            if (Input.GetKeyDown(KeyCode.Tab) && _lastTurnFinished &&
                curPlayer.GetComponent<PlayerController>().passTurnCounter <= 0)
            {
                _lastTurnFinished = false;
                print(curPlayer); 
                FindObjectOfType<CubeRandomizer>()
                    .GetComponent<CubeRandomizer>()
                    .RollDice(curPlayer);
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
        rollInfoText.text = $"Гравець {int.Parse(curPlayer.tag)+1} кидає кубик.\n\r Натисніть TAB";
    }

    private void UpdatePlayerBalance()
    {
        for (var i = 0; i < players.Length; i++)
        {
            balanceText[i].text = players[i].playerBalance.ToString();
        }
    }

    private void FinishTurn(int index)
    {
        isReadyToRoll = true;
        players[index].isPlayerNextTurn = false;
        players[index == 1 ? 0 : 1].isPlayerNextTurn = true;
        curPlayer = players[index == 1 ? 0 : 1].gameObject;
        playersFinishTurn[index].gameObject.SetActive(false);
        _lastTurnFinished = true;
        curPlayer.GetComponent<PlayerController>().passTurnCounter--;
    }
}



//private int _currentPlayerIndex = 0;
//private void SetWhoIsNext()
//{
//    _players[_currentPlayerIndex].isPlayerNextTurn = false;


//    _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Length;


//    _players[_currentPlayerIndex].isPlayerNextTurn = true;


//    curPlayer = _players[_currentPlayerIndex].gameObject;
//    //print($"Player: {curPlayer.name}, Balance: {curPlayer.GetComponent<PlayerController>().playerBalance}");
//}