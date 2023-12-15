using Cashbaazi.Game.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cashbaazi.Game.KnifeHit
{
	public class KnifeCounter : MonoBehaviour
	{
		public GameObject knifeIcon;
		public Color activeColor;
		public Color deactiveColor;
		public static KnifeCounter intance;
		//public GameObject knifeCounter;
		List<GameObject> iconList;

		void Awake()
		{
			if (intance == null)
			{
				intance = this;
				iconList = new List<GameObject>();
				//LeanTween.moveLocal(knifeCounter, new Vector3(-255f, -400f, 0f), 14f).setDelay(.2f);
			}
			else
				Destroy(gameObject);
		}
		public void setUpCounter(int totalKnife)
		{
			foreach (var item in iconList)
			{
				Destroy(item);
			}
			iconList.Clear();

			for (int i = 0; i < totalKnife; i++)
			{
				GameObject temp = Instantiate<GameObject>(knifeIcon, transform);
				temp.GetComponent<Image>().color = activeColor;
				iconList.Add(temp);
			}
		}
		public void setHitedKnife(int val)
		{
			for (int i = 0; i < iconList.Count; i++)
			{
				iconList[i].GetComponent<Image>().color = i < val ? deactiveColor : activeColor;
			}
		}
	}

	}