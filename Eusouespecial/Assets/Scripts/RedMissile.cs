using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMissile : MonoBehaviour
{


    public float speed = 5;
    public float rotatingSpeed = 200;
    GameObject target;
    Rigidbody2D rb;


    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

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
            Destroy(this.gameObject, 0.01f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "red")
        {
            Destroy(this.gameObject, 0.05f);
        }
    }
}
