using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //move, jump e groundcheck para evitar um flappybird

    public float tempo;

    Rigidbody2D myRB;
    SpriteRenderer myRenderer;
    bool facingRight = true;
    Animator myAnim;
    bool canMove = true;
    public bool grounded = false;

    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float speed = 10;
    public float jumpPower = 7;

    //Imagem dos CCs no Canvas
    public GameObject ccRed;
    public GameObject ccGreen;
    public GameObject ccBlue;

    //escudos
    public GameObject RedShield;
    public GameObject GreenShield;
    public GameObject BlueShield;

    //public cooldowns dos escudos
    public float timeStamp;
    public float delayShield = 1.1f;
    public float activeTimeR = 1;
    public float activeTimeG = 1;
    public float activeTimeB = 1;

    //Knockback publics
    public float knockback = 1000;
    public Rigidbody2D Rm;
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

        //vector2 missil vermelho
        Direction = Rm.transform.position + myRB.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        tempo = Time.time;

        //ground check and jump
        if (grounded && Input.GetAxis("Jump") > 0)
        {
            myAnim.SetBool("isGrounded", false);
            myRB.velocity = new Vector2(myRB.velocity.x, 0f);
            myRB.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            grounded = false;
        }


        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        myAnim.SetBool("isGrounded", grounded);

        //move
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

        //atalhos dos shields + cooldowns das teclas para evitar spam
        if (Input.GetKeyDown(KeyCode.Z) && (Time.time >= timeStamp))
        {
            timeStamp = Time.time + delayShield;
            RedShield.gameObject.SetActive(true);
            StartCoroutine(RedCooldown());
        }
        if (Input.GetKeyDown(KeyCode.X) && (Time.time >= timeStamp))
        {
            timeStamp = Time.time + delayShield;
            GreenShield.gameObject.SetActive(true);
            StartCoroutine(GreenCooldown());
        }
        if (Input.GetKeyDown(KeyCode.C) && (Time.time >= timeStamp))
        {
            timeStamp = Time.time + delayShield;
            BlueShield.gameObject.SetActive(true);
            StartCoroutine(BlueCooldown());
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

    // programação dos ccs e colisão dos misseis com os escudos
    void OnCollisionEnter2D(Collision2D coll)
    {

        //missil vermelho
        if (coll.gameObject.tag == "deadlyRed")
        {
            Debug.Log("Boom");

            myRB.AddForce(Direction * -knockback, ForceMode2D.Impulse);

            RedShield.gameObject.SetActive(false);

            StartCoroutine(KnockbackCooldown());
        }
        else if ((coll.gameObject.tag == "deadlyRed") && (ccRed.activeInHierarchy == true))
        {
            RedShield.gameObject.SetActive(false);
        }

        //Coroutine do cooldown do nockback apenas se o jogador tocar o chão
        //if (grounded && ccRed.activeInHierarchy)
        //{
        //    StartCoroutine(KnockbackCooldown());
        //}

        //missil verde
        if (coll.gameObject.tag == "deadlyGreen")
        {
            Debug.Log("Can't Jump");

            jumpPower = 0;

            GreenShield.gameObject.SetActive(false);

            StartCoroutine(RootCooldown());
        }
        else if ((coll.gameObject.tag == "deadlyGreen") && (GreenShield.activeInHierarchy == true))
        {
            jumpPower = 7;
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
        else if ((coll.gameObject.tag == "deadlyBlue") && (BlueShield.activeInHierarchy == true))
        {
            BlueShield.gameObject.SetActive(false);
        }
    }

    //duração do CC missil vermelho
    IEnumerator KnockbackCooldown()
    {
        if (ccRed.gameObject.activeInHierarchy == true)
        {
            yield return new WaitForSeconds(1);

            //Desativando interface do CC
            ccRed.SetActive(false);
        }
    }

    //duração do CC missil verde
    IEnumerator RootCooldown()
    {
        if (Time.time >= RootDuration)
        {
            yield return new WaitForSeconds(RootDuration);
            jumpPower = 7;

            //Desativando interface do CC
            ccGreen.SetActive(false);
        }

    }

    //duração do CC missil azul
    IEnumerator SlowCooldown()
    {
        if (Time.time >= SlowDuration)
        {
            yield return new WaitForSeconds(SlowDuration);
            speed = 10;

            //Desativando interface do CC
            ccBlue.SetActive(false);
        }
    }

    //Duração dos escudos ativos
    IEnumerator RedCooldown()
    {
        if (RedShield.gameObject.activeInHierarchy == true)
        {
            yield return new WaitForSeconds(activeTimeR);
            RedShield.gameObject.SetActive(false);
        }

    }

    IEnumerator GreenCooldown()
    {
        if (GreenShield.gameObject.activeInHierarchy == true)
        {
            yield return new WaitForSeconds(activeTimeG);
            GreenShield.gameObject.SetActive(false);
        }

    }

    IEnumerator BlueCooldown()
    {
        if (BlueShield.gameObject.activeInHierarchy == true)
        {
            yield return new WaitForSeconds(activeTimeB);
            BlueShield.gameObject.SetActive(false);
        }

    }
}
