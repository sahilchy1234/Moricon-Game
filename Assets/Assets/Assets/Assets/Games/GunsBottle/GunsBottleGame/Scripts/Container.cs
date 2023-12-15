using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cashbaazi.Game.GunsBottleGame
{
    public class Container : MonoBehaviour
    {


        public int availableBullets;
        public int avilableBolltes;
        public GameObject gunPrefab;
        public GameObject pinkBottle;
        public GameObject ContainerImage;

        public void Start()
        {
            Vector3 newPos = new Vector3(-0.2f,0f, 0f);
            ContainerImage.transform.position = newPos;
            SpwanGun();
            Bulletcounter.Instance.SetupBullet(availableBullets);
            int pinkBottleNo = Random.Range(1, 10);
            if (pinkBottleNo > 5)
            {
                if (pinkBottle == null)
                {
                    return;
                }
                else
                {
                    pinkBottle.SetActive(true);
                    avilableBolltes++;
                }


            }
            else
            {
                if (pinkBottle == null)
                {
                    return;
                }
                else
                {
                    pinkBottle.SetActive(false);
                }

            }



        }

        public void SpwanGun()
        {
            Instantiate(gunPrefab, Vector3.zero, Quaternion.identity);
            AudioManager.instance.Play("reload");

        }


    }
}



