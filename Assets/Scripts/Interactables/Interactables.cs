using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public GameObject interactionPrefab;
    public GameObject interactionObj;
    public bool isActivated;
    public string interactionMsg = "SPACE";

    protected GameObject characterObj;
 

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
        interactionObj = Instantiate(interactionPrefab, /*characterObj.transform.position, Quaternion.identity, */gameObject.transform);
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
