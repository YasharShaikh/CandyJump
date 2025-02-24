using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	
	
    public void Play()
    {
        SceneManager.LoadScene("Scenes/mainGame");
    }
    public void Home()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }
    public void Shop()
    {
        SceneManager.LoadScene("Scenes/Shop");
    }
}
