using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    public Text timeText;
    public Text aliveText;
    public Text deadText;
    public Text rescuedText;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        timeText.text = "time till another hostage dies \n" + (int)gameManager.remainingTimeInGame;
        aliveText.text = "Alive: " + gameManager.alive;
        deadText.text = "Dead: " + gameManager.dead;
        rescuedText.text = "Rescued: " + gameManager.rescued;
    }
}