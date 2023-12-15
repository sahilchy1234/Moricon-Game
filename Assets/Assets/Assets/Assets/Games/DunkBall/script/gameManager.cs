using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using Cashbaazi.Game.Common;

namespace Cashbaazi.Game.DunkBall
{

	public class gameManager : MonoBehaviour
	{

		public GameObject[] obstacles;
		private GameObject first, second, third, last;
		public GameObject ball, flame1, flame2, loseMenu, explosionBall, revivedb, light;
		private float currentPosition = -15;
		public static int score = 0, combo = 1, credit;
		private GameObject tmp;
		public Text scr, cmb, scrm, coin, bscr;
		List<GameObject> level1, level2, level3, level4;
		private bool revived = false;
		public AudioSource sound;
		//public ads ad;

		private int iteration, adsNumber = 0;


		public ParticleSystem exp;
		// Use this for initialization
		void Start()
		{



			//adsManager.instance.ShowBanner ();
			level1 = new List<GameObject>();
			level2 = new List<GameObject>();
			level3 = new List<GameObject>();
			level4 = new List<GameObject>();
			List<GameObject> tmpLevel = new List<GameObject>();

			for (int i = 0; i < obstacles.Length; i++)
			{
				if (obstacles[i].GetComponent<obstacle>().difficulty == 0)
				{
					level1.Add(obstacles[i]);
					tmpLevel.Add(obstacles[i]);

				}
				else if (obstacles[i].GetComponent<obstacle>().difficulty == 1)
				{
					level2.Add(obstacles[i]);
				}
				else if (obstacles[i].GetComponent<obstacle>().difficulty == 2)
				{
					level3.Add(obstacles[i]);
				}
				else if (obstacles[i].GetComponent<obstacle>().difficulty == 3)
				{
					level4.Add(obstacles[i]);
				}
			}

			print(level1.Count + " | " + level2.Count + " | " + level3.Count + " | " + level4.Count + " tmp : " + tmpLevel.Count);



			exp = explosionBall.GetComponent<ParticleSystem>();
			ball = GameObject.Find("ball");


			flame1.SetActive(false);
			flame2.SetActive(false);
			//print (" tmplevel content " + tmpLevel [0] + " | " + tmpLevel [1] + " | " + tmpLevel [2] + " | " + tmpLevel [3] + " | " + tmpLevel [4] + " | " + tmpLevel [5] + " | ");
			first = Instantiate(tmpLevel[0], new Vector2(0, -5), Quaternion.identity);
			tmpLevel[0].GetComponent<obstacle>().appearance++;
			//print ("removed : " + tmpLevel [0]);
			tmpLevel.Remove(tmpLevel[0]);

			int r = (int)Random.Range(0f, tmpLevel.Count);
			second = Instantiate(tmpLevel[r], new Vector2(0, first.transform.position.y - 10), Quaternion.identity);

			tmpLevel[r].GetComponent<obstacle>().appearance++;
			tmpLevel.Remove(tmpLevel[r]);

			r = (int)Random.Range(0f, tmpLevel.Count);
			third = Instantiate(tmpLevel[r], new Vector2(0, second.transform.position.y - 10), Quaternion.identity);

			tmpLevel[r].GetComponent<obstacle>().appearance++;
			tmpLevel.Remove(tmpLevel[r]);

			r = (int)Random.Range(0f, tmpLevel.Count);
			last = Instantiate(tmpLevel[r], new Vector2(0, third.transform.position.y - 10), Quaternion.identity);

			tmpLevel[r].GetComponent<obstacle>().appearance++;
			tmpLevel.Remove(tmpLevel[r]);


			iteration = 4;

			//print ("tmp : " + tmpLevel.Count + " | " + level1.Count); 



		}

		// Update is called once per frame

