using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStat : MonoBehaviour
{
    public int currentBlock = 0; //1 is current block //2 is path block
    public int visited = 1;
    public int x;
    public int y;

    public float springSpeed;
    public float springDepth;

    public Color originalColor;
    public Color pathColor;
    public Color currentBlockColor;

    private SpriteRenderer blockSprite;
    private Vector3 blockPosition;
    private Vector3 pushedPosition;
    private bool isSpringCompleted = false;
    private bool isDown = false;
    private bool isUp = true;


    // Start is called before the first frame update
    void Start()
    {
        isSpringCompleted = true;
        blockSprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        blockPosition = gameObject.GetComponent<Transform>().position;
        pushedPosition = new Vector3(blockPosition.x, blockPosition.y - springDepth, blockPosition.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBlock == 1)
        {
            
            blockSprite.color = currentBlockColor;
        }
        if (currentBlock == 2)
        {
            blockSprite.color = pathColor;
        }
        if (currentBlock == 0)
        {
            blockSprite.color = originalColor;

        }
        if (!isSpringCompleted)
        {
            StartCoroutine("SpringAnimation");
        }
    }


    IEnumerator SpringAnimation()
    {

        if (blockSprite.gameObject.GetComponent<Transform>().position.y - pushedPosition.y < 0.01)
        {
            isDown = true;
            isUp = false;
        }
        if (blockPosition.y - blockSprite.gameObject.GetComponent<Transform>().position.y < 0.01 && isDown)
        {
            isUp = true;
        }
        if (!isDown && isUp)
        {
            //going down
            blockSprite.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(blockSprite.gameObject.GetComponent<Transform>().position, pushedPosition, springSpeed * Time.deltaTime);
        }
        if (isDown && !isUp)
        {
            //going up
            blockSprite.gameObject.GetComponent<Transform>().position = Vector3.MoveTowards(blockSprite.gameObject.GetComponent<Transform>().position, blockPosition, springSpeed * Time.deltaTime);
        }
        if (isDown && isUp)
        {
            blockSprite.gameObject.GetComponent<Transform>().position = blockPosition;
            isSpringCompleted = true;
            isUp = true;
            isDown = false;
        }


        yield return null;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Character")
        {
            isSpringCompleted = false;
        }
    }

}
