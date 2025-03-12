using UnityEngine;
using UnityEngine.SceneManagement;
public class WelcomeMenu : MonoBehaviour
{void Start()
    {
        // Lock and hide the cursor for first-person-style control
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
     public void StartGame()
{SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);}
}
