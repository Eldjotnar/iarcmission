using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleBotController : MonoBehaviour {

	public int speed;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.RotateAround(Vector3.zero, Vector3.forward, speed * Time.deltaTime);
	}
}
