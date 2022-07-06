using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    private int highScore;

    private void Start()
    {
       
    }
    public int GetHighScore()
    {
        if (PlayerPrefs.HasKey("highscore"))
        {
            highScore = PlayerPrefs.GetInt("highscore");
        }
        return highScore; 
    }
    public void NewHighRecord(Text currentPointText, Text infoText)
    {
        highScore = GetHighScore();
        if (int.Parse(currentPointText.text) > highScore)
        {
            currentPointText.gameObject.SetActive(false);
            infoText.gameObject.SetActive(true);
            infoText.text = "New Record: " + currentPointText.text;
            highScore = int.Parse(currentPointText.text);
            PlayerPrefs.SetInt("highscore", highScore);
        }
    }
}
