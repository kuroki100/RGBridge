using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColetavelGreen : MonoBehaviour
{
    public Text pontuacao;


    // Use this for initialization
    void Start()
    {
        pontuacao = GameObject.Find("Pontuacao").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "green")
        {
            Destroy(this.gameObject, 0.01f);
            PersistentManagerScript.Instance.pontos = PersistentManagerScript.Instance.pontos + 1;
            AtualizarPontos();
        }
    }

    void AtualizarPontos()
    {
        pontuacao.text = "Score: " + PersistentManagerScript.Instance.pontos.ToString();
    }
}
