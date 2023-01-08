using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Gage gage;

    private static GameManager instance = null;

    private int soulCount;

    private void Awake()
    {
        if (instance == null)
        {
            GameManager.instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static void AddSouls(int soulCount)
    {
        GameManager.instance.soulCount += soulCount;
        GameManager.instance.gage.AddSouls(soulCount);
    }

    public static void GameOver()
    {
        print("GAMe over noob");
    }
}
