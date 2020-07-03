using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSquare : Door
{
    Animator squareDoorAnim;

    /*public GameObject interactionPrefab;
    GameObject interactionObj;
    public string interactionMsg = "사용";*/

    private void Awake()
    {
        isOpened = false;
        squareDoorAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isOpened == true && !isAllOpen)
        {
            squareDoorAnim.SetInteger("Open", 1);
        }
        else
        {
            squareDoorAnim.SetInteger("Open", 0);
        }
        if (isAllOpen)
        {
            squareDoorAnim.SetInteger("Open", 2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character" /*&& collision.GetComponent<Character>().isHavingSquareKey*/)
        {
            if (collision.GetComponent<Character>().isHavingSquareKey)
            {
                isOpened = true;
            }
            ShowInteractionUI();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            isOpened = false;
            HideInteractionUI();
        }
    }

    protected override void PlayOpenAnim()
    {
        base.PlayOpenAnim();
        squareDoorAnim.SetInteger("Open", 2);
    }
}
