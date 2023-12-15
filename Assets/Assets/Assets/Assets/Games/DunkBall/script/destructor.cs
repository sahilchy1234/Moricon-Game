using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cashbaazi.Game.Common;

namespace Cashbaazi.Game.DunkBall
{
	public class destructor : MonoBehaviour
	{
		public GameObject ball, light;
		public gameManager gm;
		public AudioSource d;

		Vector2 position;

		void Start()
		{

			d = GameObject.Find("soundManager3").GetComponent<AudioSource>();
			ball = GameObject.Find("ball");
			light = GameObject.Find("lightball");
			gm = GameObject.Find("obstacleManager").GetComponent<gameManager>();



		}
		void OnTriggerEnter2D(Collider2D other)
		{


			position = ball.transform.position;
			position.y = ball.transform.position.y + 3;
			ball.SetActive(true);
			light.SetActive(true);
			//gameManager.score = gameManager.score - 2;
			//gm.showScore(gameManager.score);
			//gm.loseMenus ();
			//d.Play ();
			//StartCoroutine (temp ());

		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			position = ball.transform.position;
			position.y = ball.transform.position.y + 3;
			ball.SetActive(true);
			light.SetActive(true);
	        PhotonGameController.instance.UpdateScore(-2);
			gm.showScore(gameManager.score);
		}
		IEnumerator temp()
		{

			yield return new WaitForSeconds(2);

			ball.transform.position = position;
			ball.SetActive(true);

		}

	}
}