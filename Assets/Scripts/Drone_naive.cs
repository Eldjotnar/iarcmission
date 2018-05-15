using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
public class Drone_naive : MonoBehaviour {
	//string mode = "naive";

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		var foundObjects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "gRobot(Clone)");
		if(foundObjects.Count() > 0){
			foreach(GameObject b in foundObjects){
				Debug.Log(b + " : " + b.transform.position);
			}
		}
	}
}
