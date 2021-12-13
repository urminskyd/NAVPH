using UnityEngine.SceneManagement;
using UnityEngine;

public enum GameState { MAIN_MENU }

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public InGameUI inGameUI { get; set; }
    public GameState gameState { get; private set; }
    public GameObject menuPanel;
    public GameObject mainMenu;
    public GameObject levelMenu;
    public GameObject controlsMenu;
    public GameObject statsPanel;
    public GameObject gamePanel;

    public float timeUntillHostageDeath;
    public int alive { get; set; }
    public int dead { get; set; }
    public int rescued { get; set; }

    protected GameManager() {}

    public static GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                instance = new GameManager();
                DontDestroyOnLoad(instance);
                Debug.Log("CREATING GAME MANAGER");
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        mainMenu.SetActive(true);

        alive = 5;
        dead = 0;
        rescued = 0;
        timeUntillHostageDeath = 60;

        //SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
    }

    public void HideMenu()
    {
        menuPanel.SetActive(false);
        GameObject[] menuItems = GameObject.FindGameObjectsWithTag("MenuItem");

        foreach (GameObject item in menuItems)
        {
            item.SetActive(false);
        }
    }

    public void PlayGame()
    {
        LoadScene("Game");
        HideMenu();
        gamePanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void FinishGame()
    {
        statsPanel.GetComponentInChildren<EndGameStats>().setStatsValues(rescued, dead, 0, rescued > (alive + dead));
        statsPanel.SetActive(true);
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);

        SceneManager.UnloadSceneAsync("Game");
        Debug.Log("FINISH");
    }

    public void KillHostage()
    {
        //TODO znicit objekt jedneho rukojemnika zo sceny
        dead += 1;
        alive -= 1;

        if (alive == 0 || dead > (alive + rescued)) // is killed more than half hostages
            FinishGame();
    }

    public void RescueHostage()
    {
        rescued += 1;
        alive -= 1;

        if (alive == 0)
            FinishGame();
    }

}
