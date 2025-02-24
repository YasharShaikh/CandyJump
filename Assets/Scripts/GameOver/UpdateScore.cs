using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour {
    public Text scoreText;
    public Text levelText;
    public Text winLoseText;
    public AudioClip[] audioClips;
    public Sprite happySunTexture;
    public Image sunFace;
    private AudioSource audioSource;

    public ShowAd showAd;
    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("Deaths", 0) == 4)
        {
            showAd = new ShowAd();
            showAd.Ad();
            PlayerPrefs.SetInt("Deaths", 0);
        }
        audioSource = GetComponent<AudioSource>();
        int score = PlayerStats.Score;
        int level = PlayerStats.Level;
        scoreText.text = score.ToString();
        levelText.text = "LEVEL " + level.ToString();
        int lastScore = PlayerPrefs.GetInt("Best_score", 0);

        if(lastScore < score)
        {
            winLoseText.text = "New record!";
            sunFace.sprite = happySunTexture;
            if (PlayerPrefs.GetInt("Sound", 1) == 1)
            {
                audioSource.clip = audioClips[1];
                audioSource.Play();
            }
            PlayerPrefs.SetInt("Best_score", score);
            PlayerPrefs.SetInt("Best_level", level);
        }
        else
        {
            if (PlayerPrefs.GetInt("Sound", 1) == 1)
            {
                audioSource.clip = audioClips[0];
                audioSource.Play();
            }
        }
    }
}
