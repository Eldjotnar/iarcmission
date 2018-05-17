﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
public class Drone_naive : MonoBehaviour {
	//string mode = "naive";
	public float speed;
	private GameObject target;
	bool tapped = false;

	//private Collider droneCollider;
	// Use this for initialization
	void Start () {

		//droneCollider = GetComponent<Collider>();
		//droneCollider.enabled = false
	}

	// Update is called once per frame
	void Update () {

		findBot("gBot0");
		if(target) {
			Vector3 diff = target.transform.position - transform.position;
			if(!tapped && diff.magnitude < 0.01) {
				print("Tapping!");
				target.SendMessage("spinRobot", 45);
				tapped = true;
			} else if(!tapped) {
				diff.Normalize();

	      float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
	      transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
				transform.position += transform.up * speed * Time.deltaTime;
			}
			//print(diff.magnitude);

		}
	}

	void findBot(string b){
		target = GameObject.Find(b);
	}
//Not used right now...
	void findAll(){
		var foundObjects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name.Contains("gBot"));
		if(foundObjects.Count() > 0){
			foreach(GameObject b in foundObjects){
				Debug.Log(b + " : " + b.transform.position + "--> " + b.name);
			}
		}
	}
}
