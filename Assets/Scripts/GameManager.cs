using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

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
    public int alive;
    public int dead { get; set; }
    public int rescued { get; set; }
    public bool playerIsHide { get; set; } = false;

    public List<GameObject> levels;

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
    }

    public IEnumerator LoadScene(string scene, int levelNumber)
    {
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        while (!asyncLoadLevel.isDone)
            yield return null;

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
        GameObject level = Instantiate(levels[levelNumber], new Vector3(0, 0, 0), Quaternion.identity);
        SceneManager.MoveGameObjectToScene(level, SceneManager.GetSceneByBuildIndex(1));
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
        gamePanel.SetActive(false);

        Debug.Log("FINISH");
    }

    public void KillHostage()
    {
        dead += 1;
        alive -= 1;

        if (alive == 0 || dead > (alive + rescued)) // is killed more than half hostages
            FinishGame();
        else
        {
            GameObject hostage = GameObject.FindGameObjectWithTag("Hostage");
            Destroy(hostage.gameObject);
        }
    }

    public void RescueHostage()
    {
        rescued += 1;
        alive -= 1;

        if (alive == 0)
            FinishGame();
    }

}
