using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyCapsule : MonoBehaviour
{
    bool isActivated = false;
    GameObject LobbyChar;
    Animator capsuleAnim;
    public int stageNumber;

    private void Awake()
    {
        capsuleAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated && Input.GetKeyDown(KeyCode.Space))
        {
            capsuleAnim.Play("Open_Lobby");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Character")
        {
            isActivated = true;
            LobbyChar =  collision.transform.parent.gameObject;
        }
    }

    public void PlayCharAnim()
    {
        LobbyChar.GetComponentInChildren<Animator>().Play("LieDown");
    }
    public void LoadStageSelect()
    {
        SceneManager.LoadScene("Stage Select");
    }
}
