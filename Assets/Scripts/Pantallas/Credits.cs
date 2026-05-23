using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    
    void Start()
    {
        Invoke("WaitToEnd", 10f);
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("MenuInicio");
        }
    }
    void WaitToEnd()
    {
        SceneManager.LoadScene("MenuInicio");
    }
}
