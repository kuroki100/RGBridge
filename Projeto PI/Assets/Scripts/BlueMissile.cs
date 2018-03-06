using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueMissile : MonoBehaviour
{

    public float speed = 5;
    public float rotatingSpeed = 200;
    GameObject target;

    Rigidbody2D rb;

    //Acessando script do missil vermelho
    RedMissile script;

    //Pontuação
    public Text pontuacao;
    public Text vidaTexto;

    //Imagem do CC no Canvas
    public GameObject ccInterface;

    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        //Acessando script do missil vermelho
        script = GameObject.Find("RedBullet").GetComponent<RedMissile>();
        AtualizarPontos();
        AtualizarVida();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;

        point2Target.Normalize();

        float value = Vector3.Cross(point2Target, transform.right).z;

        rb.angularVelocity = rotatingSpeed * value;

        rb.velocity = transform.right * speed;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Destroy(this.gameObject, 0.05f);
            if (coll.gameObject.tag == "Player")
            {
                ccInterface.SetActive(true);
                PersistentManagerScript.Instance.vida = PersistentManagerScript.Instance.vida - 1;
                AtualizarVida();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "blue")
        {
            Destroy(this.gameObject, 0.02f);
            PersistentManagerScript.Instance.pontos = PersistentManagerScript.Instance.pontos + 1;
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
