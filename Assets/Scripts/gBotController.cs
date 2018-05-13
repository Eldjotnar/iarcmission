using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gBotController : MonoBehaviour {
    //private Vector2 speed = new Vector2(1, 0);
    private Rigidbody2D rb2d;
    private Vector2 newVel;
    private float speed = 0.13f;
    private bool spinning = false;
    private int count = 1;

    // Use this for initialization
    IEnumerator Start () {
        rb2d = GetComponent<Rigidbody2D>();
        yield return new WaitForSeconds(5);
        StartCoroutine(RotateBot(90, Vector3.forward, 2)); // this will have to be dependent on starting orientation i think
    }
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }

    void FixedUpdate() {
        //rb2d.velocity.x = speed.x;
        if(!spinning)
        {
            newVel.x = speed * Mathf.Cos(rb2d.rotation * 0.0174533f); // convert to radians
            newVel.y = speed * Mathf.Sin(rb2d.rotation * 0.0174533f);
            rb2d.velocity = newVel;
        }
        //print(rb2d.rotation);
    }

    IEnumerator RotateBot(float angle, Vector3 axis, float inTime)
    {
        float rotationSpeed = angle / inTime;
        while (true)
        {
            spinning = true;
            Quaternion startRotation = transform.rotation;
            float dAngle = 0;
            rb2d.velocity = Vector2.zero;

            if(count == 4)
            {
                angle = 180;
                count = 1;
            } else
            {
                angle = Random.Range(1.0f, 20.0f);
            }

            while(dAngle < angle)
            {
                dAngle += rotationSpeed * Time.deltaTime;
                dAngle = Mathf.Min(dAngle, angle);

                transform.rotation = startRotation * Quaternion.AngleAxis(-dAngle, axis);

                yield return null;
            }
            spinning = false;
            count++;

            yield return new WaitForSeconds(5);
        }
    }
}
