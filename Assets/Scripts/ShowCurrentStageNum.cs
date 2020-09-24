using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShowCurrentStageNum : MonoBehaviour
{
    Text numberText;

    private void Awake()
    {
        numberText = GetComponent<Text>();
        numberText.text = ""+SceneManager.GetActiveScene().buildIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
