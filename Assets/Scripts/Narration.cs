﻿using System.Collections;
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
    Both,
    VoiceOver
}
public enum Scene
{
    Intro,
    Lobby,
    Memory,
    Stage
}

public class Narration : MonoBehaviour
{
    public GameObject subtitle;
    public float typingSpeed;

    [TextArea(3,10)]
    public string[] sentences;
    public float[] forHowLong;
    public Sound[] ttsAudio;
    public string whenSucceeded;
    public Sound succeededAudio;
    public string whenFailed;
    public Sound failedAudio;

    int index;
    public Narrate howToConvey;
    public Scene sceneID;
    float timePassed;
    public GameObject memoryPlaying;
    float endingLineTime = 3f;
    Text subtitleText;
    private int letterIndex;

    bool isThisSceneStage;
    bool isEndingTTSPlayed = false;
    float timeTillNextSentence;

    private void Awake()
    {
        subtitleText = subtitle.GetComponentInChildren<Text>();
        InitializeTTS();
        
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        if (sceneID == Scene.Memory)
        {
            index = 0;
            subtitle.SetActive(true);
        }
        
        Debug.Log("index is " + index);
    }
    void Start()
    {

        if(sceneID == Scene.Stage)
        {
            index = PlayerPrefs.GetInt("TTSIndex" + SceneManager.GetActiveScene().buildIndex);
            Debug.Log("index is " + index);
        }
        else
        {
            index = 0;
        }
        subtitle.SetActive(true);
        
        if (SceneManager.GetActiveScene().name != "Stage Select" && SceneManager.GetActiveScene().name != "Lobby")
        {
            isThisSceneStage = true;
        }
        else
        {
            isThisSceneStage = false;
        }
        if(sceneID == Scene.Lobby)
        {
            if(PlayerPrefs.GetInt("LobbyOnce") == 1)
            {
                subtitle.SetActive(false);
            }
            Debug.Log("LobbyOnce = " + PlayerPrefs.GetInt("LobbyOnce"));
        }
        if(sceneID == Scene.Stage && howToConvey == Narrate.VoiceOver)
        {
            if (index < ttsAudio.Length)
            {
                ttsAudio[index].source.Play();
            }
            
        }
        Debug.Log(gameObject.name + " index is " + index);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sceneID == Scene.Stage && PlayerPrefs.GetInt("" + SceneManager.GetActiveScene().buildIndex+ "stars") != 0)
        {
            subtitle.SetActive(false);
        }
        //if(SceneController.gameState == GameState.Died || SceneController.gameState == GameState.GameOver)
        //{
        //    Debug.Log("failedAudio");
        //    if (index < ttsAudio.Length)
        //    {
        //        ttsAudio[index].source.Stop();
        //    }
        //    failedAudio.source.Play();
        //}
        

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
                else if (Input.GetKeyDown(KeyCode.Tab))
                {
                    SkipNarration();
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
            else if(howToConvey == Narrate.VoiceOver)
            {
                if (!ttsAudio[index].source.isPlaying && subtitle.activeSelf && index < ttsAudio.Length && SceneController.gameState == GameState.Running &&Time.timeScale !=0&& !Door.isAllOpen)
                {
                    timeTillNextSentence += Time.deltaTime;
                    if(timeTillNextSentence >= 0.5f)
                    {
                        index++;
                        PlayerPrefs.SetInt("TTSIndex" + SceneManager.GetActiveScene().buildIndex, index);
                        if (index < ttsAudio.Length)
                        {
                            ttsAudio[index].source.Play();
                        }
                        else
                        {
                            PlayerPrefs.SetInt("TTSIndex" + SceneManager.GetActiveScene().buildIndex, index);
                        }
                        timeTillNextSentence = 0f;
                    }
                    

                }
 
            }
        }

        if (index == sentences.Length)
        {
            subtitle.SetActive(false);
            if (sceneID == Scene.Memory)
            {
                memoryPlaying.SetActive(false);
                subtitle.SetActive(true);
                index = 0;
                if(SceneController.gameState == GameState.MemoryPlaying)
                {
                    SceneController.gameState = GameState.Running;
                }

            }
            //
        }

        if (isThisSceneStage && PlayerPrefs.GetInt("" + SceneManager.GetActiveScene().buildIndex + "stars") == 0)
        {
            if (Door.isAllOpen && whenSucceeded != "")
            {
                if (isEndingTTSPlayed == false && howToConvey == Narrate.VoiceOver)
                {
                    if (index < ttsAudio.Length)
                    {
                        ttsAudio[index].source.Stop();
                    }

                    succeededAudio.source.Play();
                    isEndingTTSPlayed = true;
                }
                    
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
                if(isEndingTTSPlayed == false && howToConvey == Narrate.VoiceOver)
                {
                    if (index < ttsAudio.Length)
                    {
                        ttsAudio[index].source.Stop();
                    }
                    failedAudio.source.Play();
                    
                    isEndingTTSPlayed = true;
                }
                
                if (whenFailed != "")
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
    void InitializeTTS()
    {
        if (sceneID == Scene.Stage)
        {
            foreach (Sound s in ttsAudio)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                //s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
            succeededAudio.source = gameObject.AddComponent<AudioSource>();
            succeededAudio.source.clip = succeededAudio.clip;
            succeededAudio.source.volume = succeededAudio.volume;
            //succeededAudio.source.pitch = succeededAudio.pitch;
            succeededAudio.source.loop = succeededAudio.loop;

            failedAudio.source = gameObject.AddComponent<AudioSource>();
            failedAudio.source.clip = failedAudio.clip;
            failedAudio.source.volume = failedAudio.volume;
            //failedAudio.source.pitch = failedAudio.pitch;
            failedAudio.source.loop = failedAudio.loop;
        }
    }
}
