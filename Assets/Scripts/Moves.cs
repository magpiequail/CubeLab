using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moves : MonoBehaviour
{
    public static int possibleMoves = 50;
    Text movesText;
    
    // Start is called before the first frame update
    void Start()
    {
        movesText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        movesText.text = ""+possibleMoves;
    }
}
