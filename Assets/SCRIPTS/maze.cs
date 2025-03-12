using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int rows = 10;               // Number of rows (must be odd)
    public int columns = 10;            // Number of columns (must be odd)
    public float cubeSize = 1.0f;       // Size of each wall cube
    public float passageWidth = 1.0f;   // Width of passages
    public Material wallMaterial;       // Material for walls
    public Texture wallTexture;         // Texture for walls
    public Material groundMaterial;     // Material for ground
    public GameObject obstaclePrefab;
    private int[,] mazeGrid;            // Internal representation of the maze

    void Start()
    {// Read the maze size from PlayerPrefs (default to 21 if not set)
            int mazeSize = PlayerPrefs.GetInt("MazeSize", 10);
            mazeSize = mazeSize*4;
             rows = mazeSize;
             columns = mazeSize;
        if (rows % 2 == 0) rows++;      // Ensure rows are odd
        if (columns % 2 == 0) columns++; // Ensure columns are odd

        GenerateMaze();
        BuildGround(); // Build the ground
        BuildMaze();   // Build the maze walls
    }

    void GenerateMaze()
    {
        mazeGrid = new int[rows, columns];
        
        // Initialize grid with walls (1 = wall, 0 = path)
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                mazeGrid[i, j] = 1; // Mark all cells as walls
            }
        }

        // Start DFS from (1,1)
        CarvePath(1, 1);
        
        // Create entry and exit
        mazeGrid[0, 1] = 0;           // Entry point
        mazeGrid[rows - 1, columns - 2] = 0; // Exit point
    }

    void CarvePath(int x, int y)
    {
        mazeGrid[x, y] = 0; // Mark current cell as path

        // Randomized directions
        List<Vector2Int> directions = new List<Vector2Int>
        {
            new Vector2Int(0, 2),   // Down
            new Vector2Int(2, 0),   // Right
            new Vector2Int(0, -2),  // Up
            new Vector2Int(-2, 0)   // Left
        };
        Shuffle(directions);

        foreach (var dir in directions)
        {
            int nx = x + dir.x;
            int ny = y + dir.y;

            // Check bounds and if the target cell is a wall
            if (nx > 0 && ny > 0 && nx < rows && ny < columns && mazeGrid[nx, ny] == 1)
            {
                // Remove wall between current cell and next cell
                mazeGrid[x + dir.x / 2, y + dir.y / 2] = 0;
                
                // Recursive call
                CarvePath(nx, ny);
            }
        }
    }

    void BuildMaze()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (mazeGrid[i, j] == 1) // Build walls for cells marked as walls
                {
                    GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    wall.transform.position = new Vector3(i * (cubeSize + passageWidth), 0.5f, j * (cubeSize + passageWidth));  // Adjust the y position to ensure walls are above ground
                    wall.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

                    // Apply material and texture
                    if (wallMaterial != null)
                    {
                        wall.GetComponent<Renderer>().material = wallMaterial;
                        if (wallTexture != null)
                        {
                            wall.GetComponent<Renderer>().material.mainTexture = wallTexture;
                        }
                    }

                    wall.transform.parent = transform; // Group walls under the maze object
                }
                else if (Random.value < 0.2f && !(i == 0 && j == 1) && !(i == rows - 2 && j == columns - 2)) // 10% chance for obstacle
            {
                if (obstaclePrefab != null)
                {
                    GameObject obstacle = Instantiate(obstaclePrefab);
                    obstacle.transform.position = new Vector3(i * (cubeSize + passageWidth),-0.04f, j * (cubeSize + passageWidth));
                    obstacle.transform.localScale = new Vector3(cubeSize, 0.1f, cubeSize);
                }
                else
                {
                    Debug.LogWarning("Obstacle prefab is not assigned!");
                }
            }
            }
        }
    }

    // Function to build the ground beneath the maze
    void BuildGround()
    {
        // Create a large ground plane based on the maze dimensions
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        
        // Adjust the size of the ground to cover the maze area
        ground.transform.position = new Vector3((columns - 1) * (cubeSize + passageWidth) / 2, 0f, (rows - 1) * (cubeSize + passageWidth) / 2);
        ground.transform.localScale = new Vector3(columns / 10f, 1, rows / 10f); // Adjust scale based on the maze size
        
        // Apply ground material
        if (groundMaterial != null)
        {
            ground.GetComponent<Renderer>().material = groundMaterial;
        }

        ground.transform.parent = transform; // Group ground under the maze object
    }

    void Shuffle(List<Vector2Int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Vector2Int temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
