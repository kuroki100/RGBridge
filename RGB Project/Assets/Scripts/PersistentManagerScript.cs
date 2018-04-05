using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManagerScript : MonoBehaviour
{

    public static PersistentManagerScript Instance { get; private set; }

    public int pontos = 0;
    public int vida = 5;

    public int pontosVitoriaDerrota = 0;

    public int pontos1 = 0;

    public int pontos2 = 0;

    public int pontos3 = 0;

    public bool triturado = false;

    public bool chegouNoFinal = false;

    int max = 3;
    int ini = 0;
    int n = 0;
    public int[] scoreboardPontos = new int[3];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        vida = 5;
        Inserir(0);
        Inserir(0);
        Inserir(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Inserir(int e)
    {
        int fim;
        fim = ((ini + n) % max);
        scoreboardPontos[fim] = e;
        n++;
    }
}