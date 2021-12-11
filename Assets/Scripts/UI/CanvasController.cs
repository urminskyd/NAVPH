using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {

        //Debug.Log("TEST");
        if (Input.GetKeyDown(KeyCode.Escape))

        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = 1f; //unfreeze game
    }

    void Pause()
    {
        isPaused = true;
        //Debug.Log(GameManager.Instance.inGameUI.aliveText);
        pauseMenuUI.SetActive(isPaused);
        Time.timeScale = 0; //freeze game
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
