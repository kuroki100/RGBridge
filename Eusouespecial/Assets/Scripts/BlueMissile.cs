using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueMissile : MonoBehaviour {


    public float speed = 5;
    public float rotatingSpeed = 200;
    public GameObject target;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "blue")
        {
            Destroy(this.gameObject, 0.02f);
        }
    }
}
