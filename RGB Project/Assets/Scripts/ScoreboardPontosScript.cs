﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardPontosScript : MonoBehaviour
{
    public Text pontuacao1;
    public Text pontuacao2;
    public Text pontuacao3;

    // Use this for initialization
    void Start()
    {
        PersistentManagerScript.Instance.Inserir(PersistentManagerScript.Instance.pontosVitoriaDerrota);
        AtualizarPontos();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AtualizarPontos()
    {
        pontuacao1.text = "Score 1: " + PersistentManagerScript.Instance.scoreboardPontos[0].ToString();
        pontuacao2.text = "Score 1: " + PersistentManagerScript.Instance.scoreboardPontos[1].ToString();
        pontuacao3.text = "Score 1: " + PersistentManagerScript.Instance.scoreboardPontos[2].ToString();
    }
}
