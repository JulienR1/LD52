using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;

        CreateFog();
    }
    
    // Create a fog of war using a sprite on the camera
    public void CreateFog()
    {
        // create a new game object Fog and attach it to the player
        GameObject fog = new GameObject("Fog");
        fog.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;

        SpriteRenderer fogSprite = fog.AddComponent<SpriteRenderer>();
        fogSprite.sprite = Resources.Load<Sprite>("sprites/environment/fow_mask");
        fogSprite.sortingOrder = 9;
        fogSprite.color = new Color(0, 0, 0, 0.5f);
    }

    public void Update()
    {
        GameObject fog = GameObject.Find("Fog");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        fog.transform.localScale = new Vector2(cam.aspect * cam.orthographicSize * 2, cam.orthographicSize * 2);
        fog.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);

        // disable object tagged "Animal" when outside the mask
        GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");
        foreach (GameObject animal in animals)
        {
        animal.transform.Find("graphics").GetComponent<SpriteRenderer>().enabled = fog.GetComponent<SpriteRenderer>().bounds.Contains(animal.transform.position);
        }
    }
}
