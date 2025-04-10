using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ModelLoader : MonoBehaviour
{
    public static ModelLoader instance; 
    [FormerlySerializedAs("_stromTrooperModel")] [SerializeField] private GameObject stormTrooperModel;
    [FormerlySerializedAs("_elPrimoModel")][SerializeField] private GameObject elPrimoModel;
    [FormerlySerializedAs("_creeperModel")][SerializeField] private GameObject creeperModel;
    [FormerlySerializedAs("_startGameTile")][SerializeField] private GameObject startGameTile;
    private void Awake()
    {
        instance = this;
    }
    public string LoadModels(string playerName)
    {
        string loadedName = PlayerPrefs.GetString(playerName);
        if (string.IsNullOrEmpty(loadedName)) return "Error";

        switch (loadedName)
        {
            case "Creeper":
                {
                    if (playerName == Players.PLAYER1.ToString()) {
                        var obj = Instantiate(
                            creeperModel,
                            new Vector3(startGameTile.transform.position.x + 0.3f, startGameTile.transform.position.y, startGameTile.transform.position.z),
                            Quaternion.identity
                            );
                        obj.AddComponent<PlayerController>();
                        obj.AddComponent<Rigidbody>();
                        obj.GetComponent<Rigidbody>().useGravity = false;
                        obj.transform.localScale =new Vector3(0.4f, 0.4f, 0.4f);
                        obj.name = playerName;
                        obj.tag = ((int)Players.PLAYER1).ToString();
                    }
                    else if(playerName == Players.PLAYER2.ToString())
                    {
                        var obj = Instantiate(
                            creeperModel,
                            new Vector3(startGameTile.transform.position.x - 0.3f, startGameTile.transform.position.y, startGameTile.transform.position.z),
                            Quaternion.identity
                            );
                        obj.AddComponent<PlayerController>();
                        obj.AddComponent<Rigidbody>();
                        obj.GetComponent<Rigidbody>().useGravity = false;
                        obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                        obj.name = playerName;
                        obj.tag = ((int)Players.PLAYER2).ToString(); ;
                    }

                }
                
                    break;
            case "ElPrimo":
                {
                    if (playerName == Players.PLAYER1.ToString())
                    {
                        var obj = Instantiate(
                            elPrimoModel,
                            new Vector3(startGameTile.transform.position.x + 0.3f, startGameTile.transform.position.y, startGameTile.transform.position.z),
                            Quaternion.identity
                            );
                        obj.AddComponent<PlayerController>();
                        obj.AddComponent<Rigidbody>();
                        obj.GetComponent<Rigidbody>().useGravity = false;
                        obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                        obj.name = playerName;
                        obj.tag = ((int)Players.PLAYER1).ToString();
                    }
                    else if (playerName == Players.PLAYER2.ToString())
                    {
                        var obj = Instantiate(
                            elPrimoModel,
                            new Vector3(startGameTile.transform.position.x - 0.3f, startGameTile.transform.position.y, startGameTile.transform.position.z),
                            Quaternion.identity
                            );
                        obj.AddComponent<PlayerController>();
                        obj.AddComponent<Rigidbody>();
                        obj.GetComponent<Rigidbody>().useGravity = false;
                        obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                        obj.name = playerName;
                        obj.tag = ((int)Players.PLAYER2).ToString();
                    }
                }
                    break;
            case "StromTrooper":
                {
                    if (playerName == Players.PLAYER1.ToString())
                    {
                        var obj = Instantiate(
                            stormTrooperModel,
                            new Vector3(startGameTile.transform.position.x + 0.3f, startGameTile.transform.position.y, startGameTile.transform.position.z),
                            Quaternion.identity
                            );
                        obj.AddComponent<PlayerController>();
                        obj.AddComponent<Rigidbody>();
                        obj.GetComponent<Rigidbody>().useGravity = false;
                        obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                        obj.name = playerName;
                        obj.tag = ((int)Players.PLAYER1).ToString();
                    }
                    else if (playerName == Players.PLAYER2.ToString())
                    {
                        var obj = Instantiate(
                            stormTrooperModel,
                            new Vector3(startGameTile.transform.position.x - 0.3f, startGameTile.transform.position.y, startGameTile.transform.position.z),
                            Quaternion.identity
                            );
                        obj.AddComponent<PlayerController>();
                        obj.AddComponent<Rigidbody>();
                        obj.GetComponent<Rigidbody>().useGravity = false;
                        obj.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
                        obj.name = playerName;
                        obj.tag = ((int)Players.PLAYER2).ToString();
                    }
                }
                    break;
        }
        return playerName;
    }
}
