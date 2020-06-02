using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCount : MonoBehaviour
{
    Text remainingMoves;
    GameObject floor;

    // Start is called before the first frame update
    void Start()
    {
        
        remainingMoves = GetComponent<Text>();
        remainingMoves.enabled = false ;
        floor = GameObject.Find("Floor");
    }

    // Update is called once per frame
    void Update()
    {
        ////////only for BlockPath script////////
        if (Input.GetMouseButton(0))
        {
            remainingMoves.enabled = true;
        }
        else
        {
            remainingMoves.enabled = false;
        }
        remainingMoves.text = "" + (floor.GetComponent<BlockPath>().path.Count - 1);
    }
}
