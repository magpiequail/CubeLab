﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRound : Door
{
     Animator roundDoorAnim;

    public GameObject interactionPrefab;
    GameObject interactionObj;
    public string interactionMsg = "사용";

    private void Awake()
    {
        isOpened = false;
        roundDoorAnim =  GetComponent<Animator>();
    }

// Start is called before the first frame update
void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isOpened == true)
        {
            roundDoorAnim.SetInteger("Open", 1);
        }
        else
        {
            roundDoorAnim.SetInteger("Open", 0);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character" /*&& collision.GetComponent<Character>().isHavingRoundKey*/)
        {
            if (collision.GetComponent<Character>().isHavingRoundKey)
            {
                isOpened = true;
            }
            
            interactionObj = Instantiate(interactionPrefab, gameObject.transform);
            interactionObj.GetComponent<InteractionButton>().mouseInputString = interactionMsg;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            isOpened = false;
            if (GetComponentInChildren<InteractionButton>())
            {
                Destroy(GetComponentInChildren<InteractionButton>().gameObject);
            }
        }
    }

    protected override void PlayOpenAnim()
    {
        base.PlayOpenAnim();
        roundDoorAnim.SetInteger("Open", 2);
    }

}
