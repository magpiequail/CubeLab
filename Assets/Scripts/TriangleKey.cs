using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleKey : MonoBehaviour
{
    public float keyPosition;
    Animator triangleKeyAnim;
    bool isWithChar = false;
    Vector2 originPos;
    SpriteRenderer sprite;
    bool isCharOn = false;
    GameObject character;

    private void Awake()
    {
        triangleKeyAnim = GetComponentInChildren<Animator>();
        originPos = transform.position;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(sprite.gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharOn)
        {
            if (Input.GetKeyDown(KeyCode.Space) && character.GetComponent<CharacterMovement>().isHavingRoundKey == false
                 && character.GetComponent<CharacterMovement>().isHavingTriangleKey == false)
            {
                triangleKeyAnim.SetInteger("State", 2);
                isWithChar = true;
                gameObject.transform.SetParent(character.transform);
                gameObject.transform.position = new Vector2(originPos.x, originPos.y + keyPosition);
                character.GetComponent<CharacterMovement>().isHavingTriangleKey = true;
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Character")
        {
            isCharOn = true;
            triangleKeyAnim.SetInteger("State", 1);
            //sprite.gameObject.transform.position = new Vector2(originPos.x, originPos.y + keyPosition);
            character = other.transform.parent.gameObject;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character" && !isWithChar)
        {
            isCharOn = false;
            triangleKeyAnim.SetInteger("State", 0);
            sprite.gameObject.transform.position = originPos;
        }
    }
}
