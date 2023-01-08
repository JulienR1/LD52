using UnityEngine;

public class Gage : MonoBehaviour
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform foreground;

    [SerializeField] private int soulCapacity = 30;
    [SerializeField] private float soulConsuptionRate;

    private float filledPercent;

    private void Start()
    {
        filledPercent = 1;
        UpdateGage(1);
    }

    private void Update()
    {
        filledPercent -= soulConsuptionRate * Time.deltaTime;
        UpdateGage(filledPercent);
        if (filledPercent <= 0)
        {
            GameManager.GameOver();
        }
    }

    public void AddSouls(int soulCount)
    {
        filledPercent = Mathf.Clamp01(filledPercent + soulCount / (float)soulCapacity);
    }

    private void UpdateGage(float percent)
    {
        foreground.sizeDelta = new Vector2(Mathf.Clamp01(percent) * background.sizeDelta.x, foreground.sizeDelta.y);
    }
}
