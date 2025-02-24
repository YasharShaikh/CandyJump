using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour {
    public float jumpForce = 550f;
    public GameObject character;

    public float getJumpForce()
    {
        return jumpForce;
    }
}
