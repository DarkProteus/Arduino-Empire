using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TMP_Text descText;
    [SerializeField] private Image img;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text typeText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button buyButton;
    private bool _alreadyBought;

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
        img.sprite = obj.GetComponent<TileManager>()._sprite;
        nameText.text = obj.GetComponent<TileManager>()._name;
        descText.text = obj.GetComponent<TileManager>()._desc;
        typeText.text = obj.GetComponent<TileManager>()._type;
        priceText.text = obj.GetComponent<TileManager>()._price.ToString();
        _alreadyBought = obj.GetComponent<TileManager>()._alreadyBought;
        sellButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
    }
    public void CallPanel(GameObject obj)
    {
        print(obj.name);
        infoPanel.SetActive(true);
        img.sprite = obj.GetComponent<TileManager>()._sprite;
        nameText.text = obj.GetComponent<TileManager>()._name;
        descText.text = obj.GetComponent<TileManager>()._desc;
        typeText.text = obj.GetComponent<TileManager>()._type;
        priceText.text = obj.GetComponent<TileManager>()._price.ToString();
        _alreadyBought = obj.GetComponent<TileManager>()._alreadyBought;
        if (_alreadyBought)
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
}
