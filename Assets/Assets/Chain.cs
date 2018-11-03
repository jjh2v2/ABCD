using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour {

    public Vector3 sss;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Transform rt = GetComponent<Transform>();

        rt.localPosition += sss * Time.deltaTime * 1;
	}
}
