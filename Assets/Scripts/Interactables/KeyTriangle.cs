using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyTriangle : Key
{
    public float keyPosition;
    Animator triangleKeyAnim;
    bool isWithChar = false;
    Vector2 originPos;
    //public SpriteRenderer sprite;
    //bool isCharOn = false;
    //GameObject character;
    public Animator effectAnim;


    private void Awake()
    {
        triangleKeyAnim = GetComponentInChildren<Animator>();
        originPos = transform.position;
        //sprite = GetComponentInChildren<SpriteRenderer>();
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
            keyMesh.SetActive(false);
        }

    }

    private void GetKey()
    {
        if (characterObj.GetComponent<Character>().isHavingRoundKey == false
                && characterObj.GetComponent<Character>().isHavingTriangleKey == false
                && characterObj.GetComponent<Character>().isHavingSquareKey == false
                && characterObj.GetComponent<Character>().isHavingDiamondKey == false)
        {
            triangleKeyAnim.SetInteger("State", 2);
            effectAnim.SetTrigger("EffectTrigger");

            FindObjectOfType<AudioManager>().PlayAudio("Ingame_elevator");
            if (FindObjectOfType<Expression>())
            {
                Expression.faceAnim.Play("Happy");
            }
            characterObj.GetComponentInChildren<Animator>().SetTrigger("Joy");
            if (characterObj.GetComponentInChildren<Animator>().GetInteger("Direction") < 3)
            {
                characterObj.GetComponentInChildren<Animator>().SetInteger("Direction", 3);
            }

            isWithChar = true;
            gameObject.transform.SetParent(characterObj.transform);
            //currently the position of this key is controlled by animation
            //gameObject.transform.position = new Vector2(originPos.x, originPos.y + keyPosition);
            characterObj.GetComponent<Character>().isHavingTriangleKey = true;
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
            characterObj = collision.gameObject;
            ShowInteractionUI();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Character")
        {
            isActivated = true;
            triangleKeyAnim.SetInteger("State", 1);
            //sprite.gameObject.transform.position = new Vector2(originPos.x, originPos.y + keyPosition);
            characterObj = other.gameObject;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character" /*&& !isWithChar*/)
        {
            if (!isWithChar)
            {
                isActivated = false;
                triangleKeyAnim.SetInteger("State", 0);
                //sprite.gameObject.transform.position = originPos;
            }

            HideInteractionUI();
        }

    }
   /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            isCharOn = true;
            triangleKeyAnim.SetInteger("State", 1);
            //sprite.gameObject.transform.position = new Vector2(originPos.x, originPos.y + keyPosition);
            character = collision.gameObject;
            GetKey();

        }
    }*/
}
