using UnityEngine;
using UnityEngine.SceneManagement;

public class FallingKill : MonoBehaviour

{
    [Header("Fall Settings")]
    [SerializeField] private float fallThreshold = 20f;      // metres
    [SerializeField] private float timeWindow = 2f;          // seconds
    [SerializeField] private string gameOverScene = "GameOver";
    [SerializeField] private float time;
    [SerializeField] private float currentHeight;
    [SerializeField] private float lastHeight;
    [SerializeField] private GameObject monitoredEntity;
    // if fall < 20 meters in less than 2 seconds
    // then
    // kill
    // x-x  ;-;
    private void Start()
    {
        if (monitoredEntity == null)
        {
            Debug.Log("Wwahwahwa-- no gameObejct!! :C PLS FIX MEEEE");
            Debug.Log("Wait. I can. Fix meself");
            monitoredEntity = gameObject;
            currentHeight = monitoredEntity.transform.position.y;
            lastHeight = currentHeight;
            time = 0f;
        }
        else
        {
            currentHeight = monitoredEntity.transform.position.y;
            lastHeight = currentHeight;
            time = 0f;
        }
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= timeWindow)
        {
            currentHeight = monitoredEntity.transform.position.y;

            if ((lastHeight - currentHeight) >= fallThreshold)
            {
                Die();
            }
            else
            {
                lastHeight = currentHeight;
            }

            time = 0;
        }
    }

    private void Die()
    {
        if (CompareTag("Player"))
        {
            SceneManager.LoadScene(gameOverScene);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}

