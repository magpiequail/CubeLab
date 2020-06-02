using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriangle : Door
{
     Animator triDoorAnim;

    private void Awake()
    {
        isOpened = false;
        triDoorAnim = GetComponent<Animator>();
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
            triDoorAnim.SetInteger("Open", 1);
        }
        else
        {
            triDoorAnim.SetInteger("Open", 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character" && collision.GetComponentInParent<CharacterMovement>().isHavingTriangleKey)
        {
            isOpened = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            isOpened = false;

        }
    }

    protected override void PlayOpenAnim()
    {
        base.PlayOpenAnim();
        triDoorAnim.SetInteger("Open", 2);
    }
}
