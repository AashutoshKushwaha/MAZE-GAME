using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class DifficultySelector : MonoBehaviour
{
    public Slider difficultySlider; // Reference to the Slider
    public TextMeshProUGUI difficultyText;    // Reference to display slider value
    public string mazeSceneName = "MazeScreen"; // Name of the scene with the maze
    
    private int mazeSize;

    void Start()
    { Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
        // Set default values
        difficultySlider.minValue = 1;
        difficultySlider.maxValue = 20;
        difficultySlider.wholeNumbers = true;
        difficultySlider.value = 10;

        // Update the displayed difficulty size
        UpdateDifficultyText();
        difficultySlider.onValueChanged.AddListener(delegate { UpdateDifficultyText(); });
   
    }

    public void UpdateDifficultyText()
    {
        mazeSize = Mathf.RoundToInt(difficultySlider.value);
        difficultyText.text = "" + mazeSize;
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("MazeSize", mazeSize); // Store maze size for the next scene
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);    // Load the maze scene
    }
}
