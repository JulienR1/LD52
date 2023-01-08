using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int levelWidth;
    public int levelHeight;

    public int mapScale = 100;
    private float tileSize = 1.0f;

    public GameObject tilePrefab;

    public Sprite[] groundSprites;
    public Sprite[] clockwiseCornerSprites;
    public Sprite edgeHorizontalSprite;
    public Sprite edgeVerticalSprite;

    public GameObject[,] tiles;


    private void Start()
    {
        CreateMap();
    }

    private void CreateMap()
    {
        tiles = new GameObject[mapScale, mapScale];

        // Crée la map
        for (int x = 0; x < mapScale; x++)
        {
            for (int y = 0; y < mapScale; y++)
            {
                Vector2 tilePosition = new Vector2(x * tileSize - mapScale * 0.5f, y * tileSize - mapScale * 0.5f);
                GameObject tile = GameObject.Instantiate(tilePrefab, tilePosition, Quaternion.identity);

                tile.GetComponent<SpriteRenderer>().sprite = groundSprites[Random.Range(0, groundSprites.Length)];
                tile.GetComponent<SpriteRenderer>().sortingOrder = -1;

                tile.transform.parent = GameObject.Find("World").transform;
                tile.name = "World " + x + "_" + y + "";
                tiles[x, y] = tile;
            }
        }

        GenerateLevel(tiles);
    }


    private void GenerateLevel(GameObject[,] tiles)
    {
        int startX = mapScale / 2 - levelWidth / 2;
        int startY = mapScale / 2 - levelHeight / 2;

        // Recentre le niveau
        for (int x = startX; x < startX + levelWidth + 2; x++)
        {
            for (int y = startY; y < startY + levelHeight + 2; y++)
            {
                GameObject tile = tiles[x, y];

                GameObject edgeTile = GameObject.Instantiate(tile, tile.transform.position, Quaternion.identity);
                edgeTile.transform.parent = GameObject.Find("Level Edges").transform;
                edgeTile.name = "Edge " + x + "_" + y + "";
                edgeTile.GetComponent<SpriteRenderer>().sortingOrder = 1;

                edgeTile.AddComponent<BoxCollider2D>();
                edgeTile.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
                edgeTile.GetComponent<BoxCollider2D>().isTrigger = true;

                // Génère les bordures du niveau et détruit les tuiles dupliqués à l'intérieur du niveau 
                if (x == startX || x == startX + levelWidth + 1)
                {
                    edgeTile.GetComponent<SpriteRenderer>().sprite = edgeVerticalSprite;
                    edgeTile.GetComponent<BoxCollider2D>().size = new Vector2(0.165f * 0.5f, 0.165f);

                }
                else if (y == startY || y == startY + levelHeight + 1)
                {
                    edgeTile.GetComponent<SpriteRenderer>().sprite = edgeHorizontalSprite;
                    edgeTile.GetComponent<BoxCollider2D>().size = new Vector2(0.165f, 0.165f * 0.5f);

                }
                else
                {
                    Destroy(edgeTile);
                }

                // Change le sprite des coins pour le sprite de coin respectif
                if (x == startX && y == startY)
                {
                    edgeTile.GetComponent<SpriteRenderer>().sprite = clockwiseCornerSprites[3];
                }
                else if (x == startX + levelWidth + 1 && y == startY + levelHeight + 1)
                {
                    edgeTile.GetComponent<SpriteRenderer>().sprite = clockwiseCornerSprites[1];
                }
                else if (x == startX + levelWidth + 1 && y == startY)
                {
                    edgeTile.GetComponent<SpriteRenderer>().sprite = clockwiseCornerSprites[2];
                }
                else if (x == startX && y == startY + levelHeight + 1)
                {
                    edgeTile.GetComponent<SpriteRenderer>().sprite = clockwiseCornerSprites[0];
                }
            }
        }

        // Renomme les tuiles du (niveau seulement) sur un plan cartésien
        for (int x = 0; x < levelWidth; x++)
        {
            for (int y = 0; y < levelHeight; y++)
            {
                GameObject tile = tiles[x + startX + 1, y + startY + 1];
                tile.transform.parent = GameObject.Find("Level").transform;
                tile.name = "(" + (x - levelWidth / 2) + ", " + (y - levelHeight / 2) + ")";
            }
        }
    }
}