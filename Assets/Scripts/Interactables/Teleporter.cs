﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : Interactables
{
    public GameObject otherTele;
    //public GameObject attachedFloor;
    //bool isCharOn;
    //public bool isActivated = false;
    //GameObject characterColl;
    public int posX;
    public int posY;
    public Animator teleAnim;
    Teleporter[] teleArray;

    //public GameObject interactionPrefab;
    //GameObject interactionObj;
    //public string interactionMsg = "사용";

    private void Awake()
    {
        teleAnim = GetComponentInChildren<Animator>();
        teleArray = FindObjectsOfType<Teleporter>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if(!Input.GetKey(KeyCode.A)&&
            !Input.GetKey(KeyCode.S) &&
            !Input.GetKey(KeyCode.D) &&
            !Input.GetKey(KeyCode.W))
        {
            if (Input.GetKeyDown(KeyCode.Space) && isActivated && characterObj.GetComponent<Character>().isUnitMoveAllowed && CharactersMovement.isInputAllowed)
            {
                ////캐릭터가 속한 플로어 바꾸기
                //characterColl.GetComponentInParent<CharacterMovement>().floor = otherTele.GetComponent<Teleporter>().attachedFloor;
                ////캐릭터의 플로어 스크립트 변경
                //characterColl.GetComponentInParent<CharacterMovement>().fl = characterColl.GetComponentInParent<CharacterMovement>().floor.GetComponent<Floor>();
                //캐릭터의 위치 변경

                //이펙트 재생

                //teleAnim.Play("TeleportSend");
                StartInteraction();

                /*characterColl.transform.parent.gameObject.transform.position = otherTele.transform.position;
                characterColl.GetComponentInParent<Character>().currPos = otherTele.transform.position;
                characterColl.GetComponentInParent<Character>().nextPos = otherTele.transform.position;*/

                //characterColl.GetComponentInParent<Character>().fl.charPosX = otherTele.GetComponent<Teleporter>().posX;
                // characterColl.GetComponentInParent<Character>().fl.charPosY = otherTele.GetComponent<Teleporter>().posY;
            }
        }

    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        if(characterObj.GetComponent<Character>().isUnitMoveAllowed && CharactersMovement.isInputAllowed)
        {
            for (int i = 0; i < teleArray.Length; i++)
            {
                if (teleArray[i].isActivated)
                {
                    teleAnim.Play("TeleportSend");
                    FindObjectOfType<AudioManager>().PlayAudio("Lobby_incu_open");
                }
            }
        }

        //otherTele.GetComponent<Teleporter>().teleAnim.Play("TeleportSend");
    }

    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Character")
        {
            characterObj = collision.gameObject;
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
        if (collision.tag == "Character")
        {
            ShowInteractionUI();
        }
    }



    //AnimEvent
    public void CharacterSpriteOn()
    {
        characterObj.GetComponentInChildren<SpriteRenderer>().enabled = true;
        Debug.Log("spirte on called");
    }
    public void CharacterSpriteOff()
    {
        characterObj.GetComponentInChildren<SpriteRenderer>().enabled = false;
        Debug.Log("sprite off called");
    }
    public void SendCharacterToOther()
    {
        characterObj.transform.position = otherTele.transform.position;
        characterObj.GetComponent<Character>().currPos = otherTele.transform.position;
        characterObj.GetComponent<Character>().nextPos = otherTele.transform.position;

    }
    public void PlayReceive()
    {
        otherTele.GetComponentInChildren<Animator>().Play("TeleportReceive");
    }

}

