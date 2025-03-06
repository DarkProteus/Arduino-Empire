using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLoader : MonoBehaviour
{
    [SerializeField] private GameObject _stromTrooperModel;
    [SerializeField] private GameObject _elPrimoModel;
    [SerializeField] private GameObject _creeperModel;
    private void Start()
    {
        LoadModels();
    }
    public void LoadModels()
    {
        if(PlayerPrefs.GetString("Player1") == "StromTrooper")
        {
            Instantiate(_stromTrooperModel, new Vector3(-1,0,0), Quaternion.identity);
        }
        else if(PlayerPrefs.GetString("Player1") == "ElPrimo")
        {
            Instantiate(_elPrimoModel, new Vector3(-1, 0, 0), Quaternion.identity);
        }
        else if (PlayerPrefs.GetString("Player1") == "Creeper")
        {
            Instantiate(_creeperModel, new Vector3(-1, 0, 0), Quaternion.identity);
        }
        if (PlayerPrefs.GetString("Player2") == "StromTrooper")
        {
            Instantiate(_stromTrooperModel, new Vector3(1, 0, 0), Quaternion.identity);
        }
        else if (PlayerPrefs.GetString("Player2") == "ElPrimo")
        {
            Instantiate(_elPrimoModel, new Vector3(1, 0, 0), Quaternion.identity);
        }
        else if (PlayerPrefs.GetString("Player2") == "Creeper")
        {
            Instantiate(_creeperModel, new Vector3(1, 0, 0), Quaternion.identity);
        }
    }
}
