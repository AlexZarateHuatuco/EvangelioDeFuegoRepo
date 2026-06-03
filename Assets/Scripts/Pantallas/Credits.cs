using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    void Start()
    {
        Invoke("WaitToEnd", 4.5f);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MenuInicio");
        }
    }
    void WaitToEnd()
    {
        SceneManager.LoadScene("MenuInicio");
    }
}