using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour {


	public GameObject ball;
	public Camera came;
	private float yPosiyion;
	public float direction;
	Vector3 pos;
	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;
	// Use this for initialization
	void Start () {
		ball=GameObject.Find("ball");
		yPosiyion = (ball.transform.position.y);
		pos.y = yPosiyion;
		pos.x = 0;
		pos.z = -10;

		print ("width : "+(Screen.width)*0.75+" height :"+Screen.height*0.75);

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		yPosiyion = ball.transform.position.y;

		//print ((came.WorldToScreenPoint (ball.transform.position).y));
		if ((came.WorldToScreenPoint (ball.transform.position).y) > (Screen.height*0.88)) {
			pos.y = yPosiyion+direction;
			came.transform.position = Vector3.Lerp(came.transform.position, pos, 0.05f);

			//print (Time.deltaTime);
			//came.transform.Translate(Vector3.up* Time.deltaTime);
		} else if (came.WorldToScreenPoint (ball.transform.position).y < (Screen.height*0.5)) {
			pos.y = yPosiyion-2;
			//came.transform.Translate(Vector3.down* 0.2f);
			//came.transform.position = Vector3.Lerp(came.transform.position, new Vector3(0,ball.transform.position.y,-10), smoothTime);
			came.transform.position = Vector3.Lerp(came.transform.position, pos, smoothTime);

		}
		
	}
	/*void OnTriggerEnter2D()
	{
		if (ball.tag == "ball")
		{
			//came.transform.position= Vector3.SmoothDamp(came.transform.position, pos, ref velocity, smoothTime);
			//Debug.Log("OnTriggerEnter2D");
			Vector3 screenPos = came.WorldToScreenPoint(ball.transform.position);
			Debug.Log("target is " + screenPos.y + " pixels from down");
		}
	}*/
}
