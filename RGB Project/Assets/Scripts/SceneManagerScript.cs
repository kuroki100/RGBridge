using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerScript : MonoBehaviour
{
    public GameObject boataoMenuPopup;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PersistentManagerScript.Instance.vida <= 0)
        {
            PersistentManagerScript.Instance.pontosVitoriaDerrota = PersistentManagerScript.Instance.pontos;
            PersistentManagerScript.Instance.Inserir(PersistentManagerScript.Instance.pontos);
            PersistentManagerScript.Instance.pontos = 0;
            PersistentManagerScript.Instance.vida = 5;
            CarregarMenuDerrota();
        }

        if (PersistentManagerScript.Instance.chegouNoFinal)
        {
            PersistentManagerScript.Instance.pontosVitoriaDerrota = PersistentManagerScript.Instance.pontos;
            PersistentManagerScript.Instance.Inserir(PersistentManagerScript.Instance.pontos);
            PersistentManagerScript.Instance.pontos = 0;
            PersistentManagerScript.Instance.vida = 5;
            PersistentManagerScript.Instance.chegouNoFinal = false;
            CarregarMenuVitoria();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                boataoMenuPopup.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                boataoMenuPopup.SetActive(false);
            }
        }

        if (PersistentManagerScript.Instance.triturado)
        {
            PersistentManagerScript.Instance.triturado = false;
            PersistentManagerScript.Instance.pontosVitoriaDerrota = PersistentManagerScript.Instance.pontos;
            PersistentManagerScript.Instance.pontos = 0;
            PersistentManagerScript.Instance.vida = 5;
            CarregarMenuDerrota();
        }
    }

    public void CarregarMenuInicial()
    {
        SceneManager.LoadScene("MenuInicial");
    }

    public void CarregarStoryboard()
    {
        SceneManager.LoadScene("Scoreboard");
    }

    public void CarregarMenuVitoria()
    {
        SceneManager.LoadScene("MenuVitoria");
    }

    public void CarregarMenuDerrota()
    {
        SceneManager.LoadScene("MenuDerrota");
    }

    public void CarregarCena01()
    {
        SceneManager.LoadScene("Cena01");
        Time.timeScale = 1;
        PersistentManagerScript.Instance.pontos = 0;
        PersistentManagerScript.Instance.vida = 5;
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }
}
