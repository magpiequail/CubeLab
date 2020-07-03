using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroDoor : MonoBehaviour
{
    public Sprite closedDoor;
    public Sprite openedDoor;
    public Sprite redLight;
    public Sprite blueLight;

    public float waitTillBlueLight = 1.0f;
    public float waitTillDoorOpen = 1.0f;
    public float waitTillNextScene = 1.0f;
    bool isOpen = false;
    SpriteRenderer lightSprite;

    private void Awake()
    {
        lightSprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {

            StartCoroutine(OpenIntroDoor());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Character")
        {
            isOpen = true;
            collision.GetComponentInParent<IntroCharacter>().characterAnim.Play("Idle_NE");
            if(collision.GetComponentInParent<IntroCharacter>().nextPos == collision.GetComponentInParent<IntroCharacter>().currPos)
            {
                IntroCharacter.isInputAllowed = false;
            }

        }
        
    }
    IEnumerator OpenIntroDoor()
    {

        yield return new WaitForSeconds(waitTillBlueLight);
        /*lightSprite.sprite = blueLight;*/
        IntroCharacter.isInputAllowed = false;
        GetComponent<SpriteRenderer>().sprite = openedDoor;

        yield return new WaitForSeconds(waitTillDoorOpen);
        

        yield return new WaitForSeconds(waitTillNextScene);
        SceneManager.LoadScene("Lobby");

    }
}
