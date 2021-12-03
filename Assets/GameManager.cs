using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { MAIN_MENU }

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GameState gameState { get; private set; }


    public static GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                Debug.LogError("Game manager is null");
            }

            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void OnApplicationQuit()
    //{
    //    SimpleGameManager.instance = null;
    //}
}
