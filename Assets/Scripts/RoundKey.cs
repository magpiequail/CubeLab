using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundKey : MonoBehaviour
{
    public float keyPosition;
    Animator roundKeyAnim;
    Vector2 originPos;
    public SpriteRenderer sprite;
    bool isWithChar = false;
    bool isCharOn=false;
    GameObject character;
    public Animator effectAnim;

    public GameObject interactionPrefab;
    GameObject interactionObj;
    public string interactionMsg = "획득";

    private void Awake()
    {
        roundKeyAnim = GetComponentInChildren<Animator>();
        originPos = transform.position;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharOn)
        {
            if (Input.GetKeyDown(KeyCode.Space) && character.GetComponent<Character>().isHavingTriangleKey == false
                && character.GetComponent<Character>().isHavingRoundKey == false)
            {
                GetKey();

            }
        }
        if (Door.isAllOpen)
        {
            sprite.enabled = false;
        }

    }

    private void GetKey()
    {
        if (character.GetComponent<Character>().isHavingTriangleKey == false
                && character.GetComponent<Character>().isHavingRoundKey == false)
        {
            isWithChar = true;
            roundKeyAnim.SetInteger("State", 2);
            effectAnim.SetTrigger("EffectTrigger");
            character.GetComponentInChildren<Animator>().SetTrigger("Joy");
            if (character.GetComponentInChildren<Animator>().GetInteger("Direction") < 3)
            {
                character.GetComponentInChildren<Animator>().SetInteger("Direction", 3);
            }


            gameObject.transform.SetParent(character.transform);
            gameObject.transform.position = new Vector2(originPos.x, originPos.y + keyPosition); //keyPosition not working properly. shifting position with animation
            character.GetComponent<Character>().isHavingRoundKey = true;
            isCharOn = false;
        }
            
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            interactionObj = Instantiate(interactionPrefab, gameObject.transform);
            interactionObj.GetComponent<InteractionButton>().mouseInputString = interactionMsg;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Character")
        {
            isCharOn = true;
            roundKeyAnim.SetInteger("State", 1);
            //sprite.gameObject.transform.position = new Vector2(originPos.x, originPos.y + keyPosition);
            character = other.gameObject;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character" )
        {
            if (!isWithChar)
            {
                isCharOn = false;
                roundKeyAnim.SetInteger("State", 0);
                sprite.gameObject.transform.position = originPos;
            }
            if (GetComponentInChildren<InteractionButton>())
            {
                Destroy(GetComponentInChildren<InteractionButton>().gameObject);
            }

        }
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Character")
        {
            isCharOn = true;
            roundKeyAnim.SetInteger("State", 1);
            //sprite.gameObject.transform.position = new Vector2(originPos.x, originPos.y + keyPosition);
            character = collision.gameObject;

            GetKey();
        }
    }*/

}
