using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cashbaazi.App.Common
{
    public class NoRotation : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        public GameObject FruitNinja;
        public GameObject KnifeHit;
        
        private void Update()
        {
           // if (!FruitNinja.activeSelf)
            if (SceneManager.GetActiveScene().name == "FruitNinja")           
            {
                FruitNinja.SetActive(UnityEngine.Screen.width > UnityEngine.Screen.height);             
            }           
            else                    
                panel.SetActive(UnityEngine.Screen.width > UnityEngine.Screen.height);
                       
          
            if (SceneManager.GetActiveScene().name == "KnifeHit")
            {
                if (UnityEngine.Screen.width > UnityEngine.Screen.height)
                {
                    Application.Quit();
                }
                else
                {
                    UnityEngine.Screen.orientation = ScreenOrientation.Portrait;
                   
                }
            }
            
        }
    }
}