using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public static int input; //0 = keyboard, 1 = mouse
    Dropdown d;
    InteractionButton interaction;

    private void Awake()
    {
        d = GetComponentInChildren<Dropdown>();
        interaction = FindObjectOfType<InteractionButton>();
    }

    // Start is called before the first frame update
    void Start()
    {
        d.onValueChanged.AddListener(delegate
        {
            ChangeInput(d);
        });

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeInput(Dropdown d)
    {
        if(d.value == 0)
        {
            input = 0;
        }
        else if(d.value == 1)
        {
            input = 1;
        }
        if (interaction)
        {
            interaction.ChangeButtonState();
        }
    }
    
    
}
