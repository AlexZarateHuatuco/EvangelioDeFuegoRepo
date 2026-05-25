using UnityEngine;
using TMPro ;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameManager gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button menuButton;
    private bool gameOverActive = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (gameOverPanel != null) 
 //           gameOverPanel.SetActive(false);
        if (restartButton != null) 
            restartButton.onClick.AddListener(resetScene);
        if (menuButton != null) 
            menuButton.onClick.AddListener(menuScene);
    }
    void Update()
    {
        if (gameOverActive)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                resetScene();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                menuScene();
            }
        }
    }
    public void GameOver()
    {
        if (gameOverActive) return;
        gameOverActive = true;
        if (gameOverPanel != null)
        {
 //           gameOverPanel.SetActive(true);
        }
        if (gameOverText != null)
        {
            gameOverText.text = "Game Over\n\nR - Reiniciar\nM - Menú de Inicio";
        }
    }
    public void resetScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void menuScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuInicio");
    }
}
