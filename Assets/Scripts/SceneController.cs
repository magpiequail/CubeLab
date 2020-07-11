using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    Running,
    Paused,
    Died,
    GameOver
}

public class SceneController : MonoBehaviour
{
    public static GameState gameState = GameState.Running;
    public GameObject gameOver;
    GameObject gameOverUI;
    public float delayTillGameOver = 0.5f;
    public float delayTillUI = 2.0f;
    bool isGameOver = false;
    GameObject pauseUI;

    bool isAudioPlayed = false;

    public static AudioManager audioManager;

    private void Awake()
    {
        gameOver = GameObject.FindGameObjectWithTag("Game Over");
        gameOverUI = gameOver.GetComponentInChildren<Button>().transform.parent.gameObject;
        
        gameState = GameState.Running;
        pauseUI = GameObject.FindGameObjectWithTag("Pause");

        audioManager = FindObjectOfType<AudioManager>();

        
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
        gameOver.SetActive(false);
        pauseUI.SetActive(false);
        CharactersMovement.isInputAllowed = true;
        FindObjectOfType<AudioManager>().PlayAudio("StageBgm");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.GameOver && !isGameOver )
        {
            StartCoroutine(GameOver());
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.Running)
        {
            gameState = GameState.Paused;
            pauseUI.SetActive(true);
        }
        else if(gameState == GameState.Paused)
        {
            Time.timeScale = 0f;
            CharactersMovement.isInputAllowed = false;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackToGame();
            }
            
        }
        else if(gameState == GameState.Running)
        {
            Time.timeScale = 1f;
        }

        //else if(Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.Paused)
        //{
        //    gameState = GameState.Running;
        //    pauseUI.SetActive(false);
        //}

        
    }
    public void BackToGame()
    {
        pauseUI = GameObject.FindGameObjectWithTag("Pause");
        pauseUI.SetActive(false);
        CharactersMovement.isInputAllowed = true;
        gameState = GameState.Running;
    }
    public void BackToLobby()
    {
        gameState = GameState.Running;
        SceneManager.LoadScene("Lobby");
        CharactersMovement.isInputAllowed = true;
    }
    public void BackToTitle()
    {
        gameState = GameState.Running;
        SceneManager.LoadScene("Title");
        CharactersMovement.isInputAllowed = true;
    }
    public void Restart()
    {
        gameState = GameState.Running;
        Door.isAllOpen = false;
        CharactersMovement.isInputAllowed = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void BackToLevelSelect()
    {
        gameState = GameState.Running;
        CharactersMovement.isInputAllowed = true;
        SceneManager.LoadScene("Stage Select");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        InitializeOptions();
        SceneManager.LoadScene("Intro01");
    }

    IEnumerator GameOver()
    {
        

        yield return new WaitForSeconds(delayTillGameOver);
        if (!isAudioPlayed)
        {
            audioManager.PlayAudio("GameOver");
            isAudioPlayed = true;
        }
        gameOver.SetActive(true);
        yield return new WaitForSeconds(delayTillUI);
        gameOverUI.SetActive(true);
        isGameOver = true;
        
    }
    public void InitializeOptions()
    {
        PlayerPrefs.SetFloat("Volume",1.0f);
    }
}
