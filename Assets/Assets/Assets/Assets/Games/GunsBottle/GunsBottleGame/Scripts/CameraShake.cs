using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cashbaazi.Game.GunsBottleGame
{
	public class CameraShake : MonoBehaviour
	{
		public static CameraShake instance;
		Vector3 cameraInitialPosition;
		public float shakeMagnetude = 0.05f, shakeTime = 0.5f;
		//public Camera mainCamera;
		public Camera line;
		public bool isShaking = false;

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}
		private void Start()
		{
			cameraInitialPosition = line.transform.position;
		}
		public void ShakeIt()
		{

			InvokeRepeating("StartCameraShaking", 0f, 5f);
			//Invoke ("StopCameraShaking", shakeTime);
			//StartCameraShaking();
		}

		void StartCameraShaking()
		{
			isShaking = true;
			float cameraShakingOffsetX = Random.value * shakeMagnetude * 2 - shakeMagnetude;
			float cameraShakingOffsetY = Random.value * shakeMagnetude * 2 - shakeMagnetude;
			Vector3 cameraIntermadiatePosition = line.transform.position;
			cameraIntermadiatePosition.x += cameraShakingOffsetX;
			cameraIntermadiatePosition.y += cameraShakingOffsetY;
			line.transform.position = cameraIntermadiatePosition;
		}

		public void StopCameraShaking()
		{
			line.transform.position = cameraInitialPosition;
			CancelInvoke("StartCameraShaking");

			isShaking = false;
		}

	}
}
