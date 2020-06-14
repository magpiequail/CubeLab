using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDefault : Door
{
    Animator defaultDoorAnim;

    public GameObject interactionPrefab;
    GameObject interactionObj;
    public string interactionMsg = "사용";

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
        defaultDoorAnim.SetInteger("Open", 2);
    }
}
