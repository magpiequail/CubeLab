using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public bool isLaserActive = true;
    //public Sprite onSprite;
    //public Sprite offSprite;
    //SpriteRenderer laserSprite;
    PolygonCollider2D laserCollider;
    Animator laserAnim;

    private void Awake()
    {
        //laserSprite = GetComponent<SpriteRenderer>();
        laserCollider = GetComponentInChildren<PolygonCollider2D>();
        laserAnim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isLaserActive) 
        {
            laserAnim.SetInteger("LaserOn",1);
            laserCollider.enabled = true;
        }
        else 
        {
            laserAnim.SetInteger("LaserOn", 0);
            laserCollider.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!isLaserActive)
        {
            laserSprite.sprite = offSprite;
            laserCollider.enabled = false;
        }
        else
        {
            laserSprite.sprite = onSprite;
            laserCollider.enabled = true;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLaserActive == true)
        {
            if (collision.CompareTag("Character"))
            {
                collision.transform.parent.GetComponentInChildren<Animator>().Play("Electrocuted");
                //SceneController.gameState = GameState.GameOver;
            }
        }
        
    }
    public void LaserActivation()
    {
        if (isLaserActive) //set laser as deactivated
        {
            isLaserActive = false;
            laserAnim.SetInteger("LaserOn", 0);
            laserCollider.enabled = false;
        }
        else // set laser as activated
        {
            isLaserActive = true;
            laserAnim.SetInteger("LaserOn", 1);
            laserCollider.enabled = true;
        }
    }
    public void DeactivateLaser()
    {

    }
}
