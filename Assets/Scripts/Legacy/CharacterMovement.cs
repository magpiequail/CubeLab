using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Animator characterAnim;
    bool isUnitMoveAllowed = true;
    bool isInputAllowed = true;
    public bool isHavingRoundKey = false;
    public bool isHavingTriangleKey = false;
    public GameObject floor;
    float keyPressedTime = 0f;

    public int rows = 5;

    public float speed;
    public float inputTriggerTime;
    public float inputWaitTime;
    
    public Floor fl;
    private Door door;

    //public int charPosX = 2;
    //public int charPosY = 2;

    // Start is called before the first frame update
    void Start()
    {
        fl = floor.GetComponent<Floor>();
        //door = GameObject.FindObjectOfType<Door>();
        characterAnim = GetComponentInChildren<Animator>();
        transform.position = fl.gridArray[fl.charPosX, fl.charPosY].transform.position;

        fl.gridArray[fl.charPosX, fl.charPosY].GetComponent<BlockStat>().currentBlock = 1;
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


        transform.position = Vector3.MoveTowards(transform.position, fl.gridArray[fl.charPosX, fl.charPosY].transform.position, speed * Time.deltaTime);

        Vector3 dist = transform.position - fl.gridArray[fl.charPosX, fl.charPosY].transform.position;
        if (dist.sqrMagnitude > 0f) //move
        {
            characterAnim.SetInteger("Idle", 0);
            //isUnitMoveAllowed = false;

        }
        else //idle
        {
            characterAnim.SetInteger("Idle", 1);
            //isUnitMoveAllowed = true;
        }

        
        //for keyboard inputs

        if (isInputAllowed)
        {
            if (Input.GetKey(KeyCode.A))
            {
                keyPressedTime += Time.deltaTime;
                if (keyPressedTime > inputTriggerTime)
                {
                    keyPressedTime = inputTriggerTime - inputWaitTime;
                    fl.SWMovement();
                    //characterAnim.SetInteger("Idle", 0);
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                keyPressedTime += Time.deltaTime;
                if (keyPressedTime > inputTriggerTime)
                {
                    keyPressedTime = inputTriggerTime - inputWaitTime;
                    fl.SEMovement();
                    //characterAnim.SetInteger("Idle", 0);
                }
            }
            if (Input.GetKey(KeyCode.Q))
            {
                keyPressedTime += Time.deltaTime;
                if (keyPressedTime > inputTriggerTime)
                {
                    keyPressedTime = inputTriggerTime - inputWaitTime;
                    fl.NWMovement();
                    //characterAnim.SetInteger("Idle", 0);
                }
            }
            if (Input.GetKey(KeyCode.W))
            {
                keyPressedTime += Time.deltaTime;
                if (keyPressedTime > inputTriggerTime)
                {
                    keyPressedTime = inputTriggerTime - inputWaitTime;
                    fl.NEMovement();
                    //characterAnim.SetInteger("Idle", 0);
                }
            }
            if (isUnitMoveAllowed)
            {


                if (Input.GetKeyDown(KeyCode.A))
                {
                    fl.SWMovement();
                    //characterAnim.Play("Idle_SW");
                    //characterAnim.SetTrigger("Jump");
                    characterAnim.SetInteger("Direction", 3);
                    characterAnim.Play("Walk");
                    keyPressedTime = 0f;
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    fl.SEMovement();
                    //characterAnim.Play("Idle_SE");
                    //characterAnim.SetTrigger("Jump");
                    characterAnim.SetInteger("Direction", 4);
                    characterAnim.Play("Walk_SE");
                    keyPressedTime = 0f;
                }

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    fl.NWMovement();
                    //characterAnim.Play("Idle_NW");
                    //characterAnim.SetTrigger("Jump");
                    characterAnim.SetInteger("Direction", 1);
                    characterAnim.Play("Walk_NW");
                    keyPressedTime = 0f;
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    fl.NEMovement();
                    //characterAnim.Play("Idle_NE");
                    //characterAnim.SetTrigger("Jump");
                    characterAnim.SetInteger("Direction", 2);
                    characterAnim.Play("Walk_NE");
                    keyPressedTime = 0f;
                }
                if (Input.GetKeyUp(KeyCode.A)
                    || Input.GetKeyUp(KeyCode.S)
                    || Input.GetKeyUp(KeyCode.Q)
                    || Input.GetKeyUp(KeyCode.W))
                {
                    keyPressedTime = 0f;
                }
            }
        }
        
        
        
    }


}
