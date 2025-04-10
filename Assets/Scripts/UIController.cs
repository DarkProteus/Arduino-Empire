using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject gameFinishedPanel;
    [SerializeField] private TMP_Text descText;
    [SerializeField] private TMP_Text ownerText;
    [SerializeField] private Image img;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text typeText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private GameController gc;
    [SerializeField] private TMP_Text infoText;
    [SerializeField] private TMP_Text playerWinText;
    [SerializeField] private MoveController mc;
    private bool _alreadyBought;
    private string _curPlayer;
    private int _changesMade;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.gameObject.TryGetComponent<TileManager>( out var isExist))
                {
                    CallPanelInfo(hit.collider.gameObject);
                }
            }
        }else if (Input.GetMouseButtonDown(0) && mc.devCheck)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.gameObject.TryGetComponent<TileManager>(out var isExist))
                {
                    CallPanel(hit.collider.gameObject);
                }
            }
        }
        _curPlayer = gc.curPlayer.name;
    }
    private void CallPanelInfo(GameObject obj)
    {
        infoPanel.SetActive(true);
        var tm = infoText.GetComponent<TileManager>();
        img.sprite = tm.sprite;
        nameText.text = tm.name;
        descText.text = tm.desc;
        ownerText.text = tm.owner;
        typeText.text = tm.type;
        priceText.text = tm.price.ToString();
        _alreadyBought = tm.alreadyBought;
        sellButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
    }
    public void CallPanel(GameObject obj)
    {
        infoPanel.GetComponent<Image>().material.color = Color.white;
        sellButton.gameObject.SetActive(true);
        buyButton.gameObject.SetActive(true);
        _changesMade = 0;
        infoPanel.SetActive(true);
        var tm = obj.GetComponent<TileManager>();
        img.sprite = tm.sprite;
        nameText.text = tm.name;
        descText.text = tm.desc;
        typeText.text = tm.type;
        priceText.text = tm.price.ToString();
        _alreadyBought = tm.alreadyBought;
        ownerText.text =
            _alreadyBought ?
            $"Власність гравця {(tm.owner == "PLAYER1" ? 1 : 2)}" :
            "Нічия власність";
        
        if (_alreadyBought)
        {
            if (tm.owner == _curPlayer)
            {
                infoPanel.GetComponent<Image>().material.color = Color.red;
                sellButton.interactable = true;
                buyButton.interactable = false;
            }
            else
            {
                var penalty = 20 + (int)(tm.price * 0.2f);
                descText.text = $"Ця власніть вже продана. Сплатіть {penalty} енергії за користування";
                sellButton.interactable = false;
                buyButton.interactable = false;
                gc.curPlayer.gameObject.GetComponent<PlayerController>().playerBalance -= penalty;
                foreach (var pl in gc.players)
                {
                    if (pl.gameObject.name != gc.curPlayer.name)
                    {
                        pl.playerBalance+=20+(int)(tm.price*0.2f);
                    }
                }
            }
        }
        else
        {
            infoPanel.GetComponent<Image>().material.color = Color.white;
            sellButton.interactable = false;
            buyButton.interactable = true;
        }

        
    }
    public void ClosePanel()
    {
        infoPanel.SetActive(false);
        infoText.gameObject.SetActive(false);
        GameController.isReadyToRoll = true; 
        foreach (var pc in gc.players)
        {
            if (pc.isPlayerNextTurn)
            {
                FindObjectOfType<GameController>().
                    playersFinishTurn[int.Parse(pc.tag)].
                    gameObject.SetActive(true);
            }
        }
        CheckGameFinished();
    }
    public void BuyTile()
    {
       foreach (var player in gc.players){
            if (player.isPlayerNextTurn && _changesMade == 0)
            {
                var obj = player.gameObject;
                var downDirection = Vector3.down;
                Physics.Raycast(obj.transform.position, downDirection, out var hit, Mathf.Infinity);
                if (obj.GetComponent<PlayerController>().playerBalance >= hit.transform.gameObject.GetComponent<TileManager>().price)
                {
                    obj.GetComponent<PlayerController>().playerBalance -= hit.transform.gameObject.GetComponent<TileManager>().price;
                    hit.transform.gameObject.GetComponent<TileManager>().owner = obj.name;
                    hit.transform.gameObject.GetComponent<TileManager>().alreadyBought = true;
                    infoText.gameObject.SetActive(true);
                    infoText.text = $"Придбана нова власність";
                    print($"{obj.name}:  {obj.GetComponent<PlayerController>().playerBalance}");
                    print($"{hit.transform.gameObject.name}:  {hit.transform.gameObject.GetComponent<TileManager>().price}");
                    sellButton.interactable = true;
                    buyButton.interactable = false;
                    _changesMade++;
                }
                else
                {
                    infoText.gameObject.SetActive(true);
                    infoText.text = $"Недостатній баланс";
                }
            }
       }
    }
    public void SellTile()
    {
        foreach (var player in gc.players)
        {
            if (player.isPlayerNextTurn && _changesMade == 0)
            {
                var obj = player.gameObject;
                var downDirection = Vector3.down;
                Physics.Raycast(obj.transform.position, downDirection, out var hit, Mathf.Infinity);
                obj.GetComponent<PlayerController>().playerBalance += hit.transform.gameObject.GetComponent<TileManager>().price;
                hit.transform.gameObject.GetComponent<TileManager>().owner = "None";
                hit.transform.gameObject.GetComponent<TileManager>().alreadyBought = false;
                sellButton.interactable = false;
                buyButton.interactable = true;
                print(obj.GetComponent<PlayerController>().playerBalance);
                _changesMade++;
                infoText.gameObject.SetActive(true);
                infoText.text = $"Власність продана";

            }
        }
    }

    private void CheckGameFinished()
    {
        foreach (var pl in gc.players)
        {
            if (pl.playerBalance <= 0)
            {
                string obj;
                if (pl.gameObject.name == Players.PLAYER1.ToString())
                {
                    obj = Players.PLAYER2.ToString();
                    gameFinishedPanel.SetActive(true);
                    playerWinText.text = $"{obj} Виграв";
                    Time.timeScale = 0f;
                }
                else if (pl.gameObject.name == Players.PLAYER2.ToString())
                {
                    obj = Players.PLAYER1.ToString();
                    gameFinishedPanel.SetActive(true);
                    playerWinText.text = $"{obj} Виграв";
                    Time.timeScale = 0f;
                }
            }
        }
    }


    private void PassTurn(short passTimes)
    {
        gc.curPlayer.gameObject.GetComponent<PlayerController>().passTurnCounter = passTimes;
    }

    private void PenaltyTurn(byte penalty)
    {
        gc.curPlayer.gameObject.GetComponent<PlayerController>().playerBalance -= penalty;
    }
}
