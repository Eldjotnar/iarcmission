using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gBotController : MonoBehaviour {
  private Rigidbody2D rb2d;
  private Vector2 newVel;
  private float speed = 0.13f;
  public bool spinning = false;
  private int count = 1;
  private float rotationSpeed = 180/2; // 180 degrees/2 seconds

  Coroutine rotations = null;
  bool rotationsRunning = false;

  // initialization
  void Start () {
    rb2d = GetComponent<Rigidbody2D>();
    rotations = StartCoroutine(trajNoise());
  }

  // move position every frame
  void FixedUpdate() {
    if(!spinning) {
      newVel.x = speed * Mathf.Cos(rb2d.rotation * 0.0174533f); // convert to radians
      newVel.y = speed * Mathf.Sin(rb2d.rotation * 0.0174533f);
      rb2d.velocity = newVel;
    }
  }

  // rotate randomly every 5 seconds
  IEnumerator trajNoise() {
    float angle;
    rotationsRunning = true;
    while (true) {
      yield return new WaitForSeconds(5);

      if(count == 4) {
        angle = 180;
        count = 1;
      } else {
        angle = Random.Range(1.0f, 20.0f);
      }

      StartCoroutine(spinRobot((int)angle));
      count++;
    }
  }

  // if the robot hits something, turn around
  void OnCollisionEnter2D(Collision2D coll) {
    StartCoroutine(spinRobot(180));
  }

  // rotate robot around to certain heading
  IEnumerator spinRobot(int angle) {
    if(rotationsRunning) {
      StopCoroutine(rotations);
    }

    spinning = true;
    Quaternion startRotation = transform.rotation;
    float dAngle = 0;

    // stop the velocity to avoid moving in circles
    rb2d.velocity = Vector2.zero;
    rb2d.angularVelocity = 0f;

    // actually animate the rotation
    while (dAngle < angle) {
      dAngle += rotationSpeed * Time.deltaTime;
      dAngle = Mathf.Min(dAngle, angle);

      transform.rotation = startRotation * Quaternion.AngleAxis(-dAngle, Vector3.forward);
      yield return null;
    }

    // resume regular movement
    spinning = false;
    rotations = StartCoroutine(trajNoise());
  }
}

/* 
IEnumerator spinRobotFromDrone(int angle) {
  if(spinning){
    yield return null;
  }
    if(rotationsRunning) {
      StopCoroutine(rotations);
    }

    spinning = true;
    Quaternion startRotation = transform.rotation;
    float dAngle = 0;

    // stop the velocity to avoid moving in circles
    rb2d.velocity = Vector2.zero;
    rb2d.angularVelocity = 0f;

    // actually animate the rotation
    while (dAngle < angle) {
      dAngle += rotationSpeed * Time.deltaTime;
      dAngle = Mathf.Min(dAngle, angle);

      transform.rotation = startRotation * Quaternion.AngleAxis(-dAngle, Vector3.forward);
      yield return null;
    }

    // resume regular movement
    spinning = false;
    rotations = StartCoroutine(trajNoise());
  }
}
 */

