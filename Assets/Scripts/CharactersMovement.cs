using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersMovement : MonoBehaviour
{
    Character[] charactersArray;
    Grid grid;
    public Vector3 tileWorldPos;
    public Vector3Int clickedTilePos;

    static public bool isInputAllowed = true;
    Battery b;

    float halfScreen;
    public Camera leftCam;
    public Camera rightCam;
    public static Camera currentCam;

    private void Awake()
    {
        charactersArray = FindObjectsOfType<Character>();
        b = FindObjectOfType<Battery>();
        grid = FindObjectOfType<Grid>();
        halfScreen = Screen.width * 0.5f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Door.isAllOpen)
        {
            isInputAllowed = false;

        }
        else
        {
            isInputAllowed = true;
        }

        if (isInputAllowed && Battery.movesTillGameover > 0 && Options.input == 0)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (isAllCharMovedNW())
                {
                    b.MinusOneMove();
                }
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (isAllCharMovedSW())
                {
                    b.MinusOneMove();
                }
            }

            else if (Input.GetKeyDown(KeyCode.W))
            {
                if (isAllCharMovedNE())
                {
                    b.MinusOneMove();
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (isAllCharMovedSE())
                {
                    b.MinusOneMove();
                }
            }
        }
        //mouse
        if (isInputAllowed && Battery.movesTillGameover > 0 && Options.input == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.mousePosition.x < halfScreen)
                {
                    currentCam = leftCam;
                }
                else
                {
                    currentCam = rightCam;
                }
                for( int i= 0;i < charactersArray.Length; i++)
                {
                    charactersArray[i].currentCharPos = grid.WorldToCell(charactersArray[i].gameObject.transform.position);
                    Debug.Log("character" + i + " = " + charactersArray[i].currentCharPos);
                }

                Debug.Log("current Cam = " + currentCam);
                //currentCharPos = grid.WorldToCell(currentChar.transform.position);
                clickedTilePos = grid.WorldToCell(currentCam.ScreenToWorldPoint(Input.mousePosition));
                tileWorldPos = grid.GetCellCenterWorld(clickedTilePos);

                if (isCheckingNW())
                {
                    for (int i = 0; i < charactersArray.Length; i++)
                    {
                        charactersArray[i].GetComponent<Character>().NWMovement();
                    }
                    b.MinusOneMove();
                }
                if (isCheckingNE())
                {
                    for (int i = 0; i < charactersArray.Length; i++)
                    {
                        charactersArray[i].GetComponent<Character>().NEMovement();
                    }
                    b.MinusOneMove();
                }
                if (isCheckingSW())
                {
                    for (int i = 0; i < charactersArray.Length; i++)
                    {
                        charactersArray[i].GetComponent<Character>().SWMovement();
                    }
                    b.MinusOneMove();
                }
                if (isCheckingSE())
                {
                    for (int i = 0; i < charactersArray.Length; i++)
                    {
                        charactersArray[i].GetComponent<Character>().SEMovement();
                    }
                    b.MinusOneMove();
                }

                //Debug.Log("clickedTilePos = " + clickedTilePos + "currentCharPos = " + currentCharPos);
            }
        }
    }

    bool isAllCharMovedSW()
    {
        foreach (Character c in charactersArray)
        {
            if (!c.SWMovement())
            {
                return false;
            }
        }
        return true;
    }
    bool isAllCharMovedSE()
    {
        foreach (Character c in charactersArray)
        {
            if (!c.SEMovement())
            {
                return false;
            }
        }
        return true;
    }
    bool isAllCharMovedNW()
    {
        foreach (Character c in charactersArray)
        {
            if (!c.NWMovement())
            {
                return false;
            }
        }
        return true;
    }
    bool isAllCharMovedNE()
    {
        foreach (Character c in charactersArray)
        {
            if (!c.NEMovement())
            {
                return false;
            }
        }
        return true;
    }

    ////////////////////
    bool isCheckingNW()
    {
        foreach (Character c in charactersArray)
        {
            if (c.isCharCanMoveNW())
            {
                return true;
            }
           
        }
        return false;
    }
    bool isCheckingNE()
    {
        foreach (Character c in charactersArray)
        {
            if (c.isCharCanMoveNE())
            {
                return true;
            }

        }
        return false;
    }
    bool isCheckingSW()
    {
        foreach (Character c in charactersArray)
        {
            if (c.isCharCanMoveSW())
            {
                return true;
            }

        }
        return false;
    }
    bool isCheckingSE()
    {
        foreach (Character c in charactersArray)
        {
            if (c.isCharCanMoveSE())
            {
                return true;
            }

        }
        return false;
    }
}
