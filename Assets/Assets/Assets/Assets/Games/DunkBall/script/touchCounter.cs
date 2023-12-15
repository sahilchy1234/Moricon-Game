using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cashbaazi.Game.Common;

namespace Cashbaazi.Game.DunkBall
{
	public class touchCounter : MonoBehaviour
	{


		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("ball"))
			{

				ringManager.tch = 1;
				//print ("bounce");
				gameManager.combo = 1;
				StartCoroutine(dunk());
			}
		}
		IEnumerator dunk()
		{

			yield return new WaitForSeconds(1);
			ringManager.tch = 0;

		}
	}
}