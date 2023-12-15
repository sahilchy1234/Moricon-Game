using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Cashbaazi.Game.GunsBottleGame
{
    public class Bulletcounter : MonoBehaviour
    {


        public static Bulletcounter Instance;
        [SerializeField] private GameObject bulletSprite;
        [SerializeField]
        private Color KnifeReadyColor;
        [SerializeField] private Color KnifeWastedColor;

        private List<GameObject> icons = new List<GameObject>();



        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                // DontDestroyOnLoad(gameObject);
            }
        }







        public void SetupBullet(int amount)
        {
            foreach (var icon in icons)
            {
                Destroy(icon);
            }

            icons.Clear();

            for (int i = 0; i < amount; i++)
            {
                GameObject icon = Instantiate(bulletSprite, transform);
                icon.GetComponent<Image>().color = Color.white;
                icons.Add(icon);

            }




        }




        public void BulletHit(int amount)
        {
            for (int i = 0; i < icons.Count; i++)
            {
                icons[i].GetComponent<Image>().color = i < amount ? Color.white : Color.black;
            }
        }
    }
}