using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallRight : MonoBehaviour
{
    Rigidbody2D rigid;
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

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            Destroy(collision.gameObject);
        }
        if ((collision.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0) && (collision.gameObject.tag == "deadlyRed" || collision.gameObject.tag == "deadlyGreen" || collision.gameObject.tag == "deadlyBlue" || collision.gameObject.tag == "OrangeMissile" || collision.gameObject.tag == "BlackMissile"))
        {
            collision.gameObject.SetActive(false);
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
