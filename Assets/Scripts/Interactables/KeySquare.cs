﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySquare : Key
{
    public float keyPosition;
    Animator squareKeyAnim;
    Vector2 originPos;
    public SpriteRenderer sprite;
    bool isWithChar = false;
    GameObject character;
    public Animator effectAnim;


    private void Awake()
    {
        squareKeyAnim = GetComponentInChildren<Animator>();
        originPos = transform.position;
        sprite = GetComponentInChildren<SpriteRenderer>();
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
            if (isActivated && CharactersMovement.isInputAllowed)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    GetKey();

                }
            }
        }
        if (Door.isAllOpen)
        {
            sprite.enabled = false;
        }

    }

    private void GetKey()
    {
        if (character.GetComponent<Character>().isHavingRoundKey == false
                && character.GetComponent<Character>().isHavingTriangleKey == false
                && character.GetComponent<Character>().isHavingSquareKey == false
                && character.GetComponent<Character>().isHavingDiamondKey == false)
        {
            isWithChar = true;
            squareKeyAnim.SetInteger("State", 2);
            effectAnim.SetTrigger("EffectTrigger");
            character.GetComponentInChildren<Animator>().SetTrigger("Joy");
            if (character.GetComponentInChildren<Animator>().GetInteger("Direction") < 3)
            {
                character.GetComponentInChildren<Animator>().SetInteger("Direction", 3);
            }


            gameObject.transform.SetParent(character.transform);
            //currently position is controlled by animation
            //gameObject.transform.position = new Vector2(originPos.x, originPos.y + keyPosition); //keyPosition not working properly. shifting position with animation
            character.GetComponent<Character>().isHavingSquareKey = true;
            isActivated = false;
        }


    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        if (isActivated && CharactersMovement.isInputAllowed)
        {
            GetKey();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            ShowInteractionUI();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Character")
        {
            isActivated = true;
            squareKeyAnim.SetInteger("State", 1);
            //sprite.gameObject.transform.position = new Vector2(originPos.x, originPos.y + keyPosition);
            character = other.gameObject;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            if (!isWithChar)
            {
                isActivated = false;
                squareKeyAnim.SetInteger("State", 0);
                sprite.gameObject.transform.position = originPos;
            }
            HideInteractionUI();

        }
    }

}
