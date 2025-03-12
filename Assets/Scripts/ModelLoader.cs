using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelLoader : MonoBehaviour
{
    public static ModelLoader instance;
    [SerializeField] private GameObject _stromTrooperModel;
    [SerializeField] private GameObject _elPrimoModel;
    [SerializeField] private GameObject _creeperModel;
    [SerializeField] private GameObject _startGameTile;
    private void Awake()
    {
        instance = this;
    }
    public string LoadModels(string name)
    {
        string loadedName = PlayerPrefs.GetString(name);
        print($"{name},{loadedName}");
        if (string.IsNullOrEmpty(loadedName)) return "Error";

        switch (loadedName)
        {
            case "Creeper":
                {
                    GameObject obj=Instantiate(
                        _creeperModel,
                        _startGameTile.transform.position,
                        Quaternion.identity
                        );

                    obj.AddComponent<PlayerController>();
                    obj.AddComponent<Rigidbody>();
                    obj.GetComponent<Rigidbody>().useGravity = false;
                    obj.name = name;
                    obj.tag = name;
                }
                
                    break;
            case "ElPrimo":
                {
                   GameObject obj= Instantiate(
                       _elPrimoModel,
                       _startGameTile.transform.position,
                       Quaternion.identity
                       );

                    obj.AddComponent<PlayerController>();
                    obj.AddComponent<Rigidbody>();
                    obj.GetComponent<Rigidbody>().useGravity = false;
                    obj.name = name;
                    obj.tag = name;
                }
                    break;
            case "StromTrooper":
                {
                   GameObject obj= Instantiate(
                       _stromTrooperModel,
                       _startGameTile.transform.position,
                       Quaternion.identity
                       );

                    obj.AddComponent<PlayerController>();
                    obj.AddComponent<Rigidbody>();
                    obj.GetComponent<Rigidbody>().useGravity = false;
                    obj.name = name;
                    obj.tag = name;
                }
                    break;
        }
        return name;
    }
}
