using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //move, jump e groundcheck para evitar um flappybird

    public float tempo;

    Rigidbody2D myRB;
    //SpriteRenderer myRenderer;
    //bool facingRight = true;
    Animator myAnim;
    bool canMove = true;
    public bool grounded = false;

    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float speed = 10;
    public float jumpPower = 7;
    public int jumpCount = 0;

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
    //Vector2 Direction;

    //slow publics
    public float SlowDuration = 1f;

    //root publics
    public float RootDuration = 1f;

    // Use this for initialization
    void Start()
    {
        // componentes para o move e jump
        myRB = GetComponent<Rigidbody2D>();

        //myRenderer = GetComponent<SpriteRenderer>();  

        myAnim = GetComponent<Animator>();

        //shield inicia desativado.
        RedShield.gameObject.SetActive(false);

        GreenShield.gameObject.SetActive(false);

        BlueShield.gameObject.SetActive(false);

        //vector2 missil vermelho
        //Direction = Rm.transform.position + myRB.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        tempo = Time.time;

        //ground check and jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 1)
        {
            jumpCount++;
            myAnim.SetBool("isGrounded", false);
            myRB.velocity = new Vector2(myRB.velocity.x, 0f);
            myRB.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            grounded = false;
        }

        if (grounded)
        {
            jumpCount = 0;
        }


        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        myAnim.SetBool("isGrounded", grounded);

        //move
        float move = Input.GetAxis("Horizontal");
        if (canMove)
        {
            if (move < 0)
            {
                transform.localScale = new Vector3(-0.25f, 0.25f, z: 0.25f);
            }
            else if (move > 0)
            {
                transform.localScale = new Vector3(0.25f, 0.25f, z: 0.25f);
            }

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

    public void ToggleCanMove()
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

            //myRB.AddForce(Direction * -knockback, ForceMode2D.Impulse);

            Vector2 forca = new Vector2(10, 10);

            myRB.AddForce(forca, ForceMode2D.Impulse);

            speed = 0;

            Destroy(coll.gameObject, 0.01f);

            StartCoroutine(KnockbackCooldown());
        }
        else if ((coll.gameObject.tag == "deadlyRed") && (ccRed.gameObject.GetComponent<Image>().enabled == true))
        {
            RedShield.gameObject.SetActive(false);
        }

        //missil verde
        if (coll.gameObject.tag == "deadlyGreen")
        {
            Debug.Log("Can't Jump");

            jumpPower = 0;

            Destroy(coll.gameObject, 0.01f);

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

            Destroy(coll.gameObject, 0.01f);
        }
        else if ((coll.gameObject.tag == "deadlyBlue") && (BlueShield.activeInHierarchy == true))
        {
            BlueShield.gameObject.SetActive(false);
        }
    }

    //duração do CC missil vermelho
    IEnumerator KnockbackCooldown()
    {
        if (ccRed.gameObject.GetComponent<Image>().enabled == true)
        {
            yield return new WaitForSeconds(1);

            speed = 10;

            //Desativando interface do CC
            ccRed.gameObject.GetComponent<Image>().enabled = false;
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
            ccGreen.gameObject.GetComponent<Image>().enabled = false;
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
            ccBlue.gameObject.GetComponent<Image>().enabled = false;
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
