using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyCharacter : MonoBehaviour
{
    public Animator characterAnim;
    bool isUnitMoveAllowed = true;
    public static bool isInputAllowed = true;


    // public GameObject floor;


    public int rows = 5;

    public float speed;


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

    CharactersMovement cm;

    public LayerMask accessible;
    IntroText it;


    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
        gridX = grid.cellSize.x / 2;
        gridY = grid.cellSize.y / 2;

        currPos = transform.position;
        nextPos = transform.position;

        cm = FindObjectOfType<CharactersMovement>();
        it = FindObjectOfType<IntroText>();

    }

    // Start is called before the first frame update
    void Start()
    {
        characterAnim = GetComponentInChildren<Animator>();
        characterAnim.SetInteger("Idle", 1);
        isInputAllowed = true;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (SceneManager.GetActiveScene().name == "Intro01" && Input.GetKeyDown(KeyCode.Space) && IntroText.state ==2)
        {
            characterAnim.SetTrigger("GetUp");
            it.getUp.enabled = false;
            //it.keysImg.enabled = true;
            characterAnim.SetInteger("Idle", 1);
        }
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

            if (Input.GetKeyDown(KeyCode.S))
            {
                SWMovement();

                characterAnim.SetInteger("Direction", 3);
                characterAnim.Play("Walk");

            }

            else if (Input.GetKeyDown(KeyCode.D))
            {
                SEMovement();

                characterAnim.SetInteger("Direction", 4);
                characterAnim.Play("Walk_SE");

            }

            else if (Input.GetKeyDown(KeyCode.A))
            {
                NWMovement();

                characterAnim.SetInteger("Direction", 1);
                characterAnim.Play("Walk_NW");

            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                NEMovement();

                characterAnim.SetInteger("Direction", 2);
                characterAnim.Play("Walk_NE");

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
        characterAnim.Play("Walk");

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
    }



    public bool isCharCanMoveNW()
    {
        if (cm.clickedTilePos.x == currentCharPos.x && cm.clickedTilePos.y == currentCharPos.y + 1 && Physics2D.OverlapCircle(cm.tileWorldPos, 0.01f, accessible))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isCharCanMoveNE()
    {
        if (cm.clickedTilePos.x == currentCharPos.x + 1 && cm.clickedTilePos.y == currentCharPos.y && Physics2D.OverlapCircle(cm.tileWorldPos, 0.01f, accessible))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isCharCanMoveSW()
    {
        if (cm.clickedTilePos.x == currentCharPos.x - 1 && cm.clickedTilePos.y == currentCharPos.y && Physics2D.OverlapCircle(cm.tileWorldPos, 0.01f, accessible))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isCharCanMoveSE()
    {
        if (cm.clickedTilePos.x == currentCharPos.x && cm.clickedTilePos.y == currentCharPos.y - 1 && Physics2D.OverlapCircle(cm.tileWorldPos, 0.01f, accessible))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
