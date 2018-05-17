using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
public class Drone_naive : MonoBehaviour {
	//string mode = "naive";
	public float speed;
	private GameObject target;

	//private Collider droneCollider;
	// Use this for initialization
	void Start () {
		StartCoroutine(directBot());
		//droneCollider = GetComponent<Collider>();
		//droneCollider.enabled = false
	}

	// Update is called once per frame
	void Update () {

		findBot("gBot3");
		if(target) {
			Vector3 diff = target.transform.position - transform.position;
			if(diff.magnitude > 0.01) {
				diff.Normalize();
				float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
	      transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
			}
			transform.position += transform.up * speed * Time.deltaTime;
		}
	}

	IEnumerator directBot() {
		while(true) {
			yield return new WaitForSeconds(6);
			print("checking directions");
			float botHeading = target.transform.eulerAngles.z;
			//print(target.transform.eulerAngles.z);
			//Vector3 diff = target.transform.position - transform.position;
			if(botHeading > 180) {
				print("Tapping!");
				target.SendMessage("spinRobot", 180);
			} else if(botHeading > 90 && botHeading < 180) { // check if bot is in 2nd quadrant
				print("Light tapping");
				target.SendMessage("spinRobot", 45);
			}
		}
	}

	void findBot(string b) {
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
