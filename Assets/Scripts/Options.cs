using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class Options : MonoBehaviour
{
    public static int input; //0 = keyboard, 1 = mouse
    ToggleGroup inputToggleGroup;
    //Dropdown d;
    InteractionButton interaction;
    Toggle[] toggleArray;

    Slider volumeSilder;

    public Image speakerImage;
    public Sprite soundOn;
    public Sprite soundOff;
    public GameObject initialSelection;

    public Toggle currentOption
    {
        get { return inputToggleGroup.ActiveToggles().FirstOrDefault(); }
    }

    private void Awake()
    {
        //d = GetComponentInChildren<Dropdown>();
        interaction = FindObjectOfType<InteractionButton>();
        inputToggleGroup = GetComponentInChildren<ToggleGroup>();
        toggleArray = inputToggleGroup.GetComponentsInChildren<Toggle>();

        volumeSilder = GetComponentInChildren<Slider>();

    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(initialSelection);
    }

    // Start is called before the first frame update
    void Start()
    {
        //this was used for dropdown 
        /*d.onValueChanged.AddListener(delegate
        {
            ChangeInput(d);
        });
        if(PlayerPrefs.GetInt("OptionValue") == 0)
        {
            d.value = 0;
            input = 0;
        }
        else if(PlayerPrefs.GetInt("OptionValue") == 1)
        {
            d.value = 1;
            input = 1;
        }*/
        ChangeInputToggle(PlayerPrefs.GetInt("OptionValue"));

        if(PlayerPrefs.GetInt("VolumeSaved") !=1)
        {
            volumeSilder.value = 0.5f;
        }
        else if (PlayerPrefs.GetInt("VolumeSaved") == 1)
        {
            volumeSilder.value = PlayerPrefs.GetFloat("Volume");
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = volumeSilder.value;
        if(volumeSilder.value == 0)
        {
            speakerImage.sprite = soundOff;
        }
        else
        {
            speakerImage.sprite = soundOn;
        }
    }

    public void ChangeInputToggle(int i)
    {
        toggleArray[i].isOn = true;
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
        PlayerPrefs.SetInt("OptionValue", input);
    }

    public void ChangeToKeyboard()
    {
        input = 0;
        PlayerPrefs.SetInt("OptionValue", input);
        Debug.Log(input);
    }
    public void ChangeToMouse()
    {
        input = 1;
        PlayerPrefs.SetInt("OptionValue", input);
        Debug.Log(input);
    }


    public void GetCurrentInputOption()
    {
        if (PlayerPrefs.GetInt("OptionValue") == 0)
        {
            input = 0;
        }
        else if(PlayerPrefs.GetInt("OptionValue") == 1)
        {
            input = 1;
        }
    }
    public void SaveCurrentOption()
    {
        PlayerPrefs.SetFloat("Volume", volumeSilder.value);
        PlayerPrefs.SetInt("VolumeSaved", 1);
        PlayerPrefs.SetInt("SavedOption", 1);
    }
    private void OnDisable()
    {
        SaveCurrentOption();
    }
}
