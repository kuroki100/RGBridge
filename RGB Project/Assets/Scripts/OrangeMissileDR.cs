using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrangeMissileDR : MonoBehaviour
{
    Rigidbody2D rigid;
    float speed = 10;

    //Pontuação e vida
    public Text pontuacao;
    public Text vidaTexto;

    // Use this for initialization
    void Start()
    {
        pontuacao = GameObject.Find("Pontuacao").GetComponent<Text>();
        vidaTexto = GameObject.Find("Vida").GetComponent<Text>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = new Vector2(1, -1) * speed;
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
