using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cashbaazi.Game.Common;

namespace Cashbaazi.Game.GunsBottleGame
{
    public class Bullet : MonoBehaviour
    {

        public float speed = 20f;
        //	public int damage = 40;
        public Rigidbody2D rb;
        private Boss boss;

        //public GameObject impactEffect;

        // Use this for initialization
        void Start()
        {
            //rb=rb.GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * speed * Time.deltaTime;
            boss = FindObjectOfType<Boss>();
        }
        private void Update()
        {
            Destroy(gameObject, 3f);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "bottle")
            {
                LevelManager.instance.continuBottleBreak++;
                AudioManager.instance.Play("break");
                if (LevelManager.instance.continuBottleBreak == 1)
                {
                    PhotonGameController.instance.UpdateScore(+1);
                }
                else if (LevelManager.instance.continuBottleBreak == 2)
                {
                    PhotonGameController.instance.UpdateScore(+2);
                }
                else if (LevelManager.instance.continuBottleBreak == 3)
                {
                    PhotonGameController.instance.UpdateScore(+3);
                }
                else if (LevelManager.instance.continuBottleBreak == 4)
                {
                    PhotonGameController.instance.UpdateScore(+4);
                }
                else if (LevelManager.instance.continuBottleBreak == 5)
                {
                    PhotonGameController.instance.UpdateScore(+5);
                }
                else
                {
                    PhotonGameController.instance.UpdateScore(+6);
                }


                Destroy(gameObject);
            }
            else if (collision.tag == "redbottle")
            {

                LevelManager.instance.colObj = true;
                PhotonGameController.instance.UpdateScore(-2);
                LevelManager.instance.continuBottleBreak = 0;
                AudioManager.instance.Play("break");
                if (CameraShake.instance.isShaking == true)
                {
                    CameraShake.instance.StopCameraShaking();
                }

                Destroy(gameObject);
            }
            else if (collision.tag == "pinkbottle")
            {


                PhotonGameController.instance.UpdateScore(2);
                AudioManager.instance.Play("break");


                Destroy(gameObject);
            }

            else if (collision.tag == "outside")
            {
                LevelManager.instance.colObj = true;

                //if (boss == null)
                //{
                //    return;
                //}
                //else
                //{
                //    boss.noOfHits = 0;
                //}

                if (CameraShake.instance.isShaking == true)
                {
                    CameraShake.instance.StopCameraShaking();
                }
                LevelManager.instance.continuBottleBreak = 0;

                Destroy(gameObject);
            }
            else if (collision.tag == "boss")
            {
                Destroy(gameObject);
            }

        }

    }
}