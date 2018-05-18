using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
public class Drone_naive : MonoBehaviour {
	//string mode = "naive";
	public float speed;
	//Drone object
	private GameObject target;
	private LineRenderer line;
	//private GameObject target;
	bool tapped = false;

	//private Collider droneCollider;

	public Color c1 = Color.yellow;
	public Color c2 = Color.red;

	List<GameObject> roombas; //retired
	// Use this for initialization
	void Start () {
		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.widthMultiplier = 0.02f;

		// A simple 2 color gradient with a fixed alpha of 1.0f.
		float alpha = 1.0f;
		Gradient gradient = new Gradient();
		gradient.SetKeys(
		new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
		new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
		);
		lineRenderer.colorGradient = gradient;
		InvokeRepeating("findNext", 1.0f, 5.0f);

		StartCoroutine(directBot());
		//findBot("gBot3");
	}

	// Update is called once per frame
	void Update () {
		roombas = findBots();
		greedySort(ref roombas);
		visualizeLines(ref roombas);
		//updateAll();
		//findBot("gBot0");
		//findBot("gBot1");
		//target = roombas.ElementAt(1);
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
	void findNext(){
		target = roombas.ElementAt(1);
	}

	IEnumerator directBot() {
		float botHeading=0;
		while(true) {
			yield return new WaitForSeconds(6);
			print("checking directions");
			try{
				botHeading = target.transform.eulerAngles.z;
			}
			catch(MissingReferenceException){
				findBot("gBot2");
			}
			gBotController cs = target.GetComponent<gBotController>();
			bool spinning = cs.spinning;
			//Debug.Log(spinning);

			if(botHeading > 180 && !spinning) {
				print("Tapping!");
				target.SendMessage("spinRobotFromDrone", 180);
			} else if(botHeading > 90 && botHeading < 180) { // check if bot is in 2nd quadrant
				print("Light tapping");
				target.SendMessage("spinRobotFromDrone", 45);
			}
		}
	}

	List<GameObject> findBots(){
		var all = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name.Contains("gBot")).ToList();
		return all;
	}
	void findBot(string b) {
		target = GameObject.Find(b);
	}

	void greedySort(ref List<GameObject> bots){
		prioritize(ref bots);
		//bots = bots.OrderBy(go=>go.transform.position.y).ToList();
		//bots.Reverse();
	}

	void visualizeLines(ref List<GameObject> bots){
		List<GameObject> temp = bots;
		temp.Insert(0, this.gameObject);
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.positionCount = temp.Count();
		//lineRenderer.SetPosition(0, transform.position);
		for (int i = 0, i_end=temp.Count(); i < i_end; i++) {
			lineRenderer.SetPosition(i, temp.ElementAt(i).transform.position);
		}
	}

	void prioritize(ref List<GameObject> temp){
		//prioritize according to bot closest to green line
		List<GameObject> sorted = new List<GameObject>{temp[0]};
		//sorted.Add(temp[0]);
		bool found = false;
		for(int i=0, i_end=temp.Count(); i<i_end; ++i){
			found = false;
			float from = temp[i].transform.position.y;
			float to = 4.7f;
			float diff1 = to - from;

			for(int j=0, j_end=sorted.Count(); j<j_end; ++j){
				from = sorted[j].transform.position.y;
				//to = 4.7f;
				float diff2 = to - from;
				if(diff1 < diff2){
					sorted.Insert(j, temp[i]);
					found = true;
					break;
				}
			}
			if(found) continue;
			sorted.Add(temp[i]);
		}
		temp = sorted;
	}
}
