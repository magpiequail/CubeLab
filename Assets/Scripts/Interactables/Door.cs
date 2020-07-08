﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door :Interactables
{
    public float delayTillDoorOpen = 0.5f;
    public float delayTillStageClear=2.0f;
    public float delayTillNextStage = 2.5f;

    public bool isOpened = true;
    protected bool isHavingKey;
    private GameObject text;
    Door[] doorsArray;
    public static bool isAllOpen = false;

    bool isAudioPlayed = false;

    Animator stars;

    private void Awake()
    {
        text = GameObject.FindGameObjectWithTag("Stage Clear");
        stars = GameObject.FindGameObjectWithTag("Stars").GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        stars.gameObject.SetActive(false);
        text.SetActive(false);
        doorsArray = FindObjectsOfType<Door>();
        isAllOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAllDoorsOpen() == true && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Open());
        }
            
        if (isAllOpen)
        {

        }
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        if(IsAllDoorsOpen() == true)
        {
            StartCoroutine(Open());
            Debug.Log("start interaction called");
        }
        
    }

    private bool IsAllDoorsOpen()
    {
        for (int i = 0; i < doorsArray.Length; ++i)
        {
            if (doorsArray[i].isOpened == false)
            {
                isActivated = false;
                return false;
            }
        }
        if (doorsArray.Length == 0)
        {
            
            return false;
        }
        isActivated = true;
        return true;
    }

    protected virtual void PlayOpenAnim()
    {

    }

    
    IEnumerator Open()
    {

        isAllOpen = true;

        if (!isAudioPlayed)
        {
            FindObjectOfType<AudioManager>().PlayAudio("Lobby_incu_steam");
            isAudioPlayed = true;
        }

        //yield return new WaitForSeconds(0);

        //foreach not working all the time
        //for not working all the time
        /*for(int i = 0;i<doorsArray.Length;i ++)
        {
            doorsArray[i].PlayOpenAnim();
        }*/


        yield return new WaitForSeconds(delayTillStageClear);
        FindObjectOfType<AudioManager>().PlayAudio("UI_change");
        text.SetActive(true);
        ShowStars();

        yield return new WaitForSeconds(delayTillNextStage);


        Rate();
        isAllOpen = false;
        if (SceneManager.GetActiveScene().buildIndex == 25)
        {
            SceneManager.LoadScene("Stage Select");
        }
        CharactersMovement.isInputAllowed = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void Rate()
    {
        if(PlayerPrefs.GetInt(""+ SceneManager.GetActiveScene().buildIndex+"stars") < Battery.stars)
        {
            PlayerPrefs.SetInt("" + SceneManager.GetActiveScene().buildIndex + "stars", Battery.stars);
        }
        
    }
    public void ShowStars()
    {
        stars.gameObject.SetActive(true);
        stars.SetInteger("Stars", Battery.stars);
    }
}
