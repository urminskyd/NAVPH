using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EndGameStats : MonoBehaviour
{
    public Text rescuedText;
    public Text diedText;
    public Text totalTimeText;
    public Text titleText;
    public GameObject nextLevelButton;

    private int rescued { get; set; }
    private int died { get; set; }
    private int totalTime { get; set; }
    private string title { get; set; }

    void Start()
    {
        titleText.text = title;
        rescuedText.text = "Rescued: " + rescued;
        diedText.text = "Died: " + died;
        totalTimeText.text = "Total time: " + totalTime + "s";
    }

    public void setStatsValues(int rescued, int died, int totalTime, bool gameWin)
    {
        nextLevelButton.SetActive(gameWin);
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

    public void NextLevel()
    {
        GameManager.Instance.PlayGame(GameManager.Instance.currentLevel + 1);
    }
}
