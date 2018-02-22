using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour {

    public Transform target;
    public float smoothing = 5f;
    public float boundX = 5.2f;
    public float boundY = 5.2f;

    Vector3 offset;



	// Use this for initialization
	void Start () {
        offset = transform.position - target.position;
		
	}
	
	void FixedUpdate () {

        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        //X Axis
        float dx = target.position.x - transform.position.x;

        if (dx > boundX || dx < -boundX)
        {
            if (transform.position.x < target.position.x)
            {
                delta.x = dx - boundX;
            }
            else
            {
                delta.x = dx + boundX;
            }
        }

        //Y Axis
        float dy = target.position.y - transform.position.y;

        if (dy > boundY || dy < -boundY)
        {
            if (transform.position.y < target.position.y)
            {
                delta.y = dy - boundY;
            }
            else
            {
                delta.y = dy + boundY;
            }
        }

    }

}
