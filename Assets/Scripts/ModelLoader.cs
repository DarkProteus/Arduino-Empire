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
        if (string.IsNullOrEmpty(loadedName)) return "Error";

        switch (loadedName)
        {
            case "Creeper":
                {
                    if (name == Players.PLAYER1.ToString()) {
                        GameObject obj = Instantiate(
                            _creeperModel,
                            new Vector3(_startGameTile.transform.position.x + 0.3f, _startGameTile.transform.position.y, _startGameTile.transform.position.z),
                            Quaternion.identity
                            );
                        obj.AddComponent<PlayerController>();
                        obj.AddComponent<Rigidbody>();
                        obj.GetComponent<Rigidbody>().useGravity = false;
                        obj.transform.localScale =new Vector3(0.4f, 0.4f, 0.4f);
                        obj.name = name;
                        obj.tag = ((int)Players.PLAYER1).ToString(); ;
                    }
                    else if(name == Players.PLAYER2.ToString())
                    {
                        GameObject obj = Instantiate(
                            _creeperModel,
                            new Vector3(_startGameTile.transform.position.x - 0.3f, _startGameTile.transform.position.y, _startGameTile.transform.position.z),
                            Quaternion.identity
                            );
                        obj.AddComponent<PlayerController>();
                        obj.AddComponent<Rigidbody>();
                        obj.GetComponent<Rigidbody>().useGravity = false;
                        obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                        obj.name = name;
                        obj.tag = ((int)Players.PLAYER2).ToString(); ;
                    }

                }
                
                    break;
            case "ElPrimo":
                {
                    if (name == Players.PLAYER1.ToString())
                    {
                        GameObject obj = Instantiate(
                            _elPrimoModel,
                            new Vector3(_startGameTile.transform.position.x + 0.3f, _startGameTile.transform.position.y, _startGameTile.transform.position.z),
                            Quaternion.identity
                            );
                        obj.AddComponent<PlayerController>();
                        obj.AddComponent<Rigidbody>();
                        obj.GetComponent<Rigidbody>().useGravity = false;
                        obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                        obj.name = name;
                        obj.tag = ((int)Players.PLAYER1).ToString(); ;
                    }
                    else if (name == Players.PLAYER2.ToString())
                    {
                        GameObject obj = Instantiate(
                            _elPrimoModel,
                            new Vector3(_startGameTile.transform.position.x - 0.3f, _startGameTile.transform.position.y, _startGameTile.transform.position.z),
                            Quaternion.identity
                            );
                        obj.AddComponent<PlayerController>();
                        obj.AddComponent<Rigidbody>();
                        obj.GetComponent<Rigidbody>().useGravity = false;
                        obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                        obj.name = name;
                        obj.tag = ((int)Players.PLAYER2).ToString(); ;
                    }
                }
                    break;
            case "StromTrooper":
                {
                    if (name == Players.PLAYER1.ToString())
                    {
                        GameObject obj = Instantiate(
                            _stromTrooperModel,
                            new Vector3(_startGameTile.transform.position.x + 0.3f, _startGameTile.transform.position.y, _startGameTile.transform.position.z),
                            Quaternion.identity
                            );
                        obj.AddComponent<PlayerController>();
                        obj.AddComponent<Rigidbody>();
                        obj.GetComponent<Rigidbody>().useGravity = false;
                        obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                        obj.name = name;
                        obj.tag = ((int)Players.PLAYER1).ToString(); ;
                    }
                    else if (name == Players.PLAYER2.ToString())
                    {
                        GameObject obj = Instantiate(
                            _stromTrooperModel,
                            new Vector3(_startGameTile.transform.position.x - 0.3f, _startGameTile.transform.position.y, _startGameTile.transform.position.z),
                            Quaternion.identity
                            );
                        obj.AddComponent<PlayerController>();
                        obj.AddComponent<Rigidbody>();
                        obj.GetComponent<Rigidbody>().useGravity = false;
                        obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                        obj.name = name;
                        obj.tag = ((int)Players.PLAYER2).ToString();
                    }
                }
                    break;
        }
        return name;
    }
}
