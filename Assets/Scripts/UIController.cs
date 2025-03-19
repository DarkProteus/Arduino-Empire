using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TMP_Text descText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text typeText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private GameObject sellButton;
    [SerializeField] private GameObject buyButton;
    private bool _alreadyBought;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                CallPanelInfo(hit.collider.gameObject);
            }
        }
    }
    private void CallPanelInfo(GameObject obj)
    {
        infoPanel.SetActive(true);
        nameText.text = obj.GetComponent<TileManager>()._name;
        descText.text = obj.GetComponent<TileManager>()._desc;
        typeText.text = obj.GetComponent<TileManager>()._type;
        priceText.text = obj.GetComponent<TileManager>()._price.ToString();
        _alreadyBought = obj.GetComponent<TileManager>()._alreadyBought;
        sellButton.SetActive(false); 
        buyButton.SetActive(false);
    }
    public void CallPanel(GameObject obj)
    {
        infoPanel.SetActive(true);
        nameText.text = obj.GetComponent<TileManager>()._name;
        descText.text = obj.GetComponent<TileManager>()._desc;
        typeText.text = obj.GetComponent<TileManager>()._type;
        priceText.text = obj.GetComponent<TileManager>()._price.ToString();
        _alreadyBought = obj.GetComponent<TileManager>()._alreadyBought;
        if (_alreadyBought)
        {
            sellButton.SetActive(true);
            buyButton.SetActive(false);
        }
        else
        {
            sellButton.SetActive(false);
            buyButton.SetActive(true);
        }
    }
    public void ClosePanel()
    {
        infoPanel.SetActive(false);
    }
}
