using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

	public GameObject[] CloudsPrefabs;
	public float MinDelay = 3f;
	public float MaxDelay = 10f;
    public float MinSpeed = 0.3f;
    public float MaxSpeed = 1.6f;
    public float MinScal = 0.25f;
    public float MaxScal = 0.7f;
	// Use this for initialization
	void Start () {
		StartCoroutine (Spawner ());
	}

	IEnumerator Spawner(){
		yield return new WaitForSeconds (Random.Range (MinDelay, MaxDelay));
		Spawn ();
	}

	void Spawn(){
        if (Camera.main.transform.position.y > 0)
        {
            GameObject obj = Instantiate(CloudsPrefabs[Random.Range(0, CloudsPrefabs.Length)],
                transform.position, transform.rotation) as GameObject;
            obj.transform.parent = Camera.main.transform;
            float targetScal = Random.Range(MinScal, MaxScal);
            obj.GetComponent<MoveCloud>().moveSpeed = map(targetScal, MinScal, MaxScal, MinSpeed, MaxSpeed);
            obj.transform.localScale = new Vector2(targetScal, targetScal);
        }
		StartCoroutine (Spawner ());
	}

    float map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

}
