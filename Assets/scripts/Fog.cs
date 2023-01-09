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
        GameObject fog = new GameObject("Fog");
        fog.transform.parent = GameObject.FindGameObjectWithTag("Player").transform;

        SpriteRenderer fogRenderer = fog.AddComponent<SpriteRenderer>();
        fogRenderer.sprite = Resources.Load<Sprite>("sprites/environment/mask");
        fogRenderer.color = new Color(255, 255, 255, 0.05f);
        fogRenderer.sortingOrder = 0;

        fog.transform.localScale = new Vector2(cam.aspect * cam.orthographicSize * 4, cam.orthographicSize * 4);
        fog.transform.position = new Vector2(cam.transform.position.x, cam.transform.position.y - 0.25f);
    }

    public void Update()
    {
        GameObject fog = GameObject.Find("Fog");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // disable object tagged "Animal" when outside the mask
        GameObject[] animals = GameObject.FindGameObjectsWithTag("Animal");
        foreach (GameObject animal in animals)
        {
            animal.transform.Find("graphics").GetComponent<SpriteRenderer>().enabled = fog.GetComponent<SpriteRenderer>().bounds.Contains(animal.transform.position);
        }

        fog.transform.localScale = new Vector2(fog.transform.localScale.x + Mathf.Sin(Time.time * 0.5f) * 0.0025f, fog.transform.localScale.y + Mathf.Sin(Time.time * 0.5f) * 0.0025f);
    }
}
