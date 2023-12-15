using Cashbaazi.Game.Common;
using UnityEngine;

namespace Cashbaazi.Game.KnifeHit
{
	public class Knife : MonoBehaviour
	{


		#region Public_Variables
		public float speed = 1f;
		public bool isFire = false;
		public bool isHitted = false;
		#endregion

		public Rigidbody2D rb;
		public AudioClip knifeHitsfx, ThrowKnifeSfx;

		void Start()
		{
			rb = GetComponentInChildren<Rigidbody2D>();
			rb.isKinematic = true;
			//GetComponents<BoxCollider2D> () [0].enabled = false;
			//GetComponents<BoxCollider2D> () [1].enabled = false;
		}

		// Update is called once per frame

		void Update()
		{
			if (isFire && !isHitted)
			{
				//rb.velocity = Vector2.up * speed;
			}
		}
		public void ThrowKnife()
		{
			if (!isFire && !GameManager.isGameOver)
			{
				isFire = true;
				GetComponents<BoxCollider2D>()[0].enabled = true;
				GetComponents<BoxCollider2D>()[1].enabled = true;
				rb.isKinematic = false;
				rb.AddForce(new Vector2(0f, speed), ForceMode2D.Impulse);
				SoundManager.instance.PlaySingle(ThrowKnifeSfx);
			}
		}


		void OnCollisionEnter2D(Collision2D coll)
		{
			if (coll.gameObject.tag == "Knife" && !isHitted && coll.gameObject.GetComponent<Knife>().isFire && isFire && !GameManager.isGameOver)
			{
				isHitted = true;

				GetComponents<BoxCollider2D>()[0].enabled = false;
				GetComponents<BoxCollider2D>()[1].enabled = false;
				SoundManager.instance.PlaySingle(knifeHitsfx);
				SoundManager.instance.playVibrate();
				rb.freezeRotation = false;
				rb.velocity = Vector2.zero;
				rb.angularVelocity = Random.Range(20f, 50f) * 25f;
				rb.AddForce(new Vector2(Random.Range(-5f, 5f), -30f), ForceMode2D.Impulse);
				DestroyMe();
				//Invoke("gameOver", 0.5f);
				print("Game  Over from Knife");
				//Application.LoadLevel ("Main");
				GameManager.score -= 2;
                PhotonGameController.instance.UpdateScore(-2);
                FindObjectOfType<Circle>().OnKnifeHit(coll.gameObject.GetComponent<Knife>(), false);
			}
			else if (coll.gameObject.tag == "Wood" && !isHitted && !GameManager.isGameOver)
			{
				coll.gameObject.GetComponent<Circle>().OnKnifeHit(this, true);

			}/*
		else {
			Physics2D.IgnoreCollision (coll.collider, GetComponents<Collider2D> ()[0]);
			Physics2D.IgnoreCollision (coll.collider, GetComponents<Collider2D> ()[1]);
		}*/
		}

		void gameOver()
		{
			GamePlayManager.instance.GameOver();

		}
		public void DestroyMe()
		{
			LeanTween.alpha(gameObject, 0f, 2f).setOnComplete(() =>
			{
				Destroy(gameObject);
			});
		}

	}
}