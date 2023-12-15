using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BallManager : MonoBehaviour {
	public GameObject[] balls;
	private GameObject ball;
	public Rigidbody2D r;
	private Vector2 vpower;
	public static int played;
	// Use this for initialization
	void Awake () {

		balls=Resources.LoadAll("balls", typeof(GameObject)).Cast<GameObject>().ToArray();
		foreach (var t in balls)
			Debug.Log(t.name);
		
		int ballIndex = PlayerPrefs.GetInt ("ball");
		//print ("ball index : "+ballIndex);
		ball = Instantiate (balls [ballIndex], new Vector2 (-1, 3.5f), Quaternion.identity);
		ball.name="ball";
		vpower = new Vector2 (3.5f, 6f);
		r = ball.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("space")) {

			//print ("speed :" + r.velocity.magnitude);
			r.isKinematic=false;

			r.velocity = Vector2.zero;

			r.AddForce (vpower, ForceMode2D.Impulse);

			if(vpower.x>0){
				vpower.x=-2;
			}
			else{
				vpower.x=2;
			}
		}

		if (Input.GetMouseButtonDown(0))
			{
			//r.isKinematic = false;

			r.bodyType=RigidbodyType2D.Dynamic;
			r.velocity = Vector2.zero;
			r.AddForce (vpower, ForceMode2D.Impulse);
			if(vpower.x>0){
				vpower.x=-2;
			}
			else{
				vpower.x=2;
			}
			}

		
	}
	void FixedUpdate()
	{
		if(r.velocity.magnitude > 8)
		{
			r.velocity = r.velocity.normalized * 8;
		}
	}

}
