using System.Collections;
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
        doorsArray = GameObject.FindObjectsOfType<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAllDoorsOpen() == true && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Open());
        }
            
        if (IsAllDoorsOpen())
        {

        }
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        if(IsAllDoorsOpen() == true)
        {
            StartCoroutine(Open());
        }
        
    }

    private bool IsAllDoorsOpen()
    {
        for (int i = 0; i < doorsArray.Length; ++i)
        {
            if (doorsArray[i].isOpened == false)
            {
                return false;
            }
        }
        if (doorsArray.Length == 0)
        {
            return false;
        }
        return true;
    }

    protected virtual void PlayOpenAnim()
    {

    }

    
    IEnumerator Open()
    {

        isAllOpen = true;



        yield return new WaitForSeconds(0);
        foreach (Door doors in doorsArray)
        {
            doors.PlayOpenAnim();
        }


        yield return new WaitForSeconds(delayTillStageClear);
        text.SetActive(true);
        ShowStars();

        yield return new WaitForSeconds(delayTillNextStage);


        Rate();
        isAllOpen = false;
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            SceneManager.LoadScene("Lobby");
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
