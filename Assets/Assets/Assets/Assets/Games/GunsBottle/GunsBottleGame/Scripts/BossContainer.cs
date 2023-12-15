using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cashbaazi.Game.GunsBottleGame
{
    public class BossContainer : MonoBehaviour
    {



        public GameObject displayImage;

        public GameObject bossManager;
        private void Awake()
        {
            StartCoroutine(DisplayImage());



        }

        private void Start()
        {
            StopCoroutine(DisplayImage());

        }
        IEnumerator DisplayImage()
        {
            displayImage.SetActive(true);
            bossManager.SetActive(false);
            this.GetComponent<Container>().enabled = false;
            yield return new WaitForSeconds(3f);
            displayImage.SetActive(false);
            bossManager.SetActive(true);
            this.GetComponent<Container>().enabled = true;

        }

    }
}