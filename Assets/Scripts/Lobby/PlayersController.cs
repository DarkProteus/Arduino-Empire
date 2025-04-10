using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayersController : MonoBehaviour
{
    [FormerlySerializedAs("_pickText")] [SerializeField] private TMP_Text pickText;
    [FormerlySerializedAs("_playersText")] [SerializeField] private TMP_Text[] playersText;
    
    public static string player1Name;
    public static string player2Name;

    private int _counter = 0;

    private void Awake()
    {
        pickText.gameObject.SetActive(false);
        GetComponent<PickCharController>().enabled = false;
        foreach(var pText in playersText)
            pText.transform.parent.gameObject.SetActive(false);
    }

    public void StartPicking(Players player)
    {
        StartCoroutine(ChangeTextFlow(player));
    }
    public void PickPlayer(string charName)
    {
            playersText[_counter].transform.parent.gameObject.SetActive(true);
            playersText[_counter].text = $"Гравець {_counter + 1} обрав {charName}";
            PlayerPrefs.SetString($"PLAYER{_counter + 1}", charName);
            _counter++;
            StartPicking(Players.PLAYER2);
    }
    private IEnumerator ChangeTextFlow(Players player)
    {
        yield return new WaitForSeconds(1f);
        if (_counter == 0)
        {
            pickText.gameObject.SetActive(true);
            pickText.text = $"Оберіть персонажа за якого будете грати";
            
            yield return new WaitForSeconds(.3f);
            GetComponent<PickCharController>().enabled = true;
        }

        pickText.text = $"Гравець {(int)player+1} обирає персонажа";
        if(_counter == 2)
        {
            GameObject.Find("CharsSelector").SetActive(false);
            StartCoroutine(GamePreparing());
        }
    }
    private IEnumerator GamePreparing()
    {
        var c = 3;
        while (c > 0)
        {
            pickText.text = $"Гравців обрано, гра почнеться через {c}";
            yield return new WaitForSeconds(1f);
            c--;
        }
        SceneManager.LoadScene(1);
    }
}
