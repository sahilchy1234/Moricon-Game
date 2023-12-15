using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour {

	public GameObject ball;
	public float x, y;
	// Use this for initialization
	void Start () {

		ball=GameObject.Find("ball");
		
	}
	
	// Update is called once per frame
	void Update () {

		float positionX = ball.transform.position.x + x;
		float positionY = ball.transform.position.y + y;

		gameObject.transform.position = new Vector2(positionX,positionY);
		
	}
}
