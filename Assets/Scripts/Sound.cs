using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour {
    public Toggle toggleSound;
    void Start()
    {
        Toggle toggle = GetComponent<Toggle>();
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            toggle.isOn = false;
        }
        else
        {
            toggle.isOn = true;
        }
        toggleSound.onValueChanged.AddListener(delegate {soundOffOn();});
    }
	void Update () {
		
	}

    public void soundOffOn()
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
    }
}
