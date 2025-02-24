using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenCandy : MonoBehaviour {
    public Sprite sprite_texture;
    public float jumpForce = 450f;
    EdgeCollider2D edCollider;
    SpriteRenderer sprite;
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        edCollider = GetComponent<EdgeCollider2D>();
	}

    public float getJumpForce()
    {
        return jumpForce;
    }

    public void BrokeIt()
    {
        sprite.sprite = sprite_texture;
        Destroy(edCollider);

    }
}
