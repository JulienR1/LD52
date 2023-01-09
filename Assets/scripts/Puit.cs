using UnityEngine;

public class Puit : MonoBehaviour
{
    private bool triggerActive = false;
    private int pendingSouls = 0;

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
            //GameManager.ShoppingTime();
        }
    }

    public void Balayeuse()
    {
        foreach (var spirit in FindObjectsOfType<Spirit>())
        {
            spirit.Sacrifice();
        }
    }

    public void Open()
    {
        if (pendingSouls == 0)
        {
            //TODO: open well animation or something
        }

        pendingSouls++;
    }

    public void Close()
    {
        if (pendingSouls == 0)
            return;

        pendingSouls--;

        if (pendingSouls == 0)
        {

            //TODO: close well animation or something
        }
    }

}