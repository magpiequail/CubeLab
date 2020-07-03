﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Elevator : Interactables
{
    public GameObject otherElevator;
    //public GameObject attachedFloor;
    //bool isCharOn;
    //public bool isActivated = false;
    GameObject characterColl;
    SpriteRenderer sprite;

    Animator elevatorAnim; // state 1 is when it is opening, state 0 is when it is closing, open receive = 2, close receive = 3

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        elevatorAnim = GetComponentInChildren<Animator>();
        isActivated = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.S) &&
            !Input.GetKey(KeyCode.D) &&
            !Input.GetKey(KeyCode.W))
        {
            if (Input.GetKeyDown(KeyCode.Space) && isActivated && characterColl.GetComponent<Character>().isUnitMoveAllowed && CharactersMovement.isInputAllowed)
            {
                ////캐릭터가 속한 플로어 바꾸기
                //characterColl.GetComponentInParent<CharacterMovement>().floor = otherTele.GetComponent<Teleporter>().attachedFloor;
                ////캐릭터의 플로어 스크립트 변경
                //characterColl.GetComponentInParent<CharacterMovement>().fl = characterColl.GetComponentInParent<CharacterMovement>().floor.GetComponent<Floor>();
                //캐릭터의 위치 변경

                //이펙트 재생


                StartInteraction();

                //characterColl.GetComponentInParent<Character>().fl.charPosX = otherTele.GetComponent<Teleporter>().posX;
                // characterColl.GetComponentInParent<Character>().fl.charPosY = otherTele.GetComponent<Teleporter>().posY;
            }


        }


    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.tag == "Character")
    //    {
    //        if (isSpacePressed /*&& otherTele.GetComponent<Teleporter>().isCharOn*/)
    //        {
    //            Debug.Log("space pressed");
    //            other.gameObject.GetComponentInParent<CharacterMovement>().floor = otherTele.GetComponent<Teleporter>().attachedFloor;
    //            other.gameObject.transform.parent.gameObject.transform.position = otherTele.transform.position;
    //            other.gameObject.GetComponentInParent<CharacterMovement>().fl = other.gameObject.GetComponentInParent<CharacterMovement>().floor.GetComponent<Floor>();
    //            //Debug.Log("character position = " +other.gameObject.transform.parent.gameObject.transform.position + other.gameObject.transform.parent.gameObject.name);
    //            // Debug.Log("other teleporter = " +otherTele.transform.position + otherTele.gameObject.name);
    //            isSpacePressed = false;
    //        }
    //    }

    //}

    public override void StartInteraction()
    {
        base.StartInteraction();
        if (isActivated && characterColl.GetComponent<Character>().isUnitMoveAllowed && CharactersMovement.isInputAllowed)
        {
            if (sprite.transform.position.x < characterColl.transform.position.x)
            {
                characterColl.GetComponent<Character>().characterAnim.SetInteger("Direction", 1);
            }
            else
            {
                characterColl.GetComponent<Character>().characterAnim.SetInteger("Direction", 2);
            }
            elevatorAnim.SetInteger("State", 1);
            //characterColl.transform.position = otherElevator.transform.position;
            //characterColl.GetComponent<Character>().currPos = otherElevator.transform.position;
            //characterColl.GetComponent<Character>().nextPos = otherElevator.transform.position;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            characterColl = collision.gameObject;
            isActivated = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            isActivated = false;
            HideInteractionUI();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character" && collision.GetComponentInChildren<SpriteRenderer>().enabled)
        {
            ShowInteractionUI();
        }
    }



    //AnimEvent
    public void CharacterSpriteOn()
    {
        characterColl.GetComponentInChildren<SpriteRenderer>().enabled = true;
        if (characterColl.GetComponentInChildren<Key>())
        {
            characterColl.GetComponentInChildren<Key>().gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }

    }
    public void CharacterSpriteOff()
    {
        characterColl.GetComponentInChildren<SpriteRenderer>().enabled = false;
        if (characterColl.GetComponentInChildren<Key>())
        {
            characterColl.GetComponentInChildren<Key>().gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }

    }
    public void SendCharacterToOther()
    {
        characterColl.transform.position = otherElevator.transform.position;
        characterColl.GetComponent<Character>().currPos = otherElevator.transform.position;
        characterColl.GetComponent<Character>().nextPos = otherElevator.transform.position;
        if (sprite.transform.position.x < characterColl.transform.position.x)
        {
            characterColl.GetComponent<Character>().characterAnim.SetInteger("Direction", 4);
        }
        else
        {
            characterColl.GetComponent<Character>().characterAnim.SetInteger("Direction", 3);
        }
    }
    public void PlayOpenReceive()
    {
        otherElevator.GetComponentInChildren<Animator>().SetInteger("State",2);
    }
    public void PlayCloseReceive()
    {
        elevatorAnim.SetInteger("State", 3);
    }
    public void CloseElevator()
    {
        elevatorAnim.SetInteger("State", 0);
        
    }

}