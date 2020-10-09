using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroText : MonoBehaviour
{
    public string[] sentences;
    Text currentText;
    public static int state = 0;
    public GameObject timeline;
    Image subtitleImg;
    public GameObject optionButton;
    

    bool showedInputOption = false;
    
    

    IntroCharacter introChar;

    public int index = 0;
    public float delayBetweenLine = 2.0f;

    private void Awake()
    {
        currentText = GetComponent<Text>();
        timeline.SetActive(false);
        subtitleImg = GetComponentInParent<Image>();
        optionButton.SetActive(false);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        IntroCharacter.isInputAllowed = false;
        introChar = FindObjectOfType<IntroCharacter>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(index < sentences.Length && Time.timeScale !=0f)
        {

            if (Input.GetKeyDown(KeyCode.Space)/* && state == 0 && index < 7*/)
            {
                if(state == 0 && index < 7)
                {
                    index++;
                }
                else if(state == 2 && index < sentences.Length - 1)
                {
                    index++;
                    if (index == sentences.Length - 1)
                    {
                        optionButton.SetActive(true);
                        showedInputOption = true;
                    }
                }

                
            }
            /*else if (index == 7)
            {

                subtitleImg.enabled = false;
                timeline.SetActive(true);
                StartCoroutine(StateTwo(0.5f));
                //subtitleImg.enabled = true;
                
            }*/

            else if (Input.GetKeyDown(KeyCode.Tab))
            {
                if(index < 7)
                {
                    index = 7;
                }
                else if(index > 7 && !showedInputOption)
                {
                    index = sentences.Length - 1;
                    introChar.TriggerGetUp();
                    //IntroCharacter.isInputAllowed = true;
                    optionButton.SetActive(true);
                    showedInputOption = true;
                }

            }
            
        }
        if (index > sentences.Length)
        {
            
        }
        if (index == 7)
        {

            subtitleImg.enabled = false;
            timeline.SetActive(true);
            StartCoroutine(StateTwo(0.5f));
            //subtitleImg.enabled = true;

        }
        /*if (index == sentences.Length - 1)
        {
            IntroCharacter.isInputAllowed = true;
        }
        else
        {
            IntroCharacter.isInputAllowed = false;
        }*/
        /*else if (state == 2)     
        {
            subtitleImg.enabled = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                index++;
            }
        }*/


        currentText.text = sentences[index];
        
    }

    public void SkipIntro()
    {

    }

    
    IEnumerator StateTwo(float sec)
    {
        yield return new WaitForSeconds(sec);
        state = 2;
        index = 8;
        subtitleImg.enabled = true;
    }
}
