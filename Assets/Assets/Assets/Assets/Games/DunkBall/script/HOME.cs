using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HOME : MonoBehaviour {

	public GameObject info;

	public void play(){

	
		SceneManager.LoadScene("main");
	}
	public void shop(){


		SceneManager.LoadScene("shop");
	}

	public void infos(){
		info.SetActive (true);

	}
	public void home(){
		info.SetActive (false);
	}
}
