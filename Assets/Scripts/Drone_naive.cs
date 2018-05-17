using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
public class Drone_naive : MonoBehaviour {
	//string mode = "naive";
	public float speed;
	private Transform target;
	private LineRenderer line;
	//private Collider droneCollider;
	// Use this for initialization
	void Start () {
		
		line = gameObject.AddComponent<LineRenderer>();
		line.SetWidth(0.05F, 0.05F);
		line.SetVertexCount(2);
		//droneCollider = GetComponent<Collider>();
		//droneCollider.enabled = false
	}
	
	// Update is called once per frame
	void Update () {

		findAll();
		findBot("gBot0");
		if(target){
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position -transform.position), 
				speed * Time.deltaTime);
			transform.position += transform.forward * speed * Time.deltaTime;
		}
        tapBot();
	}

	void findBot(string b){
		target = GameObject.Find(b).transform;
	}

    void tapBot() {
        findBot("gBot0");
        var at = target.GetComponent<botAttributes>();
        at.tapMe();
    }

//Not used right now...
	void findAll(){
		var allRoombas = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name.Contains("gBot"));
		if(allRoombas.Count() > 0){
			foreach(GameObject b in allRoombas){
				line.SetPosition(0, this.transform.position);
				line.SetPosition(1, b.transform.position);
				//Debug.Log(b + " : " + b.transform.position + "--> " + b.name);
			}
		}
	}
}
