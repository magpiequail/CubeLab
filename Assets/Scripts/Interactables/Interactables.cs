using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public GameObject interactionPrefab;
    public GameObject interactionObj;
    public bool isActive;
    public string interactionMsg = "SPACE";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void StartInteraction()
    {

    }
    protected virtual void ShowInteractionUI()
    {
        interactionObj = Instantiate(interactionPrefab, gameObject.transform);
        interactionObj.GetComponent<InteractionButton>().mouseInputString = interactionMsg;
    }
    protected virtual void HideInteractionUI()
    {
        if (GetComponentInChildren<InteractionButton>())
        {
            Destroy(GetComponentInChildren<InteractionButton>().gameObject);
        }
    }
}
