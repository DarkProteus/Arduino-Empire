using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _playGame;
    [SerializeField] private Button _quitGame;
    [SerializeField] private GameObject _charSelectorObj;
    [SerializeField] private GameObject _menuCanvas;
    private float _duration = 1f;
    private float _targetZ = 0f;
    private float _startZ = 10f;
    private Vector3 _startScale = new Vector3(.6f, .6f, .6f);
    private Vector3 _targetScale = new Vector3(1f, 1f, 1f);

    private void Awake()
    {
        _playGame.onClick.AddListener(PlayGame);
        _quitGame.onClick.AddListener(QuitGame);
        _charSelectorObj.SetActive(false);
        PlayerPrefs.SetString("PLAYER1", "none");
        PlayerPrefs.SetString("PLAYER2", "none");
    }
    
    private void PlayGame()
    {
        GetComponent<PlayersController>().StartPicking(Players.PLAYER1);
        _menuCanvas.SetActive(false);

        _charSelectorObj.SetActive(true);

        _charSelectorObj.transform.position = new Vector3(
            _charSelectorObj.transform.position.x,
            _charSelectorObj.transform.position.y,
            _startZ);

        _charSelectorObj.transform.localScale = _startScale;

        Sequence sequence = DOTween.Sequence();

        sequence.Join(
            _charSelectorObj.transform.DOMoveZ(_targetZ, _duration)
            .SetEase(Ease.InOutQuad)
            );

        sequence.Join(
            _charSelectorObj.transform.DOScale(_targetScale, _duration)
            .SetEase(Ease.InOutQuad)
            );


        sequence.Play();
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
