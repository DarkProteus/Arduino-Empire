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
                TileManager isExist;
                if (hit.collider.gameObject.TryGetComponent<TileManager>(out isExist))
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
        img.sprite = obj.GetComponent<TileManager>().sprite;
        nameText.text = obj.GetComponent<TileManager>().name;
        descText.text = obj.GetComponent<TileManager>().desc;
        ownerText.text = obj.GetComponent<TileManager>().owner;
        typeText.text = obj.GetComponent<TileManager>().type;
        priceText.text = obj.GetComponent<TileManager>().price.ToString();
        alreadyBought = obj.GetComponent<TileManager>().alreadyBought;
        if (alreadyBought)
        {
            sellButton.interactable = true;
            buyButton.interactable = false;
        }
        else
        {
            sellButton.interactable = false;
            buyButton.interactable = true;
        }
    }
    public void ClosePanel()
    {
        infoPanel.SetActive(false);
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
            }
        }
    }
}
