using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cashbaazi.Game.Common;
using Cashbaazi.App.Helper;

namespace Cashbaazi.Game.GunsBottleGame
{
    public class LevelManager : MonoBehaviour
    {
       public static LevelManager instance;

        // public List<Container> container;
        public List<GameObject> container = new List<GameObject>();

        private int containerCount = 0;
        public Transform point;
        private GameObject temp;
        public float timeRemaining = 60f;
        public Text time;
        public Text scoreTxt;
        public bool isGameOver;
       
        public GameObject levelPanel;
        public int continuBottleBreak;
      public GameObject background;
        public GameObject boss;
       private int stage = 1;
        public bool isFireIns = false;
        public bool colObj = false;
  
        // Start is called before the first frame update
        public  void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        void Start()
        {
            timeRemaining = 60f;
            isGameOver = false;
            GameSetUp();
        }

        // Update is called once per frame
        void Update()
        {
            if (timeRemaining >= 0)
            {
                timeRemaining -= Time.deltaTime;
                time.text = (Mathf.RoundToInt(timeRemaining % 60)).ToString();

                PhotonGameController.instance.UpdateScore(0);
            }
            else
            {
                isGameOver = true;
                
              
            }

            if (continuBottleBreak == 3)
            {
                Debug.Log("3bottle breaks");
                CameraShake.instance.ShakeIt();

            }

        }
        private void GameSetUp()
        {
            temp = Instantiate(container[containerCount], point.position, Quaternion.identity) as GameObject;

            containerCount++;
        }
        public void NextLevel()
        {
            StartCoroutine(levPan());
        }

        IEnumerator levPan()
        {
            if (isFireIns)
            {
                isFireIns = false;
            }

            yield return new WaitForSeconds(1f);

            Destroy(temp);
            temp = Instantiate(container[containerCount], point.position, Quaternion.identity);
            containerCount++;
        }
    }
}
