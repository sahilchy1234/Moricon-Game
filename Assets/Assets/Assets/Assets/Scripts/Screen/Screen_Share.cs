using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cashbaazi.App.Common;
using Cashbaazi.App.Manager;
using UnityEngine.UI;
using System;
using Cashbaazi.App.Helper;
using TMPro;

namespace Cashbaazi.App.Screen
{
    public class Screen_Share : ISCREEN
    {
        [Space(20)]
        [SerializeField] Button Btn_ShareVia_Message;
        [SerializeField] Button Btn_ShareVia_Instagram;
       // [SerializeField] Button Btn_ShareVia_Mail;
       /// [SerializeField] Button Btn_ShareVia_Other;
        [SerializeField] Button Btn_CopyRefral;
        [SerializeField] TextMeshProUGUI Txt_Refreal;

        [Header("Refrence")]
        [SerializeField] Screen_Common commonScreen;

        private void Start()
        {
            Btn_ShareVia_Message.onClick.AddListener(OnClick_ShareVia_Message);
            Btn_ShareVia_Instagram.onClick.AddListener(OnClick_ShareVia_Instagram);
           // Btn_ShareVia_Mail.onClick.AddListener(OnClick_ShareVia_Mail);
          //  Btn_ShareVia_Other.onClick.AddListener(OnClick_ShareVia_Other);
            Btn_CopyRefral.onClick.AddListener(OnClick_CopyRefral);
        }



        public override void Show()
        {
            Txt_Refreal.text = ApiManager.instance.responce_userdata.referel_code;

            base.Show();
            commonScreen.currentScreen = this;

            ScreenManager.instance.AddScreenToStack(this);
        }


        private void OnClick_ShareVia_Message()
        {
            string title = "Share Morcoin and Earn";
            string message = string.Format("Click on the link below to download the app MORCOIN\n{0}\n\nUse my referral code to get instant Rs.10\n{1}", "https://morcoin.in/site/index", ApiManager.instance.responce_userdata.referel_code);

            new NativeShare().SetTitle(title).SetText(message).Share();
        }

        private void OnClick_ShareVia_Instagram()
        {
            string title = "Share Morcoin and Earn";
            string message = string.Format("Click on the link below to download the app MORCOIN\n{0}\n\nUse my referral code to get instant Rs.10\n{1}", "https://morcoin.in/site/index", ApiManager.instance.responce_userdata.referel_code);

            new NativeShare().SetTitle(title).SetText(message).Share();
        }

      /*  private void OnClick_ShareVia_Mail()
        {
            Application.OpenURL("mailto:contact@test.com");
        }*/

        private void OnClick_ShareVia_Other()
        {
            string title = "Share Morcoin and Earn";
            string message = string.Format("Click on the link below to download the app MORCOIN\n{0}\n\nUse my referral code to get instant Rs.10\n{1}", "https://morcoin.in/site/index", ApiManager.instance.responce_userdata.referel_code);

            new NativeShare().SetTitle(title).SetText(message).Share();
        }
        private void OnClick_CopyRefral()
        {
            GUIUtility.systemCopyBuffer = ApiManager.instance.responce_userdata.referel_code;
            Toast.ShowToast(String.Format("{0} copied to clipboard", ApiManager.instance.responce_userdata.referel_code));
        }
    }
}