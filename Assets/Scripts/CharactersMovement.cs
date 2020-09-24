using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class CharactersMovement : MonoBehaviour
{
    Character[] charactersArray;
    Grid grid;

    float keyPressedTime;
    public float inputTriggerTime;
    public float inputWaitTime;

    [Space(10)]

    public Vector3 tileWorldPos;
    public Vector3Int clickedTilePos;
    TilemapColor tmc;

    static public bool isInputAllowed = true;
    Battery b;

    AudioManager audioManager;

    private void Awake()
    {
        charactersArray = FindObjectsOfType<Character>();
        b = FindObjectOfType<Battery>();
        grid = FindObjectOfType<Grid>();
        tmc = FindObjectOfType<TilemapColor>();
        audioManager = FindObjectOfType<AudioManager>();
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
        //else if(!Door.isAllOpen && /*SceneController.gameState != GameState.GameOver && */SceneController.gameState == GameState.Running)
        //{
        //    isInputAllowed = true;
        //}
        if(SceneController.gameState == GameState.Died || SceneController.gameState == GameState.GameOver)
        {
            isInputAllowed = false;
        }
       
        if (isInputAllowed && Battery.movesTillGameover > 0 && Options.input == 0 && !Input.GetKey(KeyCode.Space))
        {
            //W = NE, A = NW, S = SW, D = SE
            if (Input.GetKeyDown(KeyCode.A) )
            {
                tmc.ColorTiles();
                if (isAllCharMovedNW())
                {
                    b.MinusOneMove();
                    audioManager.PlayCharacterFootstep();
                }
            }

            else if (Input.GetKeyDown(KeyCode.S))
            {
                tmc.ColorTiles();
                if (isAllCharMovedSW())
                {
                    b.MinusOneMove();
                    audioManager.PlayCharacterFootstep();
                }
            }

            else if (Input.GetKeyDown(KeyCode.W))
            {
                tmc.ColorTiles();
                if (isAllCharMovedNE())
                {
                    b.MinusOneMove();
                    audioManager.PlayCharacterFootstep();
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                tmc.ColorTiles();
                if (isAllCharMovedSE())
                {
                    b.MinusOneMove();
                    audioManager.PlayCharacterFootstep();
                }
            }
            //while getkey
            if (Input.GetKey(KeyCode.A))
            {
                keyPressedTime += Time.deltaTime;
                if (keyPressedTime > inputTriggerTime)
                {
                    keyPressedTime = inputTriggerTime - inputWaitTime;
                    tmc.ColorTiles();
                    if (isAllCharMovedNW())
                    {
                        b.MinusOneMove();
                        audioManager.PlayCharacterFootstep();
                    }
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                keyPressedTime += Time.deltaTime;
                if (keyPressedTime > inputTriggerTime)
                {
                    keyPressedTime = inputTriggerTime - inputWaitTime;
                    tmc.ColorTiles();
                    if (isAllCharMovedSW())
                    {
                        b.MinusOneMove();
                        audioManager.PlayCharacterFootstep();
                    }
                }
            }
            else if (Input.GetKey(KeyCode.W))
            {
                keyPressedTime += Time.deltaTime;
                if (keyPressedTime > inputTriggerTime)
                {
                    keyPressedTime = inputTriggerTime - inputWaitTime;
                    tmc.ColorTiles();
                    if (isAllCharMovedNE())
                    {
                        b.MinusOneMove();
                        audioManager.PlayCharacterFootstep();
                    }
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                keyPressedTime += Time.deltaTime;
                if (keyPressedTime > inputTriggerTime)
                {
                    keyPressedTime = inputTriggerTime - inputWaitTime;
                    tmc.ColorTiles();
                    if (isAllCharMovedSE())
                    {
                        b.MinusOneMove();
                        audioManager.PlayCharacterFootstep();
                    }
                }
            }
        }
        //mouse
        if (isInputAllowed && Battery.movesTillGameover > 0 && Options.input == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                for( int i= 0;i < charactersArray.Length; i++)
                {
                    charactersArray[i].currentCharPos = grid.WorldToCell(charactersArray[i].gameObject.transform.position);
                    Debug.Log("character" + i + " = " + charactersArray[i].currentCharPos);
                }

                Debug.Log("current Cam = " + CameraManager.currentCam);
                //currentCharPos = grid.WorldToCell(currentChar.transform.position);
                clickedTilePos = grid.WorldToCell(CameraManager.currentCam.ScreenToWorldPoint(Input.mousePosition));
                tileWorldPos = grid.GetCellCenterWorld(clickedTilePos);

                tmc.ColorTiles();

                if (isCheckingNW())
                {
                    for (int i = 0; i < charactersArray.Length; i++)
                    {
                        charactersArray[i].GetComponent<Character>().NWMovement();
                    }
                    b.MinusOneMove();
                    audioManager.PlayCharacterFootstep();
                }
                if (isCheckingNE())
                {
                    for (int i = 0; i < charactersArray.Length; i++)
                    {
                        charactersArray[i].GetComponent<Character>().NEMovement();
                    }
                    b.MinusOneMove();
                    audioManager.PlayCharacterFootstep();
                }
                if (isCheckingSW())
                {
                    for (int i = 0; i < charactersArray.Length; i++)
                    {
                        charactersArray[i].GetComponent<Character>().SWMovement();
                    }
                    b.MinusOneMove();
                    audioManager.PlayCharacterFootstep();
                }
                if (isCheckingSE())
                {
                    for (int i = 0; i < charactersArray.Length; i++)
                    {
                        charactersArray[i].GetComponent<Character>().SEMovement();
                    }
                    b.MinusOneMove();
                    audioManager.PlayCharacterFootstep();
                }

            }
            else if (Input.GetMouseButtonUp(0))
            {

            }
        }
    }


    bool isAllCharMovedSW()
    {
        foreach (Character c in charactersArray)
        {
            c.characterAnim.SetInteger("Direction", 3);
        }
        foreach (Character c in charactersArray)
        {
            
            if (!c.SWMovement())
            {
                /*foreach(Character ch in charactersArray)
                {
                    ch.nextPos = ch.currPos;
                }*/
                return false;
            }
        }
        /*foreach (Character c in charactersArray)
        {
            
            c.nextCharPos = c.tempNextCharPos;

        }*/

            return true;
    }
    bool isAllCharMovedSE()
    {
        foreach (Character c in charactersArray)
        {
            c.characterAnim.SetInteger("Direction", 4);
            
        }
        foreach (Character c in charactersArray)
        {
            if (!c.SEMovement())
            {
                /*foreach (Character ch in charactersArray)
                {
                    ch.nextPos = ch.currPos;
                }*/
                return false;
            }
        }
        /*foreach (Character c in charactersArray)
        {
            
            c.nextCharPos = c.tempNextCharPos;
        }*/
        return true;
    }
    bool isAllCharMovedNW()
    {
        foreach (Character c in charactersArray)
        {
            c.characterAnim.SetInteger("Direction", 1);
        }
        foreach (Character c in charactersArray)
        {
            if (!c.NWMovement())
            {
                /*foreach (Character ch in charactersArray)
                {
                    ch.nextPos = ch.currPos;
                }*/
                return false;
            }
        }
        /*foreach (Character c in charactersArray)
        {
            
            c.nextCharPos = c.tempNextCharPos;
        }*/
        return true;
    }
    bool isAllCharMovedNE()
    {
        foreach (Character c in charactersArray)
        {
            c.characterAnim.SetInteger("Direction", 2);
        }
        foreach (Character c in charactersArray)
        {
            if (!c.NEMovement())
            {
                /*foreach (Character ch in charactersArray)
                {
                    ch.nextPos = ch.currPos;
                }*/
                return false;
            }
        }
        /*foreach (Character c in charactersArray)
        {
            
            c.nextCharPos = c.tempNextCharPos;
        }*/
        return true;
    }

    //for mouse click movement
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
