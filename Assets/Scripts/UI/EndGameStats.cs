using UnityEngine;
using UnityEngine.UI;

public class EndGameStats : MonoBehaviour
{
    public Text rescuedText;
    public Text diedText;
    public Text totalTimeText;
    public Text scoreText;

    private int rescued = 0;
    private int died = 0;
    private int totalTime = 0;
    private string score = "A";

    void Update()
    {
        scoreText.text = score;
        rescuedText.text = "Rescued: " + rescued;
        diedText.text = "Died: " + died;
        totalTimeText.text = "Total time: " + totalTime;
    }
}
