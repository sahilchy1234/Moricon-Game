using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cashbaazi.Game.GunsBottleGame
{
    public class Bottles : MonoBehaviour
    {
        private Container container;
        // public ParticleSystem ps;
        public GameObject particle;

        // public GameObject brokenBottle;
        // Start is called before the first frame update
        private void Start()
        {
            container = FindObjectOfType<Container>();


        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "bullet")
            {
                container.avilableBolltes--;
                // GameObject temp=  Instantiate(brokenBottle, transform.position, Quaternion.identity) as GameObject;
                //  ps.Play(); 
                //  Destroy(temp, 0.1f);
                // Vector2 pos = new Vector2(0f, 60f);
                //if (move.currentPosition == 0)
                //{
                //    GameObject br = Instantiate(particle, transform.position, Quaternion.Euler(-90, 0, 0)) as GameObject;

                //    Destroy(br, 1f);
                //}
                //else if (move.currentPosition == 1)
                //{
                //    GameObject br = Instantiate(particle, transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;

                //    Destroy(br, 1f);
                //}
                //else if (move.currentPosition == 2)
                //{
                //    GameObject br = Instantiate(particle, transform.position, Quaternion.Euler(90, 0, 0)) as GameObject;

                //    Destroy(br, 1f);
                //}
                //else if (move.currentPosition == 3)
                //{
                //    GameObject br = Instantiate(particle, transform.position, Quaternion.Euler(0, -90, 0)) as GameObject;

                //    Destroy(br, 1f);
                //}
                GameObject br = Instantiate(particle, transform.position, Quaternion.identity) as GameObject;

                Destroy(br, 1f);


                Destroy(gameObject);
            }
        }
    }
}