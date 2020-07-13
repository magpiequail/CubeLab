using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//이 스크립트는 해님이의 대사 출력용 스크립트로, 메모리를 보여줄 때는 재생될 메모리 오브젝트에 붙이고,
//스테이지 씬에서는 scene controller에 붙인다.
public enum Narrate
{
    Manual,
    Automatic,
    Both
}

public class Narration : MonoBehaviour
{
    public GameObject subtitle;
    public float typingSpeed;

    [TextArea(3,10)]
    public string[] sentences;
    public float[] forHowLong;
    public string whenSucceeded;
    public string whenFailed;
    int index;
    public Narrate howToConvey;
    float timePassed;
    public GameObject memoryPlaying;
    float endingLineTime = 3f;
    Text subtitleText;
    private int letterIndex;

    bool isThisSceneStage;

    private void Awake()
    {
        subtitleText = subtitle.GetComponentInChildren<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        subtitle.SetActive(true);
        
        if (SceneManager.GetActiveScene().name != "Stage Select" && SceneManager.GetActiveScene().name != "Lobby")
        {
            isThisSceneStage = true;
        }
        else
        {
            isThisSceneStage = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isThisSceneStage && PlayerPrefs.GetInt("" + SceneManager.GetActiveScene().buildIndex+ "stars") != 0)
        {
            subtitle.SetActive(false);
        }

        if (index < sentences.Length)
        {
            subtitleText.text = sentences[index];
            //StartCoroutine(Type());

            if (!isThisSceneStage)
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    SkipNarration();
                }
            }

            if (howToConvey == Narrate.Manual)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    index++;
                }
            }
            else if (howToConvey == Narrate.Automatic)
            {
                timePassed += Time.deltaTime;
                if (timePassed >= forHowLong[index])
                {
                    index++;
                    timePassed = 0f;
                }
            }
            else if (howToConvey == Narrate.Both)
            {
                timePassed += Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Space) || timePassed >= forHowLong[index])
                {
                    index++;
                    timePassed = 0f;
                }
            }
        }

        if (index == sentences.Length)
        {
            subtitle.SetActive(false);
            if (SceneManager.GetActiveScene().name == "Stage Select")
            {
                memoryPlaying.SetActive(false);
                subtitle.SetActive(true);
            }
            index = 0;
        }

        if (isThisSceneStage && PlayerPrefs.GetInt("" + SceneManager.GetActiveScene().buildIndex + "stars") == 0)
        {
            if (Door.isAllOpen && whenSucceeded != "")
            {
                if (timePassed < endingLineTime)
                {
                    subtitle.SetActive(true);
                    subtitleText.text = whenSucceeded;
                    timePassed += Time.deltaTime;
                }
                else
                {
                    subtitle.SetActive(false);
                }
            }

            if (SceneController.gameState == GameState.Died || SceneController.gameState == GameState.GameOver)
            {
               if( whenFailed != "")
                {
                    if (timePassed < endingLineTime)
                    {
                        subtitle.SetActive(true);
                        subtitleText.text = whenFailed;
                        timePassed += Time.deltaTime;
                    }
                    else
                    {
                        subtitle.SetActive(false);
                    }
                }

            }
        }
            
        
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            subtitleText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator Wait(float sec)
    {
        yield return new WaitForSeconds(sec);
    }

    public void SkipNarration()
    {
        index = sentences.Length;
    }
}
