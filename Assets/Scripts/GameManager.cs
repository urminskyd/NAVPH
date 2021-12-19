using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    //panels
    public GameObject menuPanel;
    public GameObject mainMenu;
    public GameObject statsPanel;
    public GameObject gamePanel;

    private float timeUntillHostageDeath;

    private IDictionary<int, float> levelTime = new Dictionary<int, float>();
    public float LevelOneTimeForHostageDie;
    public float LevelTwoTimeForHostageDie;
    public float LevelThreeTimeForHostageDie;

    public float remainingTimeInGame { get; set; }
    public bool playerIsHide { get; set; } = false;
    public bool light { get; set; } = true;

    public List<GameObject> levels;

    //hostages state variables
    private float totalTime = 0;
    public int dead { get; set; }
    public int rescued { get; set; }
    public int currentLevel;

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
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        mainMenu.SetActive(true);
        remainingTimeInGame = timeUntillHostageDeath;

        levelTime.Add(1, LevelOneTimeForHostageDie);
        levelTime.Add(2, LevelTwoTimeForHostageDie);
        levelTime.Add(3, LevelThreeTimeForHostageDie);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals(SceneManager.GetSceneByBuildIndex(1).name))
        {
            if (remainingTimeInGame <= 0)
                KillHostage();
            remainingTimeInGame = remainingTimeInGame > 0 ? remainingTimeInGame -= Time.deltaTime : timeUntillHostageDeath;
            totalTime += Time.deltaTime;
        }
    }

    public IEnumerator LoadScene(string scene, int levelNumber)
    {
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (!asyncLoadLevel.isDone)
            yield return null;

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1)); //set "Game" as active scene
        Instantiate(levels[levelNumber], new Vector3(0, 0, 0), Quaternion.identity); //load chosen level

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
       Scene scene = SceneManager.GetActiveScene();
       if (scene.name == "Game")
            SceneManager.UnloadSceneAsync(scene);

       remainingTimeInGame = levelTime[levelNumber+1];
       currentLevel = levelNumber;
       StartCoroutine(LoadScene("Game", levelNumber));
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void FinishGame()
    {
        statsPanel.GetComponentInChildren<EndGameStats>().
            setStatsValues(rescued, dead, (int)totalTime, rescued > (alive + dead));
        statsPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    public void KillHostage()
    {
        dead += 1;
        Alive -= 1;

        if (Alive == 0 || dead > (alive + rescued)) //if killed more than half hostages, game over
            FinishGame();
        else //kill hostage
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
