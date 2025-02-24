using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlatformsDestroyer : MonoBehaviour {

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Platform" || coll.gameObject.tag == "Broken")
        {
            Destroy(coll.gameObject);
        }
        if (coll.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Scenes/GameOver");
            int deaths = PlayerPrefs.GetInt("Deaths", 0);
            deaths += 1;
            PlayerPrefs.SetInt("Deaths", deaths);
        }
    }
}
