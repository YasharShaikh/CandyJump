using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Character : MonoBehaviour  {

	[HideInInspector] public bool facingRight = true;
    public Transform camTransform;
    public Transform groundCheck;
    public Text scoreText;

    private Animator animator;
    private Rigidbody2D rb2d;

	public float maxSpeed = 5f;
	public float jumpForce = 450f;
    public float jumpTimeLimit = 0.1f;

    private float horizontal;
    private float totalTimeForJump;
    private float flipTimer = 0f;

    private float lastPosition_Y;
    private bool playerIsUp = false;
    private int scorePoints;

    public float dist = 10f;

    private AudioSource audioSource;
    private bool sound = true;
    private bool isJump = false;
	void Awake () 
	{
        animator = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D>();
	}
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            sound = true;
        }
        else sound = false;

        scorePoints = Mathf.RoundToInt(lastPosition_Y);
        lastPosition_Y = gameObject.transform.position.y;
        if (lastPosition_Y >= 0)
        {
            scoreText.text = scorePoints.ToString();
            PlayerStats.Score = scorePoints;

        }
        else
        {
            scoreText.text = "0";
        }
        
    }
    private void Update()
    {
        totalTimeForJump += Time.deltaTime;
        flipTimer += Time.deltaTime;
    }
    void FixedUpdate()
	{
        
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 bottomRight = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, 0));
        

        if (gameObject.transform.position.y >= lastPosition_Y)
        {
            lastPosition_Y = gameObject.transform.position.y;
            if (lastPosition_Y >= 0)
            {
                scoreText.text = Mathf.RoundToInt(lastPosition_Y).ToString();
                PlayerStats.Score = Mathf.RoundToInt(lastPosition_Y); ;
            }
            else
            {
                scoreText.text = "0";
            }
            
            playerIsUp = true;
        }
        else
        {
            playerIsUp = false;
        }
        

        if (Application.platform == RuntimePlatform.Android) {
			horizontal = Input.acceleration.x * 2.0f;
		} else {
			horizontal = Input.GetAxis("Horizontal");
		}
        if (!isJump)
        {
            animator.SetFloat("Running", Mathf.Abs(horizontal));
        }
        float charPosX = transform.position.x;
        if (charPosX < bottomLeft.x)
        {
            transform.position = new Vector2(bottomLeft.x, transform.position.y);
        }else if (charPosX > bottomRight.x)
        {
            transform.position = new Vector2(bottomRight.x, transform.position.y);
        }
        rb2d.linearVelocity = new Vector2(horizontal * maxSpeed, rb2d.linearVelocity.y);
		
		if (horizontal > 0 && !facingRight)
			Flip ();
		else if (horizontal < 0 && facingRight)
			Flip ();
		
	}

    public void Jump(float jumpForce)
    {
        rb2d.AddForce(new Vector2(0f, jumpForce));
    }
   
    private void OnCollisionStay2D(Collision2D coll)
    {
        bool grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("platform"));
        
        if ((coll.gameObject.tag == "Platform" || coll.gameObject.tag == "Broken")  
             && !playerIsUp && totalTimeForJump >= jumpTimeLimit && grounded)
        {
            animator.SetBool("Jump", true);
            isJump = true;

            if (sound) audioSource.Play();
            float jumpForce = 0;
            if (coll.gameObject.tag == "Platform")
            {
                jumpForce = coll.gameObject.GetComponent<MovePlatform>().getJumpForce();
            }
            if (coll.gameObject.tag == "Broken")
            {
                coll.gameObject.GetComponent<BrokenCandy>().BrokeIt();
                jumpForce = coll.gameObject.GetComponent<BrokenCandy>().getJumpForce();
            }
            Jump(jumpForce);
            totalTimeForJump = 0f;
        }
    }
    void Flip()
	{
        if (flipTimer >= 0.1f)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            flipTimer = 0f;
        }
	}
}
