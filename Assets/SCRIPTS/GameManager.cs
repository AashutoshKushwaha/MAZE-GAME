using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI winText;
        void Start()
    {   Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Ensure the texts are hidden at the start of the game
        if (gameOverText != null) gameOverText.enabled = false;
        if (winText != null) winText.enabled = false;
    }


    public void GameOver()
    {
        gameOverText.enabled = true;
        Debug.Log("Game Over");
        Time.timeScale = 01; // Pause the game
        Invoke ("LoadNextLevel",0.5f);
    }

    public void WinGame()
    {   Debug.Log("Game Won");
        winText.enabled = true;
        Time.timeScale = 01; // Pause the game
        Invoke ("LoadNextLevel",0.5f);
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Resume the game
        Invoke ("LoadNextLevel",0.5f);
         }

void LoadNextLevel()
{SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);}

}
