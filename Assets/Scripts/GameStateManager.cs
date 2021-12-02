using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public bool isGameOver = false;
    public int dead;
    public int remaining;
    public int rescued;

    void Awake()
    {
        if (instance == null) // Singleton
            instance = this;
        else if(instance != this)
        {
            // If the instance reference has already been set, and this is not the
            // the instance reference, destroy this game object.
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); // Do not destroy this object, when we load a new scene.
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
