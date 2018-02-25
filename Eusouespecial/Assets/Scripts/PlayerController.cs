using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //move

    Rigidbody2D myRB;
    SpriteRenderer myRenderer;
    bool facingRight = true;
    Animator myAnim;
    bool canMove = true;
    public float speed = 10;

    //jump e grounded check

    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpPower = 7;

    //escudos

    public GameObject RedShield;
    public GameObject GreenShield;
    public GameObject BlueShield;


    //cooldown dos escudos

    public float delayRed = 1;
    public float delayGreen = 1;
    public float delayBlue = 1;
    float timeStamp;
    public float sec = 1;

    //Knockback publics
    public GameObject RedMissile;

    public float knockback = 1000;

    Vector2 Direction;


    //slow publics
    public float SlowDuration = 1f;

    //root publics
    public float RootDuration = 1f;

    // Use this for initialization
    void Start()
    {

        // componentes para o move e jump

        myRB = GetComponent<Rigidbody2D>();

        myRenderer = GetComponent<SpriteRenderer>();

        myAnim = GetComponent<Animator>();

        //shield inicia desativado.

        RedShield.gameObject.SetActive(false);

        GreenShield.gameObject.SetActive(false);

        BlueShield.gameObject.SetActive(false);

        //Co routine dos cooldowns

        StartCoroutine(RedCooldown());

        StartCoroutine(GreenCooldown());

        StartCoroutine(BlueCooldown());
    }


    // Update is called once per frame
    void Update()
    {
        //vector2 knockback

        Direction = RedMissile.transform.position - myRB.transform.position;

        //jump and move
        if (grounded && Input.GetAxis("Jump") > 0)
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

        //atalhos dos shields + cooldows das teclas para evitar spam

        if (Input.GetKeyDown(KeyCode.Z) && (Time.time >= timeStamp))
        {
            RedShield.gameObject.SetActive(true);
            timeStamp = Time.time + delayRed;
        }
        if (Input.GetKeyDown(KeyCode.X) && (Time.time >= timeStamp))
        {
            GreenShield.gameObject.SetActive(true);
            timeStamp = Time.time + delayGreen;
        }
        if (Input.GetKeyDown(KeyCode.C) && (Time.time >= timeStamp))
        {
            BlueShield.gameObject.SetActive(true);
            timeStamp = Time.time + delayBlue;
        }
    }

    // programação dos ccs e colisão dos misseis com os escudos

    void OnCollisionEnter2D(Collision2D coll)
    {

        //missil vermelho

        if (coll.gameObject.tag == "deadlyRed")
        {
            Debug.Log("Boom");
            RedShield.gameObject.SetActive(false);

            myRB.AddForce(Direction.normalized * -knockback);
        }
        else if (RedShield.gameObject.activeInHierarchy == true)
        {
            RedShield.gameObject.SetActive(false);
        }

        //missil verde

        if (coll.gameObject.tag == "deadlyGreen")
        {
            Debug.Log("Can't Jump");

            jumpPower = 0;

            GreenShield.gameObject.SetActive(false);

            StartCoroutine(RootCooldown());
        }
        else if (GreenShield.gameObject.activeInHierarchy == true)
        {
            GreenShield.gameObject.SetActive(false);   
        }

        //missil azul

        if (coll.gameObject.tag == "deadlyBlue")
        {
            Debug.Log("Slow");

            speed = speed * 0.5f;

            StartCoroutine(SlowCooldown());

            BlueShield.gameObject.SetActive(false);
        }
        else if (BlueShield.gameObject.activeInHierarchy == true)
        {
            BlueShield.gameObject.SetActive(false);
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

    //duração do CC missil verde

    IEnumerator RootCooldown()
    {
        if (Time.time >= RootDuration)
        {
            yield return new WaitForSeconds(RootDuration);
            jumpPower = 7;
        }
        
    }

    //duração do CC missil azul

    IEnumerator SlowCooldown()
    {
        if (Time.time >= SlowDuration)
        {
            yield return new WaitForSeconds(SlowDuration);
            speed = 10;
        }
    }

    //cooldown dos escudos quando ativados

    IEnumerator RedCooldown()
    {
        if (gameObject.activeInHierarchy == true)
        {
            yield return new WaitForSeconds(sec);
            RedShield.gameObject.SetActive(false);
        }
        yield return StartCoroutine(RedCooldown());
    }

    IEnumerator GreenCooldown()
    {
        if (gameObject.activeInHierarchy == true)
        {
            yield return new WaitForSeconds(sec);
            GreenShield.gameObject.SetActive(false);
        }
        yield return StartCoroutine(GreenCooldown());
    }

    IEnumerator BlueCooldown()
    {
        if (gameObject.activeInHierarchy == true)
        {
            yield return new WaitForSeconds(sec);
            BlueShield.gameObject.SetActive(false);
        }
        yield return StartCoroutine(BlueCooldown());
    }
}
