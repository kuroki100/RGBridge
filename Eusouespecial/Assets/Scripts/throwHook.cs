using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwHook : MonoBehaviour {

    public GameObject hook;

    public bool ropeActive;

    GameObject curHook;

    public float RopeDuration = 1;

    // Use this for initialization
    void Start() {

        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && GameObject.FindGameObjectWithTag("CanHook"))
        {
            if (ropeActive == false)
            {
                Vector2 destiny = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                curHook = Instantiate(hook, transform.position, Quaternion.identity);

                curHook.GetComponent<RopeScript>().destiny = destiny;

                ropeActive = true;
            }
            else
            {
                StartCoroutine(RopeCooldown());
                ropeActive = false;
            }
        }
        
    }

    IEnumerator RopeCooldown()
    {
        if (ropeActive == true)
        {
            yield return new WaitForSeconds(RopeDuration);
            Destroy(curHook);            
        }
    }
}
