using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField] GameObject pauseMenu; 
    [SerializeField] GameObject letterPanel;
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");

    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
        Debug.Log("Start Game");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
        Debug.Log("Back to star menu");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else 
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        isGamePaused = false;
    }

    public void CloseLetter()
    {
        letterPanel.SetActive(false);
        Time.timeScale = 1.0f;
        isGamePaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }
}
