using UnityEngine;
using UnityEditor;

public class FloorGenerator : EditorWindow
{
    bool isGenerated = false;

    int rows = 5;
    Vector2 startingGrid = new Vector2(0, 0);
    float scaleX = FindObjectOfType<Grid>().cellSize.x * 0.5f;
    float scaleY = FindObjectOfType<Grid>().cellSize.y * 0.5f;
    GameObject gridPrefab;
    GameObject[,] gridArray;
    GameObject floor;


    [MenuItem("Custom Editor/Floor Generator")]
    public static void ShowWindow()
    {
        GetWindow<FloorGenerator>();
    }

    private void Awake()
    {

    }

    private void OnGUI()
    {
        rows = EditorGUILayout.IntField("Floor Size", rows);
        gridPrefab = EditorGUILayout.ObjectField("Block Prefab", gridPrefab, typeof(GameObject), false) as GameObject;
        floor = EditorGUILayout.ObjectField("Parent Object", floor, typeof(GameObject), true) as GameObject;
        isGenerated = EditorGUILayout.Toggle("isGenerated", isGenerated);

        if (GUILayout.Button("Generate")&& !isGenerated)
        {
            GenerateGrid();
            isGenerated = true;
            
        }
    }

    void GenerateGrid()
    {
        startingGrid = floor.transform.position;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                GameObject obj = Instantiate(gridPrefab, new Vector2(startingGrid.x + scaleX * j, startingGrid.y - scaleY * j), Quaternion.identity);
                obj.transform.SetParent(floor.transform);
                obj.GetComponentInChildren<BlockStat>().x = i;
                obj.GetComponentInChildren<BlockStat>().y = j;
                
            }
            startingGrid = new Vector2(startingGrid.x - scaleX, startingGrid.y + scaleY);
        }
        startingGrid = new Vector2(0, 0);

    }

}


