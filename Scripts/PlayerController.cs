using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Component Variables
    SpriteRenderer spriteRenderer;
    Animator animator;
    public Rigidbody2D rb2d;
    BoxCollider2D boxCollider2D;
    public GameObject player, gunPivot;
    Light2D playerLight;
    SpriteRenderer gunSpriteRenderer;
    //Input Variables
    private float horizontalInput;
    public float jumpInput;
    public float runSpeed = 8.0f;

    //Jump Variables
    //private bool isGrounded = true;
    public float jumpForce = 13.0f;
    public LayerMask ground;

    private float jumpPressedRememberTime = 0.2f;
    private float groundedRememberTime = 0.1f;
    private float cutJumpHeight = 0.5f;

    private float jumpPressedRemember = 0;
    private float groundedRemember = 0;

    float playerMaxHealth = 3f;
    public float playerHealth;

    public GameObject playerParticles;
    public AudioSource die;

    SpriteRenderer reticleSprite;
    
    GameObject gun;



    // Start is called before the first frame update
    void Start()
    {
        reticleSprite = GameObject.Find("Reticle").GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gun = GameObject.Find("Gun");
        gunSpriteRenderer = gun.GetComponent<SpriteRenderer>();
        
        boxCollider2D = GetComponent<BoxCollider2D>();
        gunPivot = GameObject.Find("GunPivot Player");
        playerLight = GameObject.Find("Player Light").GetComponent<Light2D>();
        

        playerHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Inputs
        horizontalInput = Input.GetAxis("Horizontal");

        //Jump Timer Values
        jumpPressedRemember -= Time.deltaTime;

        if(Input.GetButtonDown("Jump")) jumpPressedRemember = jumpPressedRememberTime;

        if(playerHealth == 2) 
        {
            spriteRenderer.color = Color.yellow;
            gunSpriteRenderer.color = Color.yellow;
            playerLight.color = Color.yellow;
            reticleSprite.color = Color.yellow;
        }
        else if(playerHealth == 1)
        {
            spriteRenderer.color = Color.red;
            gunSpriteRenderer.color = Color.red;
            playerLight.color = Color.red;
            reticleSprite.color = Color.red;
        }
        else if(playerHealth <= 0)
        {
            Die();
        }

    }

    private void FixedUpdate()
    {
        //Horizontal Movement
        rb2d.velocity = new Vector2(runSpeed * horizontalInput, rb2d.velocity.y);

        if((jumpPressedRemember > 0) && isGrounded()) {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }

        if(Input.GetButtonUp("Jump")) {
            if(rb2d.velocity.y > 0) {
                rb2d.velocity = new Vector2 (rb2d.velocity.x, rb2d.velocity.y * cutJumpHeight);
            }
        }
    }

    private bool isGrounded() {
        float extraHeightTest = 0.02f;
        RaycastHit2D hit2D = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down, boxCollider2D.bounds.extents.y + extraHeightTest, ground);
 
        return hit2D.collider != null;
    }

    private void Die()
    {
        this.enabled = false;
        spriteRenderer.enabled = false;
        gun.GetComponent<BarrelController>().enabled = false;

        die.Play();
        Destroy(player, die.clip.length);
        Destroy(gunPivot, die.clip.length);

        Instantiate(playerParticles, transform.position, Quaternion.identity);
        
       

        //Particles and Menu
    }
}
