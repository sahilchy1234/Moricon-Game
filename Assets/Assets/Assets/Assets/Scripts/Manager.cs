using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cashbaazi.Game.KnifeHit
{
   public class Manager : MonoBehaviour
    {
        public GameObject gameManager;
        public GameObject gameWaiting;
        public GameObject gameScreen;

        void Start()
        {
            if(gameWaiting.activeSelf)
            {
                gameManager.SetActive(false);
                gameScreen.SetActive(false);
                Debug.Log("Working");
            }
            if(!gameManager.activeSelf)
            {
                gameManager.SetActive(true);
                gameScreen.SetActive(true);
            }
        }

    }
}