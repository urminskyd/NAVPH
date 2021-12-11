using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


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

    //public static bool isPaused = false;
    //public GameObject pauseMenuUI;
    //public Text timeText;
    //public Text aliveText;
    //public Text deadText;
    //public Text rescuedText;

    //public float timeRemaining;
    //private int alive = 5;
    //private int dead = 0;
    //private int rescued = 0;

    protected GameManager() {}

    public static GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                DontDestroyOnLoad(instance);
                instance = new GameManager();
                Debug.Log("CREATING GAME MANAGER");
            }
            return instance;
        }
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

    public void FinishGame()
    {
        //LoadScene("Menu");
        //if (menuPanel) {
        //print(menuPanel);
        //menuPanel.SetActive(false);
        //}
        menuPanel.SetActive(true);
        statsPanel.SetActive(true);
        Debug.Log("FINISH");
    }

    private void Awake()
    {
        instance = this;
        mainMenu.SetActive(true);
        Debug.Log("GameManager - Main menu");
        //statsPanel.SetActive(true);
        //Debug.Log(statsPanel);

        //SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }

    //private void Resume()
    //{
    //    isPaused = false;
    //    pauseMenuUI.SetActive(isPaused);
    //    Time.timeScale = 1f; //unfreeze game
    //}

    //private void Pause()
    //{
    //    isPaused = true;
    //    pauseMenuUI.SetActive(isPaused);
    //    Time.timeScale = 0; //freeze game
    //}

    // Start is called before the first frame update
    void Start()
    {
        //instance = this;
        //Debug.Log("Loading");
    }

    // Update is called once per frame
    void Update()
    {
        //timeRemaining = timeRemaining > 0 ? timeRemaining - Time.deltaTime : 60;
        //timeText.text = "time till another hostage dies \n" + (int)timeRemaining;

        //aliveText.text = "Alive: " + alive;
        //deadText.text = "Dead: " + dead;
        //rescuedText.text = "Rescued: " + rescued;
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Debug.Log("TEST");
        //    if (isPaused)
        //        Resume();
        //    else
        //        Pause();
        //}
    }
}
