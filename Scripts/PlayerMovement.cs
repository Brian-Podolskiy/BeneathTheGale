using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public bool facingRight;
    [SerializeField] private Sprite playerMove;
    [SerializeField] private Sprite playerIdle;
    [SerializeField] private bool isGrounded;
    private bool wishJump;
    private bool moving = false;
    private float ttAnimation = 0.5f;
    private bool spriteStateIdle;
    private int framesToJump = 5;
    private int coyoteFrames = 20;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;


    [SerializeField] private AudioClip move;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip music;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            wishJump = true;

        }
        rb.position += new Vector2 (Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0);
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            facingRight = true;
            moving = true;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            facingRight = false;
            moving = true;
        }
        else
        {
            moving = false;
        } 

        if (facingRight)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (!facingRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        if (wishJump == true)
        {
            framesToJump -= 1;
        }
        if (framesToJump < 0)
        {
            wishJump = false;
            framesToJump = 50;
        }
        if (wishJump == true && coyoteFrames >= 0)
        {
            rb.AddForce(new Vector2 (0, jumpForce));
            source.PlayOneShot(jump, 0.7f);
            coyoteFrames = 0;
            wishJump = false;
        }
        //Debug.Log(coyoteFrames);

        if (isGrounded == false)
        {
            coyoteFrames--;
        }
        if (isGrounded == true)
        {
            coyoteFrames = 5;
        }
        //Debug.Log(Input.GetAxisRaw("Horizontal"));
        if (isGrounded)
        {
            if (moving)
            {
                if (moving && ttAnimation > 0)
                {
                    ttAnimation -= 1 * Time.deltaTime;
                }
                if (ttAnimation <= 0 && spriteStateIdle)
                {
                    GetComponent<SpriteRenderer>().sprite = playerMove;
                    spriteStateIdle = false;
                    ttAnimation = 0.5f;
                    source.PlayOneShot(move, 0.7f);
                }
                if (ttAnimation <= 0 && !spriteStateIdle)
                {
                    GetComponent<SpriteRenderer>().sprite = playerIdle;
                    spriteStateIdle = true;
                    ttAnimation = 0.5f;
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = playerIdle;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = playerMove;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground")) 
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = false;
            
        }
    }

}
