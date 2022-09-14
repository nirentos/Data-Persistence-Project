using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameUpdater : MonoBehaviour
{
    public TMP_Text inputFieldText;
    public string curText;
    public TMP_Text[] Scores;

    private void Start()
    {
        for (int i = 0; i < Scores.Length; i++)
        {
            Scores[i].text = HighScoreManager.Instance.top5Players[i] + ": " + HighScoreManager.Instance.top5PlayerScores[i];
        }
    }
    private void Update()
    {
        if (inputFieldText.text != curText && inputFieldText.text != null)
        {
            curText = inputFieldText.text;
            HighScoreManager.Instance.playerName = curText;
        }
        else if (curText != "Default Player" && (inputFieldText.text == "" || inputFieldText.text != null))
        {
            curText = "Default Player";
            HighScoreManager.Instance.playerName = curText;
        }
    }
}
