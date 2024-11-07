using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI top1Text;
    public TextMeshProUGUI top2Text;
    public TextMeshProUGUI top3Text;

    public void SaveNewScore(int newScore)
    {
        List<int> highScores = new List<int>();

        for (int i = 1; i <= 3; i++)
        {
            highScores.Add(PlayerPrefs.GetInt("HighScore" + i, 0));
        }

        highScores.Add(newScore);

        highScores.Sort((a, b) => b.CompareTo(a));

        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetInt("HighScore" + (i + 1), highScores[i]);
        }

        PlayerPrefs.Save();
        UpdateScoreDisplay();
    }

    public void UpdateScoreDisplay()
    {
        int top1 = PlayerPrefs.GetInt("HighScore1", 0);
        int top2 = PlayerPrefs.GetInt("HighScore2", 0);
        int top3 = PlayerPrefs.GetInt("HighScore3", 0);

        top1Text.text = "1st: " + top1.ToString();
        top2Text.text = "2nd: " + top2.ToString();
        top3Text.text = "3rd: " + top3.ToString();
    }

    private void Start()
    {
        UpdateScoreDisplay();
    }
}
