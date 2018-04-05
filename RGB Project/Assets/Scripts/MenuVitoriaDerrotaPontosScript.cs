using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuVitoriaDerrotaPontosScript : MonoBehaviour
{

    public Text pontuacao;

    // Use this for initialization
    void Start()
    {
        AtualizarPontos();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AtualizarPontos()
    {
        pontuacao.text = "Score: " + PersistentManagerScript.Instance.pontosVitoriaDerrota.ToString();
    }
}
