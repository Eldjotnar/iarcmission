using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject gRobot;
    public int numElements;
    public float radius;

	// Use this for initialization
	void Start () {
        float step = (2 * Mathf.PI) / numElements;
        float angle = 0;

        for(int i =0; i < numElements; i++)
        {
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);
            //print(angle);

            GameObject newObject = (GameObject)Instantiate(gRobot, new Vector3(x, y, 0), Quaternion.Euler(0,0,angle*180/Mathf.PI));

            angle += step;
        }

    }

	// Update is called once per frame
	void Update () {

	}
}
