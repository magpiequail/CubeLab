using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryPopup : MonoBehaviour
{
    AudioManager audioManager;
    public float visableForHowLong;
    public GameObject[] memoryPopupPrefab = new GameObject[2];
    int totalLevelNum =32;
    int totalStars;
    public int[] requiredStars;
    GameObject MemoryPopupUI;
    bool isUIDestroyed = false;
    float accuTime = 0f;
    bool canDestroyPopup = true;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < memoryPopupPrefab.Length; i++)
        {
            if (GetAllStars() >= requiredStars[i] && PlayerPrefs.GetInt("isMemoryPopupShowed" + i) != 1)
            {
                audioManager.PlayAudio("UI_change");
                MemoryPopupUI = Instantiate(memoryPopupPrefab[i]);
                PlayerPrefs.SetInt("isMemoryPopupShowed" + i, 1);
                return;
            }
        }
        //MemoryPopupUI = Instantiate(memoryPopupPrefab[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if(isUIDestroyed == false)
        {
            accuTime += Time.deltaTime;
            if (accuTime > visableForHowLong && SceneController.gameState == GameState.Running)
            {
                Destroy(MemoryPopupUI);
                isUIDestroyed = true;
            }
        }

    }

    int GetAllStars()
    {
        for(int i = 1; i <= totalLevelNum; i++)
        {
            totalStars += PlayerPrefs.GetInt("" + i + "stars");
        }
        return totalStars;
    }

    public void ActivateMemoryNarration(GameObject meomoryNarration)
    {
        //canDestroyPopup = false;
        meomoryNarration.SetActive(true);
        SetStateToMem();
        
        Debug.Log("button clicked");
    }
    public void DestroyUI(GameObject popupUI)
    {

        Destroy(popupUI);

    }
    public void SetStateToMem()
    {
        SceneController.gameState = GameState.MemoryPlaying;
    }
    public void SetStateToRun()
    {
        SceneController.gameState = GameState.Running;
    }
}
