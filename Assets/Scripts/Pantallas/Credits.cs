using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    void Start()
    {
        Invoke("WaitToEnd", 18f);
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