using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundKey : MonoBehaviour
{
    public float keyPosition;
    Animator roundKeyAnim;
    Vector2 originPos;
    SpriteRenderer sprite;
    bool isWithChar = false;
    bool isCharOn=false;
    GameObject character;

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
            if (Input.GetKeyDown(KeyCode.Space) && character.GetComponent<CharacterMovement>().isHavingTriangleKey == false 
                && character.GetComponent<CharacterMovement>().isHavingRoundKey == false)
            {
                isWithChar = true;
                roundKeyAnim.SetInteger("State", 2);
                gameObject.transform.SetParent(character.transform);
                gameObject.transform.position = new Vector2(originPos.x, originPos.y + keyPosition);
                character.GetComponent<CharacterMovement>().isHavingRoundKey = true;

            }
        }
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Character")
        {
            isCharOn = true;
            roundKeyAnim.SetInteger("State", 1);
            //sprite.gameObject.transform.position = new Vector2(originPos.x, originPos.y + keyPosition);
            character = other.transform.parent.gameObject;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character" && !isWithChar)
        {
            isCharOn = false;
            roundKeyAnim.SetInteger("State", 0);
            sprite.gameObject.transform.position = originPos;
        }
    }

}
