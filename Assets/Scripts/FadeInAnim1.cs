using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInAnim1 : MonoBehaviour
{

    float time = 0;

    // Update is called once per frame
    void Update()
    {
        if (time < 3f)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, time/1);
            time += Time.deltaTime;
        }
        else
        {
          
        }
        

    }

    public void resetAnim()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        this.gameObject.SetActive(true);
        time = 0;
    }
}
