using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Gage gage;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text soulCounterText;

    [SerializeField] private GameObject cardManager;

    [Header("Settings")]
    [SerializeField] private float scorePerSecond = 1;
    [SerializeField] private int soulBonusScore = 100;

    private static GameManager instance = null;

    private float score;
    private int soulCount;

    public static int GetSoulCount()
    {
        return GameManager.instance.soulCount;
    }

    public static void SetSoulCount(int newSoulCount)
    {
        GameManager.instance.soulCount = newSoulCount;
    }

    private void Awake()
    {
        if (instance == null)
            GameManager.instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Update()
    {
        score += scorePerSecond * Time.deltaTime;

        scoreText.text = "Score: " + ((int)score).ToString().PadLeft(8, '0');
        soulCounterText.text = "Souls: " + soulCount;
    }

    public static void AddSouls(int soulCount)
    {
        GameManager.instance.soulCount += soulCount;
        GameManager.instance.gage.AddSouls(soulCount);
        GameManager.instance.score += soulCount * GameManager.instance.soulBonusScore;
    }

    public static void ShoppingTime()
    {
        // GameManager.instance.cardManager.showCards();
    }

    public static void GameOver()
    {
        // print("GAMe over noob");
    }
}