		void Update()
		{

			if (ball.transform.position.y < currentPosition)
			{
				List<GameObject> tmp1 = new List<GameObject>();

				if (iteration > 0 && iteration < 4)
				{
					tmp1 = level1;

				}
				else if (iteration >= 4 && iteration < 9)
				{
					tmp1 = level1.Union(level2).ToList();

				}
				else if (iteration >= 9 && iteration < 13)
				{
					tmp1 = (level1.Union(level2)).Union(level3).ToList();


				}
				else if (iteration >= 13)
				{
					tmp1 = (((level1.Union(level2)).Union(level3)).Union(level4)).ToList();
				}

				generate(tmp1);
				print("count : " + tmp1.Count + " iteration : " + iteration);
				iteration++;
				for (int i = 0; i < obstacles.Length; i++)
				{
					obstacles[i].GetComponent<obstacle>().iterate();
				}
				currentPosition = currentPosition - 10;

			}




		}

		public void generate(List<GameObject> obs)
		{

			tmp = first;
			first = second;
			second = third;
			third = last;

			int r = (int)Random.Range(0f, obs.Count);
			int index = r;

			if (obs[r].GetComponent<obstacle>().appearance > 0)
			{
				while (r == index)
				{
					print("how many times ?");
					r = (int)Random.Range(0f, obs.Count);
				}
			}
			print("random : " + r + " | " + obs.Count);
			last = Instantiate(obs[r], new Vector2(0, third.transform.position.y - 10), Quaternion.identity);
			obs[r].GetComponent<obstacle>().appearance++;
			print("this object appeared : " + obs[r].GetComponent<obstacle>().appearance + " time with index : " + obs[r]);
			Destroy(tmp);


		}


		public void showScore(int s)
		{

			scr.text = s.ToString();


		}
		public void showCombo(int s)
		{
			if (s > 1 && s < 3)
			{
				cmb.text = "+ " + s.ToString();
				flame1.SetActive(true);
				flame2.SetActive(false);
				sound.Play();
				//flame1.SetActive (true);
			}
			else if (s >= 3)
			{
				cmb.text = "+ " + s.ToString();
				flame1.SetActive(false);
				flame2.SetActive(true);
				sound.Play();
			}
			else if (s <= 1)
			{
				cmb.text = "";
				flame2.SetActive(false);
				flame1.SetActive(false);

			}
		}

		public void loseMenus()
		{

			Instantiate(explosionBall, ball.transform.position, Quaternion.identity);


			StartCoroutine(loseTime());
		}
		public void relpay()
		{

			if (adsNumber < 2)
			{

				adsNumber++;

			}
			else if (adsNumber >= 2)
			{
				//	ad.showAdsSkip ();
				adsNumber = 0;
				PlayerPrefs.SetInt("nbads", adsNumber);
			}

			score = 0;
			SceneManager.LoadScene("main");

		}

		public void home()
		{

			//ad.showAdsSkip ();
			score = 0;
			SceneManager.LoadScene("home");

		}
		IEnumerator loseTime()
		{

			yield return new WaitForSeconds(2);
			loseMenu.SetActive(true);
			scrm.text = score.ToString();
			coin.text = ((int)(score * 0.4)).ToString();
			credit = credit + ((int)(score * 0.4));
			PlayerPrefs.SetInt("credit", credit);

			if (PlayerPrefs.GetInt("bscore") < score)
			{
				PlayerPrefs.SetInt("bscore", score);
			}
			bscr.text = "best : " + PlayerPrefs.GetInt("bscore").ToString();




			cmb.text = "";
			scr.text = "";
			if (revived)
			{
				revivedb.SetActive(false);
			}
			else
			{
				revivedb.SetActive(true);
			}

		}
		public void revive()
		{


			loseMenu.SetActive(false);
			ball.SetActive(true);
			light.SetActive(true);
			Vector2 position = ball.transform.position;
			position.y = ball.transform.position.y + 5;
			position.x = 0;
			ball.transform.position = position;
			ball.GetComponent<Rigidbody2D>().isKinematic = true;
			revived = true;

		}
	}
}
