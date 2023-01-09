using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("game", LoadSceneMode.Single);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
