using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyCharacter : MonoBehaviour
{
    public Animator characterAnim;
    //bool isUnitMoveAllowed = true;
    public static bool isInputAllowed = true;

    AudioManager audioManager;

    // public GameObject floor;


    public int rows = 5;

    public float speed;

    float inputWaitTime = 0.3f;
    float keyPressedTime;

    //public Floor fl;

    //public int charPosX = 2;
    //public int charPosY = 2;

    public Vector2 nextPos;
    Grid grid;
    float gridX;
    float gridY;
    public Vector2 currPos;
    public Vector3Int currentCharPos;
    Vector3Int clickedTilePos;
    Camera mainCam;

    public LayerMask accessible;
    public Vector3 tileWorldPos;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        gridX = grid.cellSize.x / 2;
        gridY = grid.cellSize.y / 2;

        currPos = transform.position;
        nextPos = transform.position;

        mainCam = Camera.main;
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        characterAnim = GetComponentInChildren<Animator>();
        characterAnim.SetInteger("Idle", 1);
        isInputAllowed = true;

        if (PlayerPrefs.GetInt("OptionValue") == 0)
        {
            Options.input = 0;
        }
        else if (PlayerPrefs.GetInt("OptionValue") == 1)
        {
            Options.input = 1;
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (isInputAllowed)
        {
            if (Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
            }
            if (Vector3.Distance(transform.position, nextPos) < 0.01f)
            {
                currPos = nextPos;
                characterAnim.SetInteger("Idle", 1);
            }


            if(Options.input == 0)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    keyPressedTime = 0f;
                    SWMovement();
                    audioManager.PlayCharacterFootstep();
                }

                else if (Input.GetKeyDown(KeyCode.D))
                {
                    keyPressedTime = 0f;
                    SEMovement();
                    audioManager.PlayCharacterFootstep();
                }

                else if (Input.GetKeyDown(KeyCode.A))
                {
                    keyPressedTime = 0f;
                    NWMovement();
                    audioManager.PlayCharacterFootstep();

                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    keyPressedTime = 0f;
                    NEMovement();
                    audioManager.PlayCharacterFootstep();
                }

                if (Input.GetKey(KeyCode.S))
                {
                    SWMovement();
                    keyPressedTime += Time.deltaTime;
                    if(keyPressedTime > inputWaitTime)
                    {
                        keyPressedTime = 0f;
                        audioManager.PlayCharacterFootstep();
                    }
                }

                else if (Input.GetKey(KeyCode.D))
                {
                    SEMovement();
                    keyPressedTime += Time.deltaTime;
                    if (keyPressedTime > inputWaitTime)
                    {
                        keyPressedTime = 0f;
                        audioManager.PlayCharacterFootstep();
                    }
                }

                else if (Input.GetKey(KeyCode.A))
                {
                    NWMovement();
                    keyPressedTime += Time.deltaTime;
                    if (keyPressedTime > inputWaitTime)
                    {
                        keyPressedTime = 0f;
                        audioManager.PlayCharacterFootstep();
                    }
                }
                else if (Input.GetKey(KeyCode.W))
                {
                    NEMovement();
                    keyPressedTime += Time.deltaTime;
                    if (keyPressedTime > inputWaitTime)
                    {
                        keyPressedTime = 0f;
                        audioManager.PlayCharacterFootstep();
                    }
                }

            }
            else if(Options.input == 1)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    currentCharPos = grid.WorldToCell(gameObject.transform.position);

                    clickedTilePos = grid.WorldToCell(mainCam.ScreenToWorldPoint(Input.mousePosition));
                    tileWorldPos = grid.GetCellCenterWorld(clickedTilePos);


                    if (clickedTilePos.x == currentCharPos.x && clickedTilePos.y == currentCharPos.y + 1 && Physics2D.OverlapCircle(tileWorldPos, 0.01f, accessible))
                    {
                        NWMovement();
                        audioManager.PlayCharacterFootstep();
                    }
                    if (clickedTilePos.x == currentCharPos.x + 1 && clickedTilePos.y == currentCharPos.y && Physics2D.OverlapCircle(tileWorldPos, 0.01f, accessible))
                    {
                        NEMovement();
                        audioManager.PlayCharacterFootstep();
                    }
                    if (clickedTilePos.x == currentCharPos.x - 1 && clickedTilePos.y == currentCharPos.y && Physics2D.OverlapCircle(tileWorldPos, 0.01f, accessible))
                    {
                        SWMovement();
                        audioManager.PlayCharacterFootstep();
                    }
                    if (clickedTilePos.x == currentCharPos.x && clickedTilePos.y == currentCharPos.y - 1 && Physics2D.OverlapCircle(tileWorldPos, 0.01f, accessible))
                    {
                        SEMovement();
                        audioManager.PlayCharacterFootstep();
                    }
                }
            }
            

        }



    }

    public void SWMovement()
    {
        nextPos = new Vector2(currPos.x - gridX, currPos.y - gridY);
        characterAnim.SetInteger("Direction", 3);
        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
        }


        characterAnim.SetInteger("Idle", 0);
        characterAnim.Play("Walk_SW");
        //audioManager.PlayCharacterFootstep();
    }
    public void SEMovement()
    {

        nextPos = new Vector2(currPos.x + gridX, currPos.y - gridY);
        characterAnim.SetInteger("Direction", 4);

        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
        }


        characterAnim.SetInteger("Idle", 0);
        characterAnim.Play("Walk_SE");
        //audioManager.PlayCharacterFootstep();
    }
    public void NWMovement()
    {
        nextPos = new Vector2(currPos.x - gridX, currPos.y + gridY);
        characterAnim.SetInteger("Direction", 1);
        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
        }


        characterAnim.Play("Walk_NW");
        characterAnim.SetInteger("Idle", 0);
        //audioManager.PlayCharacterFootstep();
    }

    public void NEMovement()
    {
        nextPos = new Vector2(currPos.x + gridX, currPos.y + gridY);
        characterAnim.SetInteger("Direction", 2);
        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
        }


        characterAnim.Play("Walk_NE");
        characterAnim.SetInteger("Idle", 0);
        //audioManager.PlayCharacterFootstep();
    }


}
