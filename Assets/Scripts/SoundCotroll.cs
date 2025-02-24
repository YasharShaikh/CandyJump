using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundCotroll : MonoBehaviour {
    private AudioSource audioSource;
    void Start () {
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            audioSource.loop = true;
            audioSource.Play();        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
        }
    }
	void Update () {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            if (!audioSource.isPlaying) audioSource.Play();
           
        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
        }
        
    }

    
}
