using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayersController : MonoBehaviour
{
    [SerializeField] private TMP_Text _pickText;
    [SerializeField] private TMP_Text[] _playersText;
    
    public static string player1Name;
    public static string player2Name;

    private int counter = 0;

    private void Awake()
    {
        _pickText.gameObject.SetActive(false);
        GetComponent<PickCharController>().enabled = false;
        foreach(var pText in _playersText)
            pText.gameObject.SetActive(false);
    }

    public void StartPicking(Players player)
    {
        StartCoroutine(ChangeTextFlow(player));
    }
    public void PickPlayer(string charName)
    {
            _playersText[counter].gameObject.SetActive(true);
            _playersText[counter].text = $"Гравець {counter + 1} обрав {charName}";
            PlayerPrefs.SetString($"PLAYER{counter + 1}", charName);
            counter++;
            StartPicking(Players.PLAYER2);
    }
    private IEnumerator ChangeTextFlow(Players player)
    {
        yield return new WaitForSeconds(1f);
        if (counter == 0)
        {
            _pickText.gameObject.SetActive(true);
            _pickText.text = $"Оберіть персонажа за якого будете грати";
            
            yield return new WaitForSeconds(.3f);
            GetComponent<PickCharController>().enabled = true;
        }

        _pickText.text = $"Гравець {(int)player+1} обирає персонажа";
        if(counter == 2)
        {
            GameObject.Find("CharsSelector").SetActive(false);
            StartCoroutine(GamePreparing());
        }
    }
    private IEnumerator GamePreparing()
    {
        int c = 3;
        while (c > 0)
        {
            _pickText.text = $"Гравців обрано, гра почнеться через {c}";
            yield return new WaitForSeconds(.1f);
            c--;
        }
        SceneManager.LoadScene(1);
    }
}
