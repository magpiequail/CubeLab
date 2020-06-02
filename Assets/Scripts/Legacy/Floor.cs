using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public int rows = 5;
    //public float scaleX = 0.5f;
    //public float scaleY = -0.25f;
    //public Vector2 startingGrid = new Vector2(0, 0);
    //public GameObject gridPrefab;
    public GameObject[,] gridArray;
    GameObject[] everyBlock;


    public int charPosX = 2;
    public int charPosY = 2;

    private void Awake()
    {
        everyBlock = new GameObject[transform.childCount];
        gridArray = new GameObject[rows, rows];
        int i = 0;
        foreach(Transform child in transform)
        {
            everyBlock[i] = child.gameObject;
            i++;
        }
        foreach(GameObject obj in everyBlock)
        {
            gridArray[obj.GetComponent<BlockStat>().x, obj.GetComponent<BlockStat>().y] = obj;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        InitialSetUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //public void GenerateGrid()
    //{
    //    startingGrid = gameObject.transform.position;
    //    for (int i = 0; i < rows; i++)
    //    {
    //        for (int j = 0; j < rows; j++)
    //        {
    //            GameObject obj = Instantiate(gridPrefab, new Vector2(startingGrid.x + scaleX * j, startingGrid.y + scaleY * j), Quaternion.identity);
    //            obj.transform.SetParent(gameObject.transform);
    //            obj.GetComponent<BlockStat>().x = i;
    //            obj.GetComponent<BlockStat>().y = j;
    //            gridArray[i, j] = obj;
    //        }
    //        startingGrid = new Vector2(startingGrid.x - scaleX, startingGrid.y + scaleY);
    //    }
    //}


    public void SetAsCurrent(int x, int y)
    {
        foreach (GameObject obj in gridArray)
        {
            obj.GetComponent<BlockStat>().currentBlock = 0;
        }
        gridArray[x, y].GetComponent<BlockStat>().currentBlock = 1;
    }
    void InitialSetUp()
    {
        foreach (GameObject obj in gridArray)
        {
            obj.GetComponent<BlockStat>().visited = -1;
        }
       
        //gridArray[charPosX, charPosY].GetComponent<BlockStat>().currentBlock = 1;
    }

    public void SEMovement()
    {
        if (charPosY + 1 < rows && gridArray[charPosX, charPosY + 1]/* && Moves.possibleMoves > 0*/)
        {
            charPosY += 1;
            SetAsCurrent(charPosX, charPosY);
            //Moves.possibleMoves -= 1;
        }

    }
    public void NWMovement()
    {
        if (charPosY - 1 > -1 && gridArray[charPosX, charPosY - 1]/* && Moves.possibleMoves > 0*/)
        {
            charPosY -= 1;
            SetAsCurrent(charPosX, charPosY);
            //Moves.possibleMoves -= 1;
        }
    }
    public void SWMovement()
    {
        if (charPosX + 1 < rows && gridArray[charPosX + 1, charPosY]/* && Moves.possibleMoves > 0*/)
        {
            charPosX += 1;
            SetAsCurrent(charPosX, charPosY);
            //Moves.possibleMoves -= 1;
        }
    }
    public void NEMovement()
    {
        if (charPosX - 1 > -1 && gridArray[charPosX - 1, charPosY]/* && Moves.possibleMoves > 0*/)
        {
            charPosX -= 1;
            SetAsCurrent(charPosX, charPosY);
            //Moves.possibleMoves -= 1;
        }
    }
}
