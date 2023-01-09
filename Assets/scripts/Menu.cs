using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class Menu : MonoBehaviour
{
    public TMP_Text up, left, down, right;
    public TMP_Text harvest;
    public TMP_Text interact;

    private void Awake()
    {
        SetupKeys();
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("game", LoadSceneMode.Single);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void SetupKeys()
    {
        var horizontal = InputManager.GetKeysForAxis(Axis.Horizontal);
        var vertical = InputManager.GetKeysForAxis(Axis.Vertical);
        up.text = vertical.FirstOrDefault(x => x.Value.Equals(Vector2.up)).Key.ToString();
        left.text = horizontal.FirstOrDefault(x => x.Value.Equals(Vector2.left)).Key.ToString();
        down.text = vertical.FirstOrDefault(x => x.Value.Equals(Vector2.down)).Key.ToString();
        right.text = horizontal.FirstOrDefault(x => x.Value.Equals(Vector2.right)).Key.ToString();

        harvest.text = InputManager.GetKeysForAction(KeyAction.Attack)[0].ToString();
        interact.text = InputManager.GetKeysForAction(KeyAction.Interact)[0].ToString();
    }
}
