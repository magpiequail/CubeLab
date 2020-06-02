using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapColor : MonoBehaviour
{
    public Tilemap tilemap;
    public int x;
    public int y;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Vector3Int v3Int = new Vector3Int(x, y, 0);
        //tilemap.SetTileFlags(v3Int, TileFlags.None);

        //tilemap.SetColor(v3Int, (Color.red));
    }
}
