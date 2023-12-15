using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cashbaazi.Game.Common;

namespace Cashbaazi.Game.DunkBall
{
	public class ringManager : MonoBehaviour
	{

		public GameObject bound;
		public static int count = 3, tch = 0;
		public GameObject dad;
		public gameManager gm;
		public GameObject explosion;
		public AudioSource sound;


		// Use this for initialization

		void Start()
		{

			gm = GameObject.Find("obstacleManager").GetComponent<gameManager>();
			sound = GameObject.Find("soundManager").GetComponent<AudioSource>();
			//print ("nope");
		}


		void OnTriggerEnter2D(Collider2D other)
		{
			//print ("ring 2 befor: "+ringManager.count);
			if (other.gameObject.CompareTag("ball"))
			{
				//if (count == 1) {

				bound.SetActive(false);
				count = 2;

				//print (gameManager.score);
				//print ("win");

				if (tch == 0)
				{
					//print ("super dunk!");
					//gameManager.combo++;
					PhotonGameController.instance.UpdateScore(+5);
					gm.showScore(gameManager.score);
					//gm.showCombo (gameManager.combo);

				}
				else
				{
					tch = 0;
					//gameManager.score = gameManager.score + 5;
					PhotonGameController.instance.UpdateScore(+5);
					gm.showScore(gameManager.score);
					//gm.showCombo (gameManager.combo);
				}

				//gameObject.SetActive (false);
				StartCoroutine(desactivation());






			}
			//	print ("ring 2 after: "+ringManager.count);

		}

		IEnumerator desactivation()
		{
			yield return new WaitForSeconds(0.2f);
			//print ("hello");
			sound.Play();
			dad.SetActive(false);
			explosion.SetActive(true);
			//print ("hello after");
		}

	}
}
