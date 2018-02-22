using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody2D myRB;
    SpriteRenderer myRenderer;
    bool facingRight = true;
    Animator myAnim;
    bool canMove = true;

    //move

    public float speed;

    //jump

    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpPower;

    //escudos
    public GameObject shield1;
    public GameObject shield2;
    public GameObject shield3;


    //cooldown dos escudos

    public float delayRed = 1;
    public float delayGreen = 1;
    public float delayBlue = 1;
    float timeStamp;


    // Use this for initialization
    void Start () {
        myRB = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();

        //shield inicia desativado.

        shield1.gameObject.SetActive(false);
        shield2.gameObject.SetActive(false);
        shield3.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update () {

        if(grounded && Input.GetAxis("Jump") > 0)
        {
            myAnim.SetBool("isGrounded", false);
            myRB.velocity = new Vector2(myRB.velocity.x, 0f);
            myRB.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            grounded = false;
        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        myAnim.SetBool("isGrounded", grounded);


        float move = Input.GetAxis("Horizontal");
        if (canMove)
        {
            if (move > 0 && !facingRight)
                Flip();
            else if (move < 0 && facingRight)
                Flip();

            myRB.velocity = new Vector2(move * speed, myRB.velocity.y);
            myAnim.SetFloat("MoveSpeed", Mathf.Abs(move));
        }
        else
        {
            myRB.velocity = new Vector2(0, myRB.velocity.y);
            myAnim.SetFloat("MoveSpeed", 0);
        }

        //atalhos dos shields
        if (Input.GetKeyDown(KeyCode.Q) && (Time.time >= timeStamp))
        {
            shield1.gameObject.SetActive(true);
            timeStamp = Time.time + delayRed;
        }
        if (Input.GetKeyDown(KeyCode.E) && (Time.time >= timeStamp))
        {
            shield2.gameObject.SetActive(true);
            timeStamp = Time.time + delayGreen;
        }
        if (Input.GetKeyDown(KeyCode.R) && (Time.time >= timeStamp))
        {
            shield3.gameObject.SetActive(true);
            timeStamp = Time.time + delayBlue;

        }
    }

    //se atingido por um collider com a tag deadly o shield é desativado

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "deadlyRed")
        {
            shield1.gameObject.SetActive(false);
        }

        if (other.tag == "deadlyGreen")
        {
            shield2.gameObject.SetActive(false);
        }

        if (other.tag == "deadlyBlue")
        {
            shield3.gameObject.SetActive(false);
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        myRenderer.flipX = !myRenderer.flipX;

    }
    public void toggleCanMove()
    {
        canMove = !canMove;
    }
}
