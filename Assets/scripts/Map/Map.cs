using UnityEngine;

public class Map : MonoBehaviour
{
    public int levelWidth;
    public int levelHeight;

    private int mapScale;
    private float tileSize = 1.0f;

    public GameObject tilePrefab;

    public Sprite[] groundSprites;
    public Sprite[] clockwiseCornerSprites;
    public Sprite edgeHorizontalSprite;
    public Sprite edgeVerticalSprite;

    public GameObject[,] tiles;

    public void Init()
    {
        mapScale = (levelWidth > levelHeight ? levelWidth : levelHeight) + 10;
        CreateMap();
    }

    public void CreateMap()
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

                tile.transform.parent = GameObject.Find("world").transform;
                tile.name = "World " + x + "_" + y + "";
                tiles[x, y] = tile;
            }
        }

        GenerateLevel(tiles);
    }


    private void GenerateLevel(GameObject[,] tiles)
    {
        int startX = (mapScale - 2) / 2 - levelWidth / 2;
        int startY = (mapScale - 2) / 2 - levelHeight / 2;

        // Recentre le niveau
        for (int x = startX; x < startX + levelWidth + 2; x++)
        {
            for (int y = startY; y < startY + levelHeight + 2; y++)
            {
                GameObject tile = tiles[x, y];

                GameObject edgeTile = GameObject.Instantiate(tile, tile.transform.position, Quaternion.identity);
                edgeTile.transform.parent = GameObject.Find("level edges").transform;
                edgeTile.name = "Edge " + x + "_" + y + "";
                edgeTile.GetComponent<SpriteRenderer>().sortingOrder = 1;

                // Génère les bordures du niveau et détruit les tuiles dupliqués à l'intérieur du niveau 
                if (x == startX || x == startX + levelWidth + 1)
                    edgeTile.GetComponent<SpriteRenderer>().sprite = edgeVerticalSprite;
                else if (y == startY || y == startY + levelHeight + 1)
                    edgeTile.GetComponent<SpriteRenderer>().sprite = edgeHorizontalSprite;
                else
                    Destroy(edgeTile);

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
                tile.transform.parent = GameObject.Find("level").transform;
                tile.name = "(" + (x - levelWidth / 2) + ", " + (y - levelHeight / 2) + ")";
            }
        }

        // haha dont speak about that
        var leftColliderObj = new GameObject("left collider");
        var leftCollider = leftColliderObj.AddComponent<BoxCollider2D>();
        leftCollider.isTrigger = true;

        leftCollider.transform.SetParent(GameObject.Find("level").transform);
        leftCollider.transform.position = (0.5f * levelWidth + 1) * Vector3.left;
        leftCollider.transform.localScale = new Vector3(0.08f, 1.5f * levelHeight, 1);

        var rightColliderObj = new GameObject("right collider");
        var rightCollider = rightColliderObj.AddComponent<BoxCollider2D>();
        rightCollider.isTrigger = true;

        rightCollider.transform.SetParent(GameObject.Find("level").transform);
        rightCollider.transform.position = 0.5f * levelWidth * Vector3.right;
        rightCollider.transform.localScale = new Vector3(0.08f, 1.5f * levelHeight, 1);

        var topColliderObj = new GameObject("top collider");
        var topCollider = topColliderObj.AddComponent<BoxCollider2D>();
        topCollider.isTrigger = true;

        topCollider.transform.SetParent(GameObject.Find("level").transform);
        topCollider.transform.position = 0.5f * levelHeight * Vector3.up;
        topCollider.transform.localScale = new Vector3(1.5f * levelWidth, 0.08f, 1);

        var downColliderObj = new GameObject("down collider");
        var downCollider = downColliderObj.AddComponent<BoxCollider2D>();
        downCollider.isTrigger = true;

        downCollider.transform.SetParent(GameObject.Find("level").transform);
        downCollider.transform.position = (0.5f * levelHeight + 1) * Vector3.down;
        downCollider.transform.localScale = new Vector3(1.5f * levelWidth, 0.08f, 1);
    }
}