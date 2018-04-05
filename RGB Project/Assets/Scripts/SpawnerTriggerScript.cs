using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTriggerScript : MonoBehaviour
{
    public GameObject missilePrefab;
    public Transform spawner;
    public float delayMissiles = 0.5f;
    public int missileNumer = 10;
    bool activated = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !activated)
        {
            activated = true;
            StartCoroutine("SpawnMissiles");
        }
    }

    IEnumerator SpawnMissiles()
    {
        for (int i = 0; i <= missileNumer - 1; i++)
        {
            Instantiate(missilePrefab, spawner.position, spawner.rotation);
            yield return new WaitForSeconds(delayMissiles);
        }
    }
}
