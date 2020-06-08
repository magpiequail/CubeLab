using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    Running,
    Paused,
    GameOver
}

public class SceneController : MonoBehaviour
{
    public static GameState gameState = GameState.Running;
    public GameObject gameOver;
    GameObject gameOverUI;
    public float delayTillGameOver = 0.5f;
    public float delayTillUI = 1.0f;
    bool isGameOver = false;
    GameObject pauseUI;



    private void Awake()
    {
        gameOver = GameObject.FindGameObjectWithTag("Game Over");
        gameOverUI = gameOver.GetComponentInChildren<Button>().transform.parent.gameObject;
        
        gameState = GameState.Running;
        pauseUI = GameObject.FindGameObjectWithTag("Pause");

    }

    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.GameOver && !isGameOver )
        {
            StartCoroutine(GameOver());
        }
        if(Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.Running)
        {
            gameState = GameState.Paused;
            pauseUI.SetActive(true);
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
        gameState = GameState.Running;
    }
    public void BackToLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
    public void Restart()
    {
        Door.isAllOpen = false;
        CharactersMovement.isInputAllowed = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void BackToLevelSelect()
    {
        SceneManager.LoadScene("Stage Select");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Stage Select");
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(delayTillGameOver);
        //
        gameOver.SetActive(true);
        gameOverUI.SetActive(false);
        yield return new WaitForSeconds(delayTillUI);
        gameOverUI.SetActive(true);
        isGameOver = true;
        
    }
}
