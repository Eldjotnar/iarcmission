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

	public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 10;

	List<GameObject> roombas;
	// Use this for initialization
	void Start () {
		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.widthMultiplier = 0.02f;
        lineRenderer.positionCount = lengthOfLineRenderer;
		
        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
        lineRenderer.colorGradient = gradient;
	}
	
	// Update is called once per frame
	void Update () {
		findAll();

	}

	void tapFirst(string b){
		target = GameObject.Find(b).transform;
		if(target){
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position -transform.position), 
					speed * Time.deltaTime);
				transform.position += transform.forward * speed * Time.deltaTime;
			tapBot();
		}
	}

    void tapBot() {
        var at = target.GetComponent<botAttributes>();
        at.tapMe();
    }

//Not used right now...
	void findAll(){
		var all = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name.Contains("gBot")).ToList();
		var sorted = prioritize(all);
		//var sorted = all.OrderBy(go=>go.transform.position.y).ToList();
		List<GameObject> temp = sorted;
		temp.Insert(0, this.gameObject);
		LineRenderer lineRenderer = GetComponent<LineRenderer>();
		//lineRenderer.SetPosition(0, transform.position);
        for (int i = 0, i_end=temp.Count(); i < i_end; i++) {
            lineRenderer.SetPosition(i, temp.ElementAt(i).transform.position);
        }
		tapFirst(temp.ElementAt(0).name);
	}

	List<GameObject> prioritize(List<GameObject> temp){
		//prioritize according to bot closest to green line
		List<GameObject> sorted= null;
		sorted.Insert(0, temp[0]);
		for(int i=1, i_end=temp.Count(); i<i_end; ++i){
			float from = temp[i].transform.position.y;
			float to = 4.7f;
			float diff1 = to - from;

			for(int j=0; j<i; ++j){
				from = sorted[j].transform.position.y;
				to = 4.7f;
				float diff2 = to - from;
				if(diff1 < diff2){
					sorted.Insert(j, temp[i]);
					break;
				}
			}
		}
		return sorted;
		//roombas = sorted;
	}
}
