using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public bool isLaserActive = true;
    public Sprite onSprite;
    public Sprite offSprite;
    SpriteRenderer laserSprite;
    PolygonCollider2D laserCollider;

    private void Awake()
    {
        laserSprite = GetComponent<SpriteRenderer>();
        laserCollider = GetComponent<PolygonCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLaserActive)
        {
            laserSprite.sprite = offSprite;
            laserCollider.enabled = false;
        }
        else
        {
            laserSprite.sprite = onSprite;
            laserSprite.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLaserActive == true)
        {
            if (collision.CompareTag("Character"))
            {
                SceneController.gameState = GameState.GameOver;
            }
        }
        
    }
}
