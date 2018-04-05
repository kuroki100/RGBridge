using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackMissile : MonoBehaviour
{
    public GameObject target;
    Rigidbody2D rigid;
    public float speed = 10;
    public float rotateSpeed = 200;
    public float distance;
    public bool travou = false;

    //Pontuação e vida
    public Text pontuacao;
    public Text vidaTexto;

    // Use this for initialization
    void Start()
    {
        pontuacao = GameObject.Find("Pontuacao").GetComponent<Text>();
        vidaTexto = GameObject.Find("Vida").GetComponent<Text>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!travou)
        {
            Vector2 direction = (Vector2)transform.position - (Vector2)target.transform.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction, transform.right).z;

            rigid.velocity = transform.right * speed;
            rigid.angularVelocity = rotateAmount * rotateSpeed;

            distance = Vector3.Distance(transform.position, target.transform.position);
        }
        if (distance <= 5)
        {
            travou = true;
            Vector2 direction = target.transform.position;
            rigid.angularVelocity = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Destroy(this.gameObject, 0.01f);
            if (coll.gameObject.tag == "Player")
            {
                PersistentManagerScript.Instance.vida = PersistentManagerScript.Instance.vida - 1;
                AtualizarVida();
            }
        }
    }

    void AtualizarPontos()
    {
        pontuacao.text = "Score: " + PersistentManagerScript.Instance.pontos.ToString();
    }

    void AtualizarVida()
    {
        vidaTexto.text = "Vida: " + PersistentManagerScript.Instance.vida.ToString();
    }
}
