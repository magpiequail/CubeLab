using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public GameObject pauseUI;
    bool isPauseUIActive = false;

    private void Awake()
    {
        //pauseUI = GameObject.FindGameObjectWithTag("Pause");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPauseUIActive)
            {
                pauseUI.SetActive(true);
                isPauseUIActive = true;
            }
            else if (isPauseUIActive)
            {
                pauseUI.SetActive(false);
                isPauseUIActive = false;
            }

        }
    }

    /*public void LoadStage1()
    {
        SceneManager.LoadScene("Stage01");
    }
    public void LoadStage2()
    {
        SceneManager.LoadScene("Stage02");
    }
    public void LoadStage3()
    {
        SceneManager.LoadScene("Stage03");
    }
    public void LoadStage4()
    {
        SceneManager.LoadScene("Stage04");
    }
    public void LoadStage5()
    {
        SceneManager.LoadScene("Stage05");
    }
    public void LoadStage6()
    {
        SceneManager.LoadScene("Stage06");
    }*/
}
