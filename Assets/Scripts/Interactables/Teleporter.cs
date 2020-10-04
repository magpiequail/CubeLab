using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : Interactables
{
    public GameObject otherTele;
    public int floorOrCeiling; //0 = floor 1 = ceiling
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

    public Floor attachedFloor;
    

    private void Awake()
    {
        teleAnim = GetComponentInChildren<Animator>();
        teleArray = FindObjectsOfType<Teleporter>();
        attachedFloor = GetComponentInParent<Floor>();
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //detect characterObj by raycast
                /*RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 100, ~(1 << 10));
                if (hit && hit.collider.tag == "Character")
                {
                    characterObj = hit.collider.gameObject;
                    Debug.Log("characterObj Detected");
                }*/
                if (/*Input.GetKeyDown(KeyCode.Space) && */isActivated && characterObj.GetComponent<Character>().isUnitMoveAllowed && CharactersMovement.isInputAllowed)
                {
                    
                    StartInteraction();

                    
                }
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
                    characterObj.GetComponent<Character>().ResetBlockColor();
                    


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
        if (collision.GetComponentInParent<Floor>())
        {
            
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
            characterObj = collision.gameObject;
            ShowInteractionUI();
        }
    }



    //AnimEvent
    public void CharacterSpriteOn()
    {
        characterObj.GetComponentInChildren<SpriteRenderer>().enabled = true;
        ShowInteractionUI();
    }
    public void CharacterSpriteOff()
    {
        characterObj.GetComponentInChildren<SpriteRenderer>().enabled = false;
        HideInteractionUI();
    }
    public void SendCharacterToOther()
    {
        HideInteractionUI();
        //while the sprite is disabled, flip the spider character 
        if (characterObj.GetComponent<Spider>())
        {
            if (floorOrCeiling - otherTele.GetComponent<Teleporter>().floorOrCeiling != 0)
            {
                characterObj.GetComponent<Spider>().Flip();
            }
        }
        //if a normal character is trying to teleport to ceiling
        if (characterObj.GetComponent<NormalCharacter>())
        {
            if (floorOrCeiling - otherTele.GetComponent<Teleporter>().floorOrCeiling != 0)
            {
                return;
            }
        }
        //
        characterObj.transform.position = otherTele.transform.position;
        characterObj.GetComponent<Character>().currPos = otherTele.transform.position;
        characterObj.GetComponent<Character>().nextPos = otherTele.transform.position;
        characterObj.GetComponent<Character>().nextCharPos = otherTele.transform.position;

        if (otherTele.GetComponent<Teleporter>().attachedFloor.charOnFloor)
        {
            otherTele.GetComponent<Teleporter>().attachedFloor.charOnFloor = characterObj.GetComponent<Character>();

        }
        else if(!otherTele.GetComponent<Teleporter>().attachedFloor.charOnFloor)
        {
            attachedFloor.charOnFloor = null;
            otherTele.GetComponent<Teleporter>().attachedFloor.charOnFloor = characterObj.GetComponent<Character>();
        }
        


    }
    public void PlayReceive()
    {
        otherTele.GetComponentInChildren<Animator>().Play("TeleportReceive");
    }



}

