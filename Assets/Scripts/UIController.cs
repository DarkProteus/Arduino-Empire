using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Button closeButton;
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
    private bool alreadyBought;
    private string _curPlayer;
    private int changesMade;
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.TryGetComponent<TileManager>( out TileManager isExist))
                {
                    CallPanelInfo(hit.collider.gameObject);
                }
            }
        }else if (Input.GetMouseButtonDown(0) && mc.devCheck)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.TryGetComponent<TileManager>(out TileManager isExist))
                {
                    CallPanel(hit.collider.gameObject);
                }
            }
        }
        _curPlayer = gc.curPlayer.name;
    }
    
    public void CallPanel(GameObject obj)
    {
        closeButton.onClick.RemoveAllListeners();
        sellButton.gameObject.SetActive(true);
        buyButton.gameObject.SetActive(true);
        changesMade = 0;
        infoPanel.SetActive(true);
        TileManager tm = obj.GetComponent<TileManager>();
        img.sprite = tm.sprite;
        nameText.text = tm.name;
        descText.text = tm.desc;
        typeText.text = tm.type;
        priceText.text = tm.price.ToString();
        alreadyBought = tm.alreadyBought;
        ownerText.text =
            alreadyBought ?
            $"Власність гравця {(tm.owner == "PLAYER1" ? 1 : 2)}" :
            "Нічия власність";
        print(alreadyBought ? $"Власність гравця {(tm.owner == "PLAYER1" ? 1 : 2)}" : "Нічия власність");
        if (alreadyBought)
        {
            if (tm.owner == _curPlayer)
            {
                infoPanel.GetComponent<Image>().material.color = Color.red;
                sellButton.interactable = true;
                buyButton.interactable = false;
            }
            else
            {
                sellButton.interactable = false;
                buyButton.interactable = false;
                gc.curPlayer.gameObject.GetComponent<PlayerController>().playerBalance -= 20 + (int)(tm.price * 0.2f);
                foreach (PlayerController pl in gc._players)
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
        closeButton.onClick.AddListener(ClosePanel);
    }
    private void CallPanelInfo(GameObject obj)
    {
        closeButton.onClick.RemoveAllListeners();
        infoPanel.SetActive(true);
        img.sprite = obj.GetComponent<TileManager>().sprite;
        nameText.text = obj.GetComponent<TileManager>().name;
        descText.text = obj.GetComponent<TileManager>().desc;
        ownerText.text = obj.GetComponent<TileManager>().owner;
        typeText.text = obj.GetComponent<TileManager>().type;
        priceText.text = obj.GetComponent<TileManager>().price.ToString();
        alreadyBought = obj.GetComponent<TileManager>().alreadyBought;
        sellButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
        closeButton.onClick.AddListener(CloseInfoPanel);
    }
    public void ClosePanel()
    {
        infoPanel.SetActive(false);
        infoText.gameObject.SetActive(false);
        GameController.isReadyToRoll = true; 
        foreach (PlayerController pc in gc._players)
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
    public void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
        infoText.gameObject.SetActive(false);
    }
    public void BuyTile()
    {
       foreach (PlayerController player in gc._players){
            if (player.isPlayerNextTurn && changesMade == 0)
            {
                GameObject obj = player.gameObject;
                RaycastHit hit;
                Vector3 downDirection = Vector3.down;
                Physics.Raycast(obj.transform.position, downDirection, out hit, Mathf.Infinity);
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
                    changesMade++;
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
        foreach (PlayerController player in gc._players)
        {
            if (player.isPlayerNextTurn && changesMade == 0)
            {
                GameObject obj = player.gameObject;
                RaycastHit hit;
                Vector3 downDirection = Vector3.down;
                Physics.Raycast(obj.transform.position, downDirection, out hit, Mathf.Infinity);
                obj.GetComponent<PlayerController>().playerBalance += hit.transform.gameObject.GetComponent<TileManager>().price;
                hit.transform.gameObject.GetComponent<TileManager>().owner = "None";
                hit.transform.gameObject.GetComponent<TileManager>().alreadyBought = false;
                sellButton.interactable = false;
                buyButton.interactable = true;
                print(obj.GetComponent<PlayerController>().playerBalance);
                changesMade++;
                infoText.gameObject.SetActive(true);
                infoText.text = $"Власність продана";

            }
        }
    }
    public void CheckGameFinished()
    {
        string obj;
        foreach (PlayerController pl in gc._players)
        {
            if (pl.playerBalance <= 0)
            {
                if (pl.gameObject.name == Players.PLAYER1.ToString())
                {
                    obj = Players.PLAYER2.ToString();
                    gameFinishedPanel.SetActive(true);
                    playerWinText.text = $"{obj} Won";
                    Time.timeScale = 0f;
                }
                else if (pl.gameObject.name == Players.PLAYER2.ToString())
                {
                    obj = Players.PLAYER1.ToString();
                    gameFinishedPanel.SetActive(true);
                    playerWinText.text = $"{obj} Won";
                    Time.timeScale = 0f;
                }
            }
        }
    }
}
