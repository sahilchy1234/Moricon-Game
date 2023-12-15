using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

	//public int x1, x2;
	public int distance;
	public float speed=0f;
	private Vector3 position1,position2;
	public bool y;
	// Use this for initialization
	void Start () {
		
		position1 = gameObject.transform.position;
		position2 = position1;
		if (y) {
			position2.y = position1.y + distance;
		} else {
			position2.x = position1.x + distance;
		}

	}

	// Update is called once per frame
	void Update () {

		transform.position = Vector3.Lerp(position1, position2,  Mathf.PingPong(Time.time*0.5F, 1));
	}
}
