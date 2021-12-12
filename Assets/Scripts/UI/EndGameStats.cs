using UnityEngine;
using UnityEngine.UI;

public class EndGameStats : MonoBehaviour
{
    public Text rescuedText;
    public Text diedText;
    public Text totalTimeText;
    public Text titleText;

    private int rescued { get; set; }
    private int died { get; set; }
    private float totalTime { get; set; }
    private string title { get; set; }

    void Start()
    {
        titleText.text = title;
        rescuedText.text = "Rescued: " + rescued;
        diedText.text = "Died: " + died;
        totalTimeText.text = "Total time: " + totalTime;
    }

    public void setStatsValues(int rescued, int died, float totalTime, bool gameWin)
    {
        Debug.Log(totalTime);
        title = gameWin ? "GAME WIN" : "GAME OVER";
        this.rescued = rescued;
        this.died = died;
        this.totalTime = totalTime;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
