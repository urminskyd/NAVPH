using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public Text timeText;
    public Text aliveText;
    public Text deadText;
    public Text rescuedText;

    private GameManager gameManager;
    private float hostageDeathTime;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.inGameUI = this;
        hostageDeathTime = gameManager.timeUntillHostageDeath;
    }

    void Update()
    {
        if (hostageDeathTime > 0)
            hostageDeathTime -= Time.deltaTime;
        else
        {
            hostageDeathTime = gameManager.timeUntillHostageDeath;
            gameManager.KillHostage();
        }

        timeText.text = "time till another hostage dies \n" + (int)hostageDeathTime;
        aliveText.text = "Alive: " + gameManager.alive;
        deadText.text = "Dead: " + gameManager.dead;
        rescuedText.text = "Rescued: " + gameManager.rescued;
    }
}
