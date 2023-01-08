using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int levelWidth;
    public int levelHeight;

    private float tileSize = 1.0f;

    public GameObject tilePrefab;

    public Sprite[] groundSprites;

    public GameObject[,] tiles;

    public Sprite topBottomSprite;
    public Sprite leftRightSprite;

    private void Start() {
        CreateMap();    
    }

    void CreateMap()
    {
        tiles = new GameObject[levelWidth, levelHeight];
        

        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                GameObject tile = GameObject.Instantiate(tilePrefab, new Vector2(x * tileSize, y * tileSize), Quaternion.identity);
                
                if (x == 0 || x == levelWidth + 1)
                {
                    // horizontal edges
                    if (y == 0)
                    {
                        // top-left corner
                        tile.GetComponent<SpriteRenderer>().sprite = groundSprites[0];
                    }
                    else if (y == levelHeight + 1)
                    {
                        // bottom-left corner
                        tile.GetComponent<SpriteRenderer>().sprite = groundSprites[2];
                    }
                    else
                    {
                        tile.GetComponent<SpriteRenderer>().sprite = topBottomSprite;
                    }
                }
                else if (y == 0 || y == levelHeight + 1)
                {
                    // vertical edges
                    if (x == 0)
                    {
                        // top-right corner
                        tile.GetComponent<SpriteRenderer>().sprite = groundSprites[0];
                    }
                    else if (x == levelWidth + 1)
                    {
                        // bottom-right corner
                        tile.GetComponent<SpriteRenderer>().sprite = groundSprites[1];
                    }
                    else
                    {
                        tile.GetComponent<SpriteRenderer>().sprite = leftRightSprite;
                    }
                }
                else
                {
                    tile.GetComponent<SpriteRenderer>().sprite = groundSprites[Random.Range(0, groundSprites.Length)];
                }
                
                tiles[x, y] = tile;
            }
        }
    }
}
