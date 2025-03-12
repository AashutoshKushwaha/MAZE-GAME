using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NewLevel : MonoBehaviour
{   public string mazeSceneName = "DifficultyLevel"; 
  void Start()
    {
        // Lock and hide the cursor for first-person-style control
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
     public void Quit()
   {  Application.Quit();}
     public void StartGame()
    { 
        SceneManager.LoadScene(mazeSceneName);    // Load the maze scene
    }
}
