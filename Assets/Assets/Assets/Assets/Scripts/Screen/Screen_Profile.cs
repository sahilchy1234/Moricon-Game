using Cashbaazi.App.Common;
using Cashbaazi.App.Helper;
using Cashbaazi.App.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cashbaazi.App.Screen
{
    public class Screen_Profile : ISCREEN
    {
        [Space(20)]
        [SerializeField] TextMeshProUGUI Inpt_Playername;
        [SerializeField] TextMeshProUGUI Inpt_Playermobile;
        [SerializeField] TextMeshProUGUI Inpt_Playeremail;
        [SerializeField] TMP_InputField Inpt_RefralCode;

        [Space]
        [SerializeField] Image Img_Avatar;

        [Space]
        [SerializeField] Button Btn_Back;
        [SerializeField] Button Btn_Change;
        [SerializeField] Button Btn_ContactUs;
        [SerializeField] Button Btn_AboutUs;
        [SerializeField] Button Btn_ReportBug;
        [SerializeField] Button Btn_VerifyReferral;

        [Space]
        [SerializeField] Button Btn_Follow_Fb;
        [SerializeField] Button Btn_Follow_Insta;
        [SerializeField] Button Btn_Follow_Telegram;

        [Header("Refrence")]
        [SerializeField] Screen_Common commonScreen;


        private void Start()
        {
            Btn_Back.onClick.AddListener(OnClick_Back);
            Btn_Change.onClick.AddListener(OnClick_Change);
            Btn_ContactUs.onClick.AddListener(OnClick_ContactUs);
            Btn_AboutUs.onClick.AddListener(OnClick_AboutUs);
            Btn_ReportBug.onClick.AddListener(OnClick_ReportBug);
            Btn_VerifyReferral.onClick.AddListener(OnClick_VerifyRefral);

            //Btn_Follow_Fb.onClick.AddListener(OnClick_FB);
            //Btn_Follow_Insta.onClick.AddListener(OnClick_INSTA);
            //Btn_Follow_Telegram.onClick.AddListener(OnClick_TELEGRAM);
        }


        public override void Show()
        {
            Inpt_Playername.text = AppManager.instance.Get_PlayerData().name;
            Inpt_Playermobile.text = AppManager.instance.Get_PlayerData().mobile;
            Inpt_Playeremail.text = AppManager.instance.Get_PlayerData().email;

            Img_Avatar.sprite = AvatarManager.instance.Get_AvatarSprite(AppManager.instance.Get_PlayerAvatarIndex());

            base.Show();

            ScreenManager.instance.AddScreenToStack(this);

            AdsManager.instance.Show_Banner();
        }
        public override void Hide()
        {
            AdsManager.instance.Hide_Banner();
            base.Hide();
        }

        private void OnClick_Change()
        {
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.USER_PROFILE_UPDATE, this.screenType);
        }

        private void OnClick_Back()
        {
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.COMMON, this.screenType);
        }

        private void OnClick_ContactUs()
        {
            Application.OpenURL("https://morcoin.in/site/ContactUs");
        }

        private void OnClick_AboutUs()
        {
            Application.OpenURL("https://morcoin.in/site/AboutUs");
        }

        private void OnClick_ReportBug()
        {
            Application.OpenURL("https://morcoin.in/site/ContactUs");
        }
        private void OnClick_VerifyRefral()
        {
            if (string.IsNullOrEmpty(Inpt_RefralCode.text))
            {
                Toast.ShowToast("Invalid refral code");
                return;
            }

            ApiManager.instance.API_ApplyRefralCode(Inpt_RefralCode.text, () =>
            {
                ApiManager.instance.API_GetUserDetails(() =>
                {
                    commonScreen.WalletUpdate();
                  //  ApiManager.instance.API_AddWallet("Reffer Amount Added", ApiManager.instance.Responce_Userdata.MainWallet);
                });
            });
        }


        private void OnClick_FB()
        {
            Application.OpenURL("https://www.google.com/");
        }

        private void OnClick_INSTA()
        {
            Application.OpenURL("https://www.google.com/");
        }

        private void OnClick_TELEGRAM()
        {
            Application.OpenURL("https://www.google.com/");
        }
    }
}