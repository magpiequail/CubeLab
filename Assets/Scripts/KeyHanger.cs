using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HangerState
{
    Empty,
    RoundKey,
    TriangleKey
    
}

public class KeyHanger : MonoBehaviour
{
    public HangerState hangerState;
    public Sprite emptySprite;
    public Sprite roundKeySprite;
    public Sprite triangleKeySprite;
    public GameObject roundKeyPrefab;
    public GameObject triangleKeyPrefab;


    SpriteRenderer hangerSprite;
    CircleCollider2D hangerCollider;
    bool isRoundKey = false;
    bool isTriangleKey = false;
    GameObject characterColl;
    bool isCharOn = false;



    private void Awake()
    {
        hangerSprite = GetComponent<SpriteRenderer>();
        hangerCollider = GetComponent<CircleCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeHangerSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isCharOn)
        {
            if (hangerState == HangerState.Empty)
            {
                characterColl.GetComponentInParent<Character>().isHavingRoundKey = false;
                characterColl.GetComponentInParent<Character>().isHavingTriangleKey = false;
            }
            else if (hangerState == HangerState.RoundKey)
            {
                characterColl.GetComponentInParent<Character>().isHavingRoundKey = true;
                characterColl.GetComponentInParent<Character>().isHavingTriangleKey = false;
            }
            else if (hangerState == HangerState.TriangleKey)
            {
                characterColl.GetComponentInParent<Character>().isHavingRoundKey = false;
                characterColl.GetComponentInParent<Character>().isHavingTriangleKey = true;
            }
            if (isRoundKey)
            {
                hangerState = HangerState.RoundKey;

            }
            else if(isTriangleKey)
            {
                hangerState = HangerState.TriangleKey;
            }
            else if(!isRoundKey && !isTriangleKey)
            {
                hangerState = HangerState.Empty;
            }

            

            ChangeHangerSprite();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            isCharOn = true;
            characterColl = collision.gameObject;
            if (collision.transform.parent.GetComponent<Character>().isHavingRoundKey == true)
            {
                isTriangleKey = false;
                isRoundKey = true;
            }
            else if (collision.transform.parent.GetComponent<Character>().isHavingTriangleKey == true)
            {
                isRoundKey = false;
                isTriangleKey = true;
            }
            else
            {
                isRoundKey = false;
                isTriangleKey = false;
            }
            

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            isCharOn = false;
        }
    }

    void ChangeHangerSprite()
    {
        if (hangerState == HangerState.Empty)
        {
            hangerSprite.sprite = emptySprite;
        }
        else if (hangerState == HangerState.RoundKey)
        {
            hangerSprite.sprite = roundKeySprite;
        }
        else if (hangerState == HangerState.TriangleKey)
        {
            hangerSprite.sprite = triangleKeySprite;
        }
    }
}
