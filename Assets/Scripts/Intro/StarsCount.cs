using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsCount : MonoBehaviour
{
    LevelSelectButton[] levelsArray;
    public int totalStars;
    public Slider starsCollected;
    public GameObject[] memories;
    public int[] starsToUnlock;

    private void Awake()
    {
        levelsArray = FindObjectsOfType<LevelSelectButton>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < levelsArray.Length; i++)
        {
            totalStars += levelsArray[i].GetStar();
        }
        starsCollected.value = totalStars;
        for(int i = 0; i < memories.Length; i++)
        {
            //able to unlock matching memory
            if(starsToUnlock[i] <= totalStars)
            {
                memories[i].GetComponentInChildren<Button>().interactable = true;
            }
            //unable to unlock the memory
            else
            {
                memories[i].GetComponentInChildren<Button>().interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
