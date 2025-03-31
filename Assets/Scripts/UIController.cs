using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
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
    private bool alreadyBought;
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
        }
    }
    private void CallPanelInfo(GameObject obj)
    {
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
    }
    public void CallPanel(GameObject obj)
    {
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
            $"Власність гравця {int.Parse(tm.owner)}" :
            "Нічия власність";
        if (alreadyBought)
        {
            infoPanel.GetComponent<Image>().material.color = Color.red;
            sellButton.interactable = true;
            buyButton.interactable = false;
        }
        else
        {
            infoPanel.GetComponent<Image>().material.color = Color.white;
            sellButton.interactable = false;
            buyButton.interactable = true;
        }

        foreach(PlayerController pc in FindObjectsOfType<PlayerController>())
        {
            if (pc.isPlayerNextTurn)
            {
                FindObjectOfType<GameController>().
                    playersFinishTurn[int.Parse(pc.tag)].
                    gameObject.SetActive(true);
            }
        }
    }
    public void ClosePanel()
    {
        infoPanel.SetActive(false);
        infoText.gameObject.SetActive(false);
        GameController.isReadyToRoll = true;
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
                    infoText.gameObject.SetActive(true);
                    infoText.text = $"Придбана нова власність";
                }
                else
                {
                    infoText.gameObject.SetActive(true);
                    infoText.text = $"Недостатній баланс";
                }
                sellButton.interactable = true;
                buyButton.interactable = false;
                print(obj.GetComponent<PlayerController>().playerBalance);
                changesMade++;
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
                sellButton.interactable = false;
                buyButton.interactable = true;
                print(obj.GetComponent<PlayerController>().playerBalance);
                changesMade++;
                infoText.gameObject.SetActive(true);
                infoText.text = $"Власність продана";

            }
        }
    }
}
