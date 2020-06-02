using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadStage1()
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
    }
}
