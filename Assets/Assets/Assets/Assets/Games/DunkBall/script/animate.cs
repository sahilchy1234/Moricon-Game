using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animate : MonoBehaviour {

	public Vector2 position;
	private Vector2 p;
	// Use this for initialization
	void Start () {

		p = gameObject.transform.position;
		print ("POSITION : "+p);
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector3.Lerp (p, position, Time.time*1.2f);


		
	}
}
