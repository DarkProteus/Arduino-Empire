using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class PausePanelController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private Button muteButton;
    [SerializeField] private GameObject muteIcon;
    private AudioSource[] _audioSource;
    private bool _isPaused;
    private bool _isMuted;
    private void Start()
    {
        pausePanel.SetActive(_isPaused);
        
        pauseButton.onClick.AddListener(Pause);
        quitGameButton.onClick.AddListener(Application.Quit);
        
        resumeButton.onClick.AddListener(() =>
        {
            _isPaused = !_isPaused;
            pausePanel.SetActive(_isPaused);
        });
        
        muteButton.onClick.AddListener(() =>
        {
            _isMuted = !_isMuted;
            FindObjectsOfType<AudioSource>().ToList().ForEach(a =>
            {
               a.volume = _isMuted ? 0f : .2f;
            });
            muteIcon.SetActive(_isMuted);
        });
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        Pause();
    }

    private void Pause()
    {
        _isPaused = !_isPaused;
        pausePanel.SetActive(_isPaused);
        Time.timeScale = _isPaused ? 0 : 1;
    }
}
