﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botAttributes : MonoBehaviour {
    private bool tapped;
    // Use this for initialization
    void Start () {
        tapped = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (tapped) {
            print("botatts tapped!");
        }
	}

    public void tapMe()
    {
        tapped = true;
        var b = GameObject.Find("gBot0").transform;
        var c = b.GetComponent<gBotController>();
        c.RotateBot(); //this might not yet be working...
    }
}
