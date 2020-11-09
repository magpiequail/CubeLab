using UnityEngine;

public enum HangerState
{
    Empty,
    RoundKey,
    TriangleKey,
    SquareKey,
    DiamondKey
    
}
/*public enum CharacterKey
{
    Empty,
    RoundKey,
    TriangleKey,
    SquareKey,
    DiamondKey
}*/

public class KeyHanger : Interactables
{
    public HangerState hangerState;
    //private CharacterKey charKey;
    public Sprite emptySprite;
    public Sprite roundKeySprite;
    public Sprite triangleKeySprite;
    public Sprite squareKeySprite;
    public Sprite diamondKeySprite;
    public GameObject roundKeyPrefab;
    public GameObject triangleKeyPrefab;
    public GameObject squareKeyPrefab;
    public GameObject diamondKeyPrefab;


    SpriteRenderer hangerSprite;
    CircleCollider2D hangerCollider;

    GameObject characterColl;
    bool isCharOn = false;
    Character currChar;

    public GameObject interactionPrefab;
    //GameObject interactionObj;
    //public string interactionMsg = "사\r\n용";

    private void Awake()
    {
        hangerSprite = GetComponentInChildren<SpriteRenderer>();
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

            StartInteraction();
            
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Character")
        {
            isCharOn = true;
            characterColl = collision.gameObject;
            currChar = characterColl.GetComponent<Character>();

            /*if (characterColl.GetComponent<Character>().characterKey == CharKeyState.RoundKey)
            {
                charKey = CharacterKey.RoundKey;
            }
            else if (characterColl.GetComponent<Character>().characterKey == CharKeyState.TriangleKey)
            {
                charKey = CharacterKey.TriangleKey;
            }
            else if(characterColl.GetComponent<Character>().isHavingSquareKey == true)
            {
                charKey = CharacterKey.SquareKey;
            }
            else if (characterColl.GetComponent<Character>().isHavingDiamondKey)
            {
                charKey = CharacterKey.DiamondKey;
            }
            else
            {
                charKey = CharacterKey.Empty;

            }*/
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
        else if( hangerState == HangerState.SquareKey)
        {
            hangerSprite.sprite = squareKeySprite;
        }
        else if( hangerState == HangerState.DiamondKey)
        {
            hangerSprite.sprite = diamondKeySprite;
        }
    }
    void SwapKeys(GameObject key, CharKeyState state)
    {
        if (characterColl.GetComponentInChildren<Key>())
        {
            Destroy(characterColl.GetComponentInChildren<Key>().gameObject);
        }
        GameObject tempKey = Instantiate(key, characterColl.transform);
        FindObjectOfType<AudioManager>().PlayAudio("Ingame_elevator");
        tempKey.GetComponent<Key>().effectAnim.SetTrigger("EffectTrigger");
        tempKey.GetComponent<Key>().keyAnim.SetInteger("State", 2);
        if (characterColl.GetComponent<NormalCharacter>())
        {
            characterColl.GetComponentInChildren<Animator>().SetTrigger("Joy");
            if (characterColl.GetComponentInChildren<Animator>().GetInteger("Direction") < 3)
            {
                characterColl.GetComponentInChildren<Animator>().SetInteger("Direction", 3);
            }
        }
        switch (currChar.characterKey)
        {
            case CharKeyState.RoundKey:

                hangerState = HangerState.RoundKey;
                break;
            case CharKeyState.TriangleKey:
                hangerState = HangerState.TriangleKey;
                break;
            case CharKeyState.SquareKey:
                hangerState = HangerState.SquareKey;
                break;
            case CharKeyState.DiamondKey:
                hangerState = HangerState.DiamondKey;
                break;
            case CharKeyState.Empty:
                hangerState = HangerState.Empty;
                break;
        }
        currChar.characterKey = state;
    }
    public override void StartInteraction()
    {
        base.StartInteraction();
        if (hangerState == HangerState.Empty)
        {

            if (characterColl.GetComponentInChildren<Key>())
            {
                Destroy(characterColl.GetComponentInChildren<Key>().gameObject);
            }

            switch (currChar.characterKey)
            {
                case CharKeyState.Empty:
                    hangerState = HangerState.Empty;
                    break;
                case CharKeyState.RoundKey:

                    hangerState = HangerState.RoundKey;
                    break;
                case CharKeyState.TriangleKey:
                    hangerState = HangerState.TriangleKey;
                    break;
                case CharKeyState.SquareKey:
                    hangerState = HangerState.SquareKey;
                    break;
                case CharKeyState.DiamondKey:
                    hangerState = HangerState.DiamondKey;
                    break;
            }
            currChar.characterKey = CharKeyState.Empty;
        }
        else if (hangerState == HangerState.RoundKey)
        {

            SwapKeys(roundKeyPrefab, CharKeyState.RoundKey);
        }
        else if (hangerState == HangerState.TriangleKey)
        {
            SwapKeys(triangleKeyPrefab, CharKeyState.TriangleKey);
        }
        else if (hangerState == HangerState.SquareKey)
        {

            SwapKeys(squareKeyPrefab, CharKeyState.SquareKey);
        }
        else if (hangerState == HangerState.DiamondKey)
        {
            SwapKeys(diamondKeyPrefab, CharKeyState.DiamondKey);

        }



        ChangeHangerSprite();
    }
}
