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
    public Image keysImg;
    public Image getUp;
    

    public int index = 0;
    public float delayBetweenLine = 2.0f;

    private void Awake()
    {
        currentText = GetComponent<Text>();
        timeline.SetActive(false);
        subtitleImg = GetComponentInParent<Image>();
        keysImg.enabled = false;
        getUp.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        LobbyCharacter.isInputAllowed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(index < sentences.Length)
        {
            LobbyCharacter.isInputAllowed = false;
            if (Input.GetKeyDown(KeyCode.Space) && state == 0 && index < 7)
            {
                index++;
            }
            else if (index == 7)
            {

                subtitleImg.enabled = false;
                timeline.SetActive(true);
                StartCoroutine(StateTwo(0.5f));
                //subtitleImg.enabled = true;
                
            }
            else if (state == 2 && Input.GetKeyDown(KeyCode.Space) && index < sentences.Length -1)
            {
                index++;
            }
            
        }
        if (index > sentences.Length)
        {
            
        }
        if(index == sentences.Length - 1)
        {
            LobbyCharacter.isInputAllowed = true;
            keysImg.enabled = true;
        }
        else
        {
            LobbyCharacter.isInputAllowed = false;
        }
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

    IEnumerator Monologue()
    {
        for(index = 8; index < sentences.Length; index++)
        {
            yield return new WaitForSeconds(delayBetweenLine);

        }
        

    }
    IEnumerator StateTwo(float sec)
    {
        yield return new WaitForSeconds(sec);
        state = 2;
        index = 8;
        getUp.enabled = true;
        subtitleImg.enabled = true;
    }
}
