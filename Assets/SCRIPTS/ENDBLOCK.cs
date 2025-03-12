using UnityEngine;

public class MazeExit : MonoBehaviour
{
    public string mazeTag = "Maze"; // Tag assigned to the MazeGenerator GameObject

    private void Start()
    {
        // Find the MazeGenerator GameObject by tag
        GameObject mazeObject = GameObject.FindGameObjectWithTag(mazeTag);
        
        if (mazeObject != null)
        {
            // Get the MazeGenerator component
            MazeGenerator mazeGenerator = mazeObject.GetComponent<MazeGenerator>();

            if (mazeGenerator != null)
            {
                // Calculate the exit position based on the maze dimensions
                int rows = mazeGenerator.rows;
                int columns = mazeGenerator.columns;
                if (rows % 2 == 0) rows++;      // Ensure rows are odd
                if (columns % 2 == 0) columns++; // Ensure columns are odd
                transform.position = new Vector3(
                    (rows-1) * (mazeGenerator.cubeSize + mazeGenerator.passageWidth),
                    0.5f, // Slightly above the ground for proper placement
                    (columns - 2) * (mazeGenerator.cubeSize + mazeGenerator.passageWidth)
                );
            }
            else
            {
                Debug.LogError("MazeGenerator component not found on the tagged GameObject.");
            }
        }
        else
        {
            Debug.LogError("No GameObject with the specified mazeTag found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Player"))
        {
            FindAnyObjectByType<GameManager>().WinGame();
        }
    }
}
