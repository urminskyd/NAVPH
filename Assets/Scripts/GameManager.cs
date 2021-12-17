using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GameplayUI gameplayUI { get; set; }
    public GameObject menuPanel;
    public GameObject mainMenu;
    public GameObject levelMenu;
    public GameObject controlsMenu;
    public GameObject statsPanel;
    public GameObject gamePanel;

    public float timeUntillHostageDeath;
    public float remainingTimeInGame { get; set; }

    public int dead { get; set; }
    public int rescued { get; set; }
    public bool playerIsHide { get; set; } = false;

    public List<GameObject> levels;

    public delegate void OnScoreChange();
    public event OnScoreChange scoreChanged;

    public int alive;

    public int Alive
    {
        get
        {
            return alive;
        }

        set
        {
            alive = value;
            scoreChanged?.Invoke();
        }
    }

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
        remainingTimeInGame = timeUntillHostageDeath;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals(SceneManager.GetSceneByBuildIndex(1).name))
        {
            if (remainingTimeInGame <= 0)
                KillHostage();
            remainingTimeInGame = remainingTimeInGame > 0 ? remainingTimeInGame -= Time.deltaTime : timeUntillHostageDeath;
        }
    }

    public IEnumerator LoadScene(string scene, int levelNumber)
    {
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (!asyncLoadLevel.isDone)
            yield return null;

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
        GameObject level = Instantiate(levels[levelNumber], new Vector3(0, 0, 0), Quaternion.identity);
        SceneManager.MoveGameObjectToScene(level, SceneManager.GetSceneByBuildIndex(1));
        HideMenu();
        gamePanel.SetActive(true);
    }

    public void HideMenu()
    {
        menuPanel.SetActive(false);
        GameObject[] menuItems = GameObject.FindGameObjectsWithTag("MenuItem");

        foreach (GameObject item in menuItems)
            item.SetActive(false);
    }

    public void PlayGame(int levelNumber)
    {
       StartCoroutine(LoadScene("Game", levelNumber));
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
        gamePanel.SetActive(false);

        Debug.Log("FINISH");
    }

    public void KillHostage()
    {
        dead += 1;
        Alive -= 1;

        if (Alive == 0 || dead > (alive + rescued)) // is killed more than half hostages
            FinishGame();
        else
        {
            GameObject hostage = GameObject.FindGameObjectWithTag("Hostage");
            hostage.GetComponentInChildren<HostageController>().HostageDeath();
        }
    }

    public void RescueHostage()
    {
        rescued += 1;
        Alive -= 1;

        if (Alive == 0)
            FinishGame();
    }

}
