using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButton : MonoBehaviour
{
    Button interaction;
    Text buttonText;

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
            buttonText.text = "SPACE";
        }
        if (Options.input == 1)
        {
            interaction.interactable = true;
            buttonText.text = "획득";
        }
    }
}

