using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource chicken;
    public AudioSource steps;
    public AudioSource scythe;
    public AudioSource splatter;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            if (!steps.isPlaying)
            {
                steps.Play();
            }
        }
        else
        {
            steps.Stop();
        }

        if (Random.Range(0, 1000) == 1)
        {
            if (!chicken.isPlaying)
            {
                chicken.Play();
            }
        }
    }
}
