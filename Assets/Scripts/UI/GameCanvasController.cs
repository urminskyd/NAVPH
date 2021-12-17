using UnityEngine;

public class GameCanvasController : MonoBehaviour
{
    private bool isPaused;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetButton("Pause"))
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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
