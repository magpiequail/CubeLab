using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersMovement : MonoBehaviour
{
    Character[] charactersArray;
    //public int remainingMoves = 50;

    static public bool isInputAllowed = true;
    Battery b;

    private void Awake()
    {
        charactersArray = FindObjectsOfType<Character>();
        b = FindObjectOfType<Battery>();
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

        if (isInputAllowed && Battery.movesTillGameover >0)
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
}
