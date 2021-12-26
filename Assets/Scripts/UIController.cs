using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Button _pauseButton;
    public bool gameIsPaused = false;

    public void UpdateText(int level)
    {
        _text.text = "Level: " + level;
    }

    public void PausePressed()
    {
        _pausePanel.SetActive(true);
        _pauseButton.gameObject.SetActive(false);
        gameIsPaused = true;
    }

    public void ResumePressed()
    {
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
        _pauseButton.gameObject.SetActive(true);
        gameIsPaused = false;
    }

    public void MainMenuPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
