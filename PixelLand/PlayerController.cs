using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController instance;
    public SpriteRenderer playerSprite;
    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D rb;
    private bool doubleJump;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private Animator anim;
    public Vector3 respawnPosition;
    private LevelManager lm;
    public bool onPlatform;
    public GameObject stompBox;
    public float knockBackforce = 5f;
    public float knockBackLenght = 0.12f;
    private float knockBackCounter;
    public float invincibleLenght = 1.5f;
    private float invincibleCounter;
    public AudioSource jumpSound;
    public AudioSource hurtSound;
    public AudioSource pickUpSound;
    public AudioSource powerUp;
    private Vector3 theScale;
    public bool canMove;

    private void Awake()
    {
        instance = this;
        theScale = transform.localScale;
        
    }
    // Use this for initialization
    void Start () {
        canMove = true;
        LevelManager.instance.invincible = false;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        respawnPosition = transform.position;
        lm = FindObjectOfType<LevelManager>();
	}
    
	
	// Update is called once per frame
	void Update () {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (knockBackCounter <= 0&&canMove)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);

            }
            else
            {
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, 0f);
                jumpSound.Play();

            }
        }
        if(knockBackCounter>0)
        {
            knockBackCounter -= Time.deltaTime;
            

            if (transform.localScale.x > 0)
            {
                rb.velocity = new Vector3(-knockBackforce, knockBackforce, 0f);
                
            }
            else
            {
                rb.velocity = new Vector3(knockBackforce, knockBackforce, 0f);
            }
        }
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("Grounded", isGrounded);
        if (rb.velocity.y < 0)
        {
            stompBox.SetActive(true);
        }
        else
        {
            stompBox.SetActive(false);
        }
        if (invincibleCounter > 0)
        {
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.5f);
            invincibleCounter -= Time.deltaTime;
        }
        if (invincibleCounter <= 0)
        {
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            LevelManager.instance.invincible = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "KillZone")
        {
            // gameObject.SetActive(false);

            lm.respawn();

        }
        if (other.tag == "CheckPoint")
        {
            respawnPosition = other.transform.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = other.transform;
            onPlatform = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
            onPlatform = false;
        }
    }
    public void resetScale()
    {
        transform.localScale = theScale;
        
    }

    public void KnockBack()
    { 
        
        hurtSound.Play();
        
            knockBackCounter = knockBackLenght;
            invincibleCounter = invincibleLenght;
            LevelManager.instance.invincible = true;
        
    }
   
}
