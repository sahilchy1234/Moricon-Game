using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cashbaazi.Game.GunsBottleGame
{
	public class GunRotation : MonoBehaviour
	{
		public Transform firePoint;
		public GameObject bulletPrefab;
		private bool canShoot = true;
		public int bulletNo;
		private Container container;
		private Bottles bottle;
		public GameObject fire;

		private GameObject tempFire;

		public float rotationspeed = 1f;
		//private int avalibleBottles;

		private void Start()
		{
			container = FindObjectOfType<Container>();
			bottle = FindObjectOfType<Bottles>();
			bulletNo = container.availableBullets;

		}
		void Update()
		{
			Flame();
			transform.Rotate(0, 0, -rotationspeed * Time.deltaTime);


			if (Input.GetMouseButtonDown(0) && canShoot && !LevelManager.instance.isGameOver)
			{
				Shoot();

			}
			if (container.avilableBolltes == 0 || container.availableBullets == 0)
			{
				canShoot = false;
				LevelManager.instance.NextLevel();
				Destroy(gameObject);



			}



		}


		void Shoot()
		{

			Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
			StartCoroutine(RotationVariation());

			AudioManager.instance.Play("fire");
			container.availableBullets--;
			Bulletcounter.Instance.BulletHit(container.availableBullets);


		}
		IEnumerator RotationVariation()
		{
			rotationspeed = 1000f;
			yield return new WaitForSeconds(0.5f);
			rotationspeed = 180f;
		}
		void Flame()
		{
			if (LevelManager.instance.continuBottleBreak >= 3 && LevelManager.instance.isFireIns == false)
			{
				LevelManager.instance.isFireIns = true;

				tempFire = Instantiate(fire, firePoint.position, firePoint.rotation) as GameObject;
				tempFire.transform.SetParent(this.firePoint);
				//tempFire.SetActive(true);

			}
			if (LevelManager.instance.isFireIns == true && LevelManager.instance.colObj == true)
			{
				LevelManager.instance.isFireIns = false;
				LevelManager.instance.colObj = false;

				Destroy(tempFire);



			}
		}
	}
}