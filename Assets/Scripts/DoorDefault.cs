using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDefault : Door
{
    Animator defaultDoorAnim;

    private void Awake()
    {
        isOpened = false;
        defaultDoorAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
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
        defaultDoorAnim.SetInteger("Open", 2);
    }
}
