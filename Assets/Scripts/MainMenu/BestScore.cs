using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour {
    public Text scoreText;
    public Text levelText;
    public Text textBestScore;
    // Use this for initialization
    void Start () {
        int score = PlayerPrefs.GetInt("Best_score", 0);
        int level = PlayerPrefs.GetInt("Best_level", 0);
        if (score > 0)
        {
            textBestScore.text = "BEST SCORE:";
            scoreText.text = score.ToString();
            levelText.text = "LEVEL " + level.ToString();
        }
        else
        {
            textBestScore.text = "";
            scoreText.text = "";
            levelText.text = "";
        }
    }
}
