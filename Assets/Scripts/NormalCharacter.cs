﻿using System.Collections;
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 100, rayLayerMask);
        if (hit && hit.collider.transform.parent.GetComponent<Floor>())
        {
            hit.collider.transform.parent.GetComponent<Floor>().charOnFloor = this;
        }
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

        pf = FindObjectOfType<PathFinding>();

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

  

}