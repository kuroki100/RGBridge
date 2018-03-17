using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManagerScript : MonoBehaviour
{

    public static PersistentManagerScript Instance { get; private set; }

    public int pontos = 0;
    public int vida = 3;

    public int pontos1 = 0;

    public int pontos2 = 0;

    public int pontos3 = 0;

    public bool triturado = false;

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
        vida = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
