using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrituradorScript : MonoBehaviour
{
    Rigidbody2D rigid;
    float speed = 1;
    public GameObject player;
    public PlayerController playerScript;
    public Text pontuacao;
    public Text vidaTexto;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        AtualizarPontos();
        AtualizarVida();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = new Vector2(0, 1) * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "deadlyRed" || collision.gameObject.tag == "deadlyGreen" || collision.gameObject.tag == "deadlyBlue" || collision.gameObject.tag == "OrangeMissile" || collision.gameObject.tag == "BlackMissile")
        {
            Destroy(collision.gameObject);
            PersistentManagerScript.Instance.pontos = PersistentManagerScript.Instance.pontos + 1;
            AtualizarPontos();
        }
        if (collision.gameObject.tag == "Player")
        {
            PersistentManagerScript.Instance.triturado = true;
            AtualizarVida();
            AtualizarPontos();
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
