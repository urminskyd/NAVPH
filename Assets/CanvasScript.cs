using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public Text timeText;
    public Text aliveText;
    public Text deadText;
    public Text rescuedText;

    public float timeRemaining;
    private int alive = 5;
    private int dead = 0;
    private int rescued = 0;

    void Update()
    {
        timeRemaining = timeRemaining > 0 ? timeRemaining - Time.deltaTime : 60;
        timeText.text = "time till another hostage dies \n" + (int)timeRemaining;

        aliveText.text = "Alive: " + alive;
        deadText.text = "Dead: " + dead;
        rescuedText.text = "RescuedText: " + rescued;
    }
}
