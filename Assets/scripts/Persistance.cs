using UnityEngine;

public class Persistance : MonoBehaviour
{
    void Awake()
    {
        Persistance[] persistedObjs = GameObject.FindObjectsOfType<Persistance>();
        if (persistedObjs.Length > 1)
        {
            GameObject.Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
