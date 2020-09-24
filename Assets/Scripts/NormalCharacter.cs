using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCharacter : Character
{
    private void Awake()
    {
        Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(nextPos);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFunc();

    }

    public void Initialize()
    {
        grid = FindObjectOfType<Grid>();
        gridX = grid.cellSize.x / 2;
        gridY = grid.cellSize.y / 2;

        currPos = transform.position;
        nextPos = transform.position;
        tempNextCharPos = transform.position;
        nextCharPos = transform.position;
        cm = FindObjectOfType<CharactersMovement>();

        characterAnim = GetComponentInChildren<Animator>();
        characterAnim.SetInteger("Idle", 1);
        characterAnim.SetInteger("StageClear", 0);
        characterAnim.SetInteger("Direction", 3);

        SetCurrentBlock();
    }

    public override bool NWMovement()
    {
        ResetBlockColor();
        if (!isUnitMoveAllowed)
        {
            return false;
        }
        characterAnim.SetInteger("Direction", 1);

        nextPos = new Vector2(currPos.x - gridX, currPos.y + gridY);
        RaycastHit2D hit = Physics2D.Raycast(nextPos, transform.forward, 100, rayLayerMask);
        if (hit)
        {
            nextCharPos = hit.collider.gameObject.GetComponent<BlockStat>().blockCharPos;
            
        }

        
        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
            return false;
        }

        SetCurrentBlock();
        characterAnim.Play("Walk_NW");
        characterAnim.SetInteger("Idle", 0);
        return true;
    }

    public override bool NEMovement()
    {
        ResetBlockColor();
        if (!isUnitMoveAllowed)
        {
            return false;
        }
        characterAnim.SetInteger("Direction", 2);
        nextPos = new Vector2(currPos.x + gridX, currPos.y + gridY);
        RaycastHit2D hit = Physics2D.Raycast(nextPos, transform.forward, 100, rayLayerMask);
        if (hit)
        {
            nextCharPos = hit.collider.gameObject.GetComponent<BlockStat>().blockCharPos;
            
        }
        
        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
            return false;
        }

        SetCurrentBlock();
        characterAnim.Play("Walk_NE");
        characterAnim.SetInteger("Idle", 0);
        return true;
    }

    public override bool SWMovement()
    {
        ResetBlockColor();
        if (!isUnitMoveAllowed)
        {
            return false;
        }
        characterAnim.SetInteger("Direction", 3);
        nextPos = new Vector2(currPos.x - gridX, currPos.y - gridY);
        RaycastHit2D hit = Physics2D.Raycast(nextPos, transform.forward, 100, rayLayerMask);
        if (hit)
        {
            nextCharPos = hit.collider.gameObject.GetComponent<BlockStat>().blockCharPos;
            
        }
        
        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
            return false;
        }

        SetCurrentBlock();
        characterAnim.SetInteger("Idle", 0);
        characterAnim.Play("Walk_SW");

        return true;
    }

    public override bool SEMovement()
    {
        ResetBlockColor();
        if (!isUnitMoveAllowed)
        {
            return false;
        }
        characterAnim.SetInteger("Direction", 4);
        nextPos = new Vector2(currPos.x + gridX, currPos.y - gridY);
        RaycastHit2D hit = Physics2D.Raycast(nextPos, transform.forward, 100, rayLayerMask);
        if (hit)
        {
            nextCharPos = hit.collider.gameObject.GetComponent<BlockStat>().blockCharPos;
            
        }
        

        if (!Physics2D.OverlapCircle(nextPos, 0.1f, accessible))
        {
            nextPos = currPos;
            return false;
        }

        SetCurrentBlock();
        characterAnim.SetInteger("Idle", 0);
        characterAnim.Play("Walk_SE");

        return true;
    }

    public override bool isCharCanMoveNW()
    {
        return base.isCharCanMoveNW();
    }

    public override bool isCharCanMoveNE()
    {
        return base.isCharCanMoveNE();
    }

    public override bool isCharCanMoveSW()
    {
        return base.isCharCanMoveSW();
    }

    public override bool isCharCanMoveSE()
    {
        return base.isCharCanMoveSE();
    }




    //mouse click path finding ai
    /*public void PathFinding()
    {
        if (Input.GetMouseButton(0))
        {
            Initialize();
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, transform.forward);
            Debug.DrawRay(worldPoint, transform.forward * 10, Color.red, 0.3f);
            if (hit)
            {
                endX = hit.collider.gameObject.GetComponent<BlockStat>().transform.position.x;
                endY = hit.collider.gameObject.GetComponent<BlockStat>().transform.position.y;


                startX = fl.charPosX;
                startY = fl.charPosY;

                Run();

            }
            else
                return;
        }
    }
    void Run()
    {
        run = true;
        SetSteps();
        SetPath();
        run = false;
        foreach (GameObject obj in fl.gridArray)
        {
            if (path.Contains(obj) && obj != path[path.Count - 1])
            {
                obj.GetComponent<BlockStat>().currentBlock = 2;
            }
            else if (obj == fl.gridArray[fl.charPosX, fl.charPosY])
            {
                obj.GetComponent<BlockStat>().currentBlock = 1;
            }
            else
            {
                obj.GetComponent<BlockStat>().currentBlock = 0;
            }
        }

    }*/
}
