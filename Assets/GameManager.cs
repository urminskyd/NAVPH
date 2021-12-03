using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public enum GameState { MAIN_MENU }

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GameState gameState { get; private set; }

    protected GameManager() {}

    public static GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                DontDestroyOnLoad(instance);
                instance = new GameManager();
                Debug.LogError("Game manager is null");
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        Debug.Log("GameManager - Main menu");

        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }

    // Start is called before the first frame update
    void Start()
    {
        //instance = this;
        //Debug.Log("Loading");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
