using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public Laser[] controlledLasers;
    bool isSwitchActive = false;
    SpriteRenderer currentSprite;
    public Sprite sprite1;
    public Sprite sprite2;

    private void Awake()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        currentSprite.sprite = sprite1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSwitchActive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(currentSprite.sprite == sprite1)
                {
                    currentSprite.sprite = sprite2;
                }
                else if(currentSprite.sprite == sprite2)
                {
                    currentSprite.sprite = sprite1;
                }
                foreach (Laser laser in controlledLasers)
                {
                    if (laser.isLaserActive)
                    {
                        laser.isLaserActive = false;
                    }
                    else
                    {
                        laser.isLaserActive = true;
                    }
                }
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isSwitchActive = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isSwitchActive = false;
    }

}
