using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

  public GameObject gRobot;
  public int numElements;
  public float radius;
  public Text scoreText;
  public Text timeText;

  private int score;
  private int timeLeft;

  // Use this for initialization
  void Start () {
    score = 120;
    timeLeft = 600;
        Time.timeScale = 10;

    float step = (2 * Mathf.PI) / numElements;
    float angle = 0;

    for(int i =0; i < numElements; i++) {
      float x = radius * Mathf.Cos(angle);
      float y = radius * Mathf.Sin(angle);
      //print(angle);

      GameObject newObject = (GameObject)Instantiate(gRobot, new Vector3(x, y, 0), Quaternion.Euler(0,0,angle*180/Mathf.PI));
      newObject.name = "gBot"+i.ToString();

      angle += step;
    }
    SetCountText();
    StartCoroutine(LoseTime());
  }

  void SetCountText() {
    scoreText.text = "Score: " + score.ToString();
  }
  // Update is called once per frame
  void Update () {
    timeText.text = "Time: " + timeLeft.ToString();
  }

  IEnumerator LoseTime(){
    while(true) {
      yield return new WaitForSeconds(1);
      timeLeft--;
      if(timeLeft % 60 == 0) {
        score -= 1;
        SetCountText();
      }
    }
  }

  void OnCollisionEnter2D(Collision2D coll) {
    print(coll.gameObject.name);
    if(coll.gameObject.transform.position.x < 4.7 && coll.gameObject.transform.position.x > -4.7) {
      if(coll.gameObject.transform.position.y < 0) {
        score -= 10;
      } else {
        score += 20;
      }
    }
    SetCountText();
    //if(coll.gameObject.transform.position.y < 0 ) {}
    print(coll.gameObject.transform.position.y);
    Destroy(coll.gameObject);
  }
}
