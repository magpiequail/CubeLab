using UnityEngine;

public enum HangerState
{
    Empty,
    RoundKey,
    TriangleKey
    
}

public class KeyHanger : MonoBehaviour
{
    public HangerState hangerState;
    public Sprite emptySprite;
    public Sprite roundKeySprite;
    public Sprite triangleKeySprite;
    public GameObject roundKeyPrefab;
    public GameObject triangleKeyPrefab;


    SpriteRenderer hangerSprite;
    CircleCollider2D hangerCollider;
    bool isRoundKey = false;
    bool isTriangleKey = false;
    GameObject characterColl;
    bool isCharOn = false;

    public GameObject interactionPrefab;
    GameObject interactionObj;
    public string interactionMsg = "사\r\n용";

    private void Awake()
    {
        hangerSprite = GetComponent<SpriteRenderer>();
        hangerCollider = GetComponent<CircleCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeHangerSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isCharOn)
        {
            
            if (hangerState == HangerState.Empty && isRoundKey && !isTriangleKey)
            {
                characterColl.GetComponent<Character>().isHavingRoundKey = false;
                characterColl.GetComponent<Character>().isHavingTriangleKey = false;
                characterColl.GetComponentInChildren<KeyRound>().keyMesh.SetActive(false);
                hangerState = HangerState.RoundKey;

                isRoundKey = false;
                isTriangleKey = false;
            }
            else if (hangerState == HangerState.Empty && isTriangleKey && !isRoundKey)
            {
                characterColl.GetComponent<Character>().isHavingRoundKey = false;
                characterColl.GetComponent<Character>().isHavingTriangleKey = false;
                characterColl.GetComponentInChildren<KeyTriangle>().keyMesh.SetActive(false);
                hangerState = HangerState.TriangleKey;

                isRoundKey = false;
                isTriangleKey = false;
            }
            else if (hangerState == HangerState.RoundKey && !isRoundKey && !isTriangleKey)
            {
                characterColl.GetComponent<Character>().isHavingRoundKey = true;
                characterColl.GetComponent<Character>().isHavingTriangleKey = false;
                characterColl.GetComponentInChildren<KeyRound>().keyMesh.SetActive( true);
                hangerState = HangerState.Empty;

                isTriangleKey = false;
                isRoundKey = true;
            }
            else if (hangerState == HangerState.RoundKey && !isRoundKey && isTriangleKey)
            {
                characterColl.GetComponent<Character>().isHavingRoundKey = true;
                characterColl.GetComponent<Character>().isHavingTriangleKey = false;
                characterColl.GetComponentInChildren<KeyRound>().keyMesh.SetActive( true);
                characterColl.GetComponentInChildren<KeyTriangle>().keyMesh.SetActive(false);
                hangerState = HangerState.TriangleKey;

                isTriangleKey = false;
                isRoundKey = true;
            }
            else if (hangerState == HangerState.TriangleKey && !isRoundKey && !isTriangleKey)
            {
                characterColl.GetComponent<Character>().isHavingRoundKey = false;
                characterColl.GetComponent<Character>().isHavingTriangleKey = true;
                characterColl.GetComponentInChildren<KeyTriangle>().keyMesh.SetActive(true);
                hangerState = HangerState.Empty;

                isRoundKey = false;
                isTriangleKey = true;
            }
            else if (hangerState == HangerState.TriangleKey && isRoundKey && !isTriangleKey)
            {
                characterColl.GetComponent<Character>().isHavingRoundKey = false;
                characterColl.GetComponent<Character>().isHavingTriangleKey = true;
                characterColl.GetComponentInChildren<KeyRound>().keyMesh.SetActive( false);
                characterColl.GetComponentInChildren<KeyTriangle>().keyMesh.SetActive(true);
                hangerState = HangerState.RoundKey;

                isRoundKey = false;
                isTriangleKey = true;
            }

            
            ChangeHangerSprite();
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            isCharOn = true;
            characterColl = collision.gameObject;

            if (characterColl.GetComponent<Character>().isHavingRoundKey == true)
            {
                isTriangleKey = false;
                isRoundKey = true;
            }
            else if (characterColl.GetComponent<Character>().isHavingTriangleKey == true)
            {
                isRoundKey = false;
                isTriangleKey = true;
            }
            else
            {
                isRoundKey = false;
                isTriangleKey = false;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            isCharOn = false;
            if (GetComponentInChildren<InteractionButton>())
            {
                Destroy(GetComponentInChildren<InteractionButton>().gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            interactionObj = Instantiate(interactionPrefab, gameObject.transform);
            interactionObj.GetComponent<InteractionButton>().mouseInputString = interactionMsg;
        }
    }

    void ChangeHangerSprite()
    {
        if (hangerState == HangerState.Empty)
        {
            hangerSprite.sprite = emptySprite;
        }
        else if (hangerState == HangerState.RoundKey)
        {
            hangerSprite.sprite = roundKeySprite;
        }
        else if (hangerState == HangerState.TriangleKey)
        {
            hangerSprite.sprite = triangleKeySprite;
        }
    }
}
