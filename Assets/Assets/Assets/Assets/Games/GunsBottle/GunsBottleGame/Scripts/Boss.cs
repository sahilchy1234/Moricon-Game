using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cashbaazi.Game.Common;

namespace Cashbaazi.Game.GunsBottleGame
{
    public class Boss : MonoBehaviour
    {

        public int noOfHits = 0;
        private Container container;
        public GameObject like;
        public GameObject missed;
        public GameObject angry;
        public GameObject particle;
        public void Start()
        {
            container = FindObjectOfType<Container>();
        }



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "bullet")
            {
                AudioManager.instance.Play("BossBlast");
                noOfHits++;
                // StartCoroutine(Icon());
                if (noOfHits == 1)
                {
                    PhotonGameController.instance.UpdateScore(+3);
                    like.SetActive(true);
                    AudioManager.instance.Play("metal");

                }
                else if (noOfHits == 2)
                {
                    PhotonGameController.instance.UpdateScore(+3);
                    like.SetActive(false);
                    missed.SetActive(true);
                    AudioManager.instance.Play("metal");
                }
                else if (noOfHits == 3)
                {
                    PhotonGameController.instance.UpdateScore(+5);
                    angry.SetActive(true);
                    missed.SetActive(false);
                    noOfHits = 0;
                    AudioManager.instance.Play("BossBlast");


                    GameObject br = Instantiate(particle, transform.position, Quaternion.identity) as GameObject;
                    Destroy(br, 1f);
                    Destroy(gameObject);
                }


                container.avilableBolltes--;

            }
        }
        IEnumerator Icon()
        {
            like.SetActive(true);
            yield return new WaitForSeconds(3f);
            like.SetActive(false);
        }
    }
}
