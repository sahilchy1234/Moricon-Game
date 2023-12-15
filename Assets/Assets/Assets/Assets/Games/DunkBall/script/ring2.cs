using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ring2 : MonoBehaviour {

	public Collider2D col;
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//print ("ring 3 befor: "+ringManager.count);
		if (other.gameObject.CompareTag ("ball")) {

			/*
			if (ringManager.count == 2 ){
				ringManager.count = 3;
			}
			if (ringManager.count == 1) {
				ringManager.count = 5;
			}*/

			col.enabled = false;

		}
		//print ("ring 3 after: "+ringManager.count);


	}



}
