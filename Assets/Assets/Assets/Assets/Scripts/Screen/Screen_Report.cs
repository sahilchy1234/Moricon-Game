using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cashbaazi.App.Common;
using UnityEngine.UI;
using Photon.Pun;

namespace Cashbaazi.App.Screen
{
    public class Screen_Report : ISCREEN
    {
        [SerializeField] Button reportBtn;
        [SerializeField] Button backBtn;

        [SerializeField] Screen_Gameover gameoverScreen;
        private void Start()
        {
            reportBtn.onClick.AddListener(OnClickReprtBtn);
            backBtn.onClick.AddListener(OnClickBackBtn);
        }

       private  void OnClickReprtBtn()
        {
            Application.OpenURL("http://wa.me/918927599830");
        }

       private void OnClickBackBtn()
        {
            PhotonNetwork.Disconnect();
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.MENU,this.screenType);
           Debug.Log("Back Btn");
        }
    }
}