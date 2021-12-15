using System;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    public Button levelButtonPrefab;
    private float offset = 0;

    void Start()
    {
        int numOfLevels = GameManager.Instance.levels.Count;
        for (int index = 0; index < numOfLevels; index++)
        {
            Button button = Instantiate(levelButtonPrefab);
            button.GetComponentInChildren<Text>().text = "Level " + (index + 1);
            button.name = "Level " + (index + 1);
            button.onClick.AddListener(() => StartLevel(button));
            button.transform.SetParent(transform, false);
            Vector3 position = button.transform.position;
            position.y -= offset;
            button.transform.position = position;
            offset += 50f;
        }
    }

    void StartLevel(Button button)
    {
        int numberIndex = button.name.IndexOfAny("0123456789".ToCharArray());
        int levelNumber = Int32.Parse(button.name.Substring(numberIndex)); //my eyes are burning

        GameObject audioObject = GameObject.FindGameObjectWithTag("Audio");
        audioObject.GetComponent<AudioSource>().Stop();

        GameManager.Instance.PlayGame(levelNumber - 1);
    }
}
