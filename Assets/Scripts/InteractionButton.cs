using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButton : MonoBehaviour
{
    Button interaction;
    public Text buttonText;
    public string keyInputString = "SPACE";
    public string mouseInputString;

    private void Awake()
    {
        interaction = GetComponentInChildren<Button>();
        buttonText = GetComponentInChildren<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeButtonState();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeButtonState()
    {
        if (Options.input == 0)
        {
            interaction.interactable = false;
            buttonText.text = keyInputString;
        }
        if (Options.input == 1)
        {
            interaction.interactable = true;
            buttonText.text = mouseInputString;
        }
    }
    public void InteractionClick()
    {
        GetComponentInParent<Interactables>().StartInteraction();
        Debug.Log("Button Clicked");
    }
    
}

