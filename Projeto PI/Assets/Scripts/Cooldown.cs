using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour {

    public GameObject RedShield;
    public GameObject GreenShield;
    public GameObject BlueShield;

    public float sec = 1;



    // Use this for initialization
    void Start()
    {
        StartCoroutine(RedCooldown());

        StartCoroutine(GreenCooldown());

        StartCoroutine(BlueCooldown());
    }


    IEnumerator RedCooldown()
    {
        if (gameObject.activeInHierarchy == true)
        {
            yield return new WaitForSeconds(sec);
            RedShield.gameObject.SetActive(false);
        }
        yield return StartCoroutine(RedCooldown());
    }

    IEnumerator GreenCooldown()
    {
        if (gameObject.activeInHierarchy == true)
        {
            yield return new WaitForSeconds(sec);
            GreenShield.gameObject.SetActive(false);
        }
        yield return StartCoroutine(GreenCooldown());
    }

    IEnumerator BlueCooldown()
    {
        if (gameObject.activeInHierarchy == true)
        {
            yield return new WaitForSeconds(sec);
            BlueShield.gameObject.SetActive(false);
        }
        yield return StartCoroutine(BlueCooldown());
    }


    // Update is called once per frame
    void Update () {
		
	}
}
