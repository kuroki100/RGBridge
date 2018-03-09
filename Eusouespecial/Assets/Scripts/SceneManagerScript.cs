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
            PersistentManagerScript.Instance.pontos1 = PersistentManagerScript.Instance.pontos;
            PersistentManagerScript.Instance.pontos = 0;
            PersistentManagerScript.Instance.vida = 3;
            CarregarMenuDerrota();
        }

        if (PersistentManagerScript.Instance.pontos >= 3)
        {
            PersistentManagerScript.Instance.pontos1 = PersistentManagerScript.Instance.pontos;
            PersistentManagerScript.Instance.pontos = 0;
            PersistentManagerScript.Instance.vida = 3;
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
        PersistentManagerScript.Instance.vida = 3;
    }

    public void SairDoJogo()
    {
        Application.Quit();
    }
}
