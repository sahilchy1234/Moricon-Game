using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Cashbaazi.App.Common;
using Cashbaazi.App.Manager;
using Cashbaazi.App.Helper;
using UnityEngine.SceneManagement;

namespace Cashbaazi.App.Screen
{

    public class Screen_Exit : ISCREEN
    {
        [Space(20)]
        
        [SerializeField] GameObject exitPanel;
        [SerializeField] Button Btn_yesBtn;
        [SerializeField] Button Btn_noBtn;
        [SerializeField] Scene sceneName;
        void Start()
        {      
           Btn_yesBtn.onClick.AddListener(OnClick_YesBtn);
           Btn_noBtn.onClick.AddListener(OnClick_NoBtn);  
        }

        
            public void OnClick_YesBtn()
        {
            Application.Quit();
        }

        public void OnClick_NoBtn()
        {
            SceneManager.LoadScene("MENU");
        }
    }
}
