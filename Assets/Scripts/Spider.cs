using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Character
{
    private void Awake()
    {
        Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (!isUnitMoveAllowed)
        {
            return false;
        }
        ResetBlockColor();
        nextPos = new Vector2(currPos.x + gridX, currPos.y - gridY);
        characterAnim.SetInteger("Direction", 1);
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
        if (!isUnitMoveAllowed)
        {
            return false;
        }
        ResetBlockColor();
        nextPos = new Vector2(currPos.x - gridX, currPos.y - gridY);
        characterAnim.SetInteger("Direction", 2);
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
        if (!isUnitMoveAllowed)
        {
            return false;
        }
        ResetBlockColor();
        nextPos = new Vector2(currPos.x + gridX, currPos.y + gridY);
        characterAnim.SetInteger("Direction", 3);
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
        if (!isUnitMoveAllowed)
        {
            return false;
        }
        ResetBlockColor();
        nextPos = new Vector2(currPos.x - gridX, currPos.y + gridY);
        characterAnim.SetInteger("Direction", 4);

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

    
}
