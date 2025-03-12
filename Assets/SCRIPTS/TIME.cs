using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{   
    private float timeRemaining;
    public TextMeshProUGUI timerText; // Change to TextMeshProUGUI

    void Start()
    {  int mazeSize = PlayerPrefs.GetInt("MazeSize",5);
        timeRemaining=mazeSize*60;
        
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            FindAnyObjectByType<GameManager>().GameOver();
        }
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
