using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InfoMenuContoller : MonoBehaviour
{
    [SerializeField] private Button _infoButton;
    [SerializeField] private GameObject _infoPanel;

    void Start()
    {
        _infoPanel.gameObject.SetActive(false);
        _infoButton.onClick.AddListener(() =>
        {
            _infoPanel.gameObject.SetActive(true);
        });
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _infoPanel.gameObject.SetActive(false);
    }
}
