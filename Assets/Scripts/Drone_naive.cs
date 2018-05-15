using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
public class Drone_naive : MonoBehaviour {
	//string mode = "naive";
	public float speed;
	private Transform target;

	//private Collider droneCollider;
	// Use this for initialization
	void Start () {
		
		//droneCollider = GetComponent<Collider>();
		//droneCollider.enabled = false
	}
	
	// Update is called once per frame
	void Update () {

		findBot("gBot0");
		if(target){
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position -transform.position), 
				speed * Time.deltaTime);
			transform.position += transform.forward * speed * Time.deltaTime;
		}
	}

	void findBot(string b){
		target = GameObject.Find(b).transform;
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
