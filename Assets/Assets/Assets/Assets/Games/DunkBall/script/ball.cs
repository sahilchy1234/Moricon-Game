using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour {

	public int price;
	public int number;
	public bool owned;
	

	
	private int init;public AudioSource sound;


	// Use this for initialization

	void Start(){

		sound = GameObject.Find ("soundManager2").GetComponent<AudioSource> ();
		//print ("nope");
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		//Debug.Log("OnCollisionEnter2D");
		sound.Play ();
	}
}
