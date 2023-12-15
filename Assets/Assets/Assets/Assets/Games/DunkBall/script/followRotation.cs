using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followRotation : MonoBehaviour {
	public GameObject ball;
	public float x, y;
	// Use this for initialization
	void Start () {

		ball=GameObject.Find("ball");

	}

	// Update is called once per frame
	void Update () {

		Quaternion rotationZ = ball.transform.rotation;

		gameObject.transform.rotation=rotationZ;

	}
}
