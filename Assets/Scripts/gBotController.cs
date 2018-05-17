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
  private float rotationSpeed = 180/2; // 180 degrees/2 seconds

  Coroutine rotations = null;
  bool rotationsRunning = false;

  // Use this for initialization
  IEnumerator Start () {
    rb2d = GetComponent<Rigidbody2D>();
    yield return new WaitForSeconds(5);
    rotations = StartCoroutine(RotateBot()); // this will have to be dependent on starting orientation i think
  }

  // Update is called once per frame
  void Update () {
    //transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
  }

  void FixedUpdate() {
    //rb2d.velocity.x = speed.x;
    if(!spinning) {
      newVel.x = speed * Mathf.Cos(rb2d.rotation * 0.0174533f); // convert to radians
      newVel.y = speed * Mathf.Sin(rb2d.rotation * 0.0174533f);
      rb2d.velocity = newVel;
    }
    //print(rb2d.rotation);
  }


  IEnumerator RotateBot() {
    rotationsRunning = true;
    float angle;
    //float rotationSpeed = 180 / 2;
    while (true) {
      yield return new WaitForSeconds(5);

      spinning = true;
      Quaternion startRotation = transform.rotation;
      float dAngle = 0;
      rb2d.velocity = Vector2.zero;

      if(count == 4) {
        angle = 180;
        count = 1;
        } else {
          angle = Random.Range(1.0f, 20.0f);
        }

        while (dAngle < angle) {
          dAngle += rotationSpeed * Time.deltaTime;
          dAngle = Mathf.Min(dAngle, angle);

          transform.rotation = startRotation * Quaternion.AngleAxis(-dAngle, Vector3.forward);

          yield return null;
        }
        spinning = false;
        count++;
      }
    }

    IEnumerator OnCollisionEnter2D(Collision2D coll) {
      //print(coll.gameObject.name);
      StopCoroutine(rotations);
      //float rotationSpeed = 180 / 2;
      spinning = true;
      Quaternion startRotation = transform.rotation;
      float dAngle = 0;
      rb2d.velocity = Vector2.zero;
      rb2d.angularVelocity = 0f;
      while (dAngle < 180) {
        //print("here");
        dAngle += rotationSpeed * Time.deltaTime;
        dAngle = Mathf.Min(dAngle, 180);

        transform.rotation = startRotation * Quaternion.AngleAxis(-dAngle, Vector3.forward);
        yield return null;
      }
      spinning = false;
      rotations = StartCoroutine(RotateBot());
    }

    IEnumerator spinRobot(int angle) {
      print("spinning robot");
      if(rotationsRunning) {
        StopCoroutine(rotations);
      }
      //StopCoroutine(rotations);

      spinning = true;
      Quaternion startRotation = transform.rotation;
      float dAngle = 0;

      rb2d.velocity = Vector2.zero;
      rb2d.angularVelocity = 0f;

      print(dAngle + " vs " + angle);

      while (dAngle < angle) {
        dAngle += rotationSpeed * Time.deltaTime;
        dAngle = Mathf.Min(dAngle, angle);

        transform.rotation = startRotation * Quaternion.AngleAxis(-dAngle, Vector3.forward);
        yield return null;
      }

      spinning = false;
      rotations = StartCoroutine(RotateBot());
    }
  }
