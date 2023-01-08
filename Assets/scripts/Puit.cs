using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puit : MonoBehaviour
{
    private int souls = 0;

    public int getSouls()
    {
        return souls;
    }

    [SerializeField] private bool triggerActive = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerActive = false;
        }
    }

    private void Update()
    {
        if (triggerActive && InputManager.GetActionDown(KeyAction.Interact))
        {
            Balayeuse();
        }
    }

    public void Balayeuse()
    {
        //TODO generate event so the game is freeze or whatever...
        foreach (GameObject spiritObj in GameObject.FindGameObjectsWithTag("Soul"))
        {
            souls++;
            print(souls);
            spiritObj.transform.position = Vector2.MoveTowards(spiritObj.transform.position, this.transform.position, 1);
            if (Vector2.Distance(spiritObj.transform.position, this.transform.position) < 0.1f)
            {
                GameManager.AddSouls(1);
                Destroy(spiritObj);
            }
        }
    }

}