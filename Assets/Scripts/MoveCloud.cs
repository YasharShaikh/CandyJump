using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloud : MonoBehaviour {
    public float moveSpeed = 1f;
	void Update () {
        transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
        transform.Translate(Vector2.down * Time.deltaTime * moveSpeed);
    }
}
