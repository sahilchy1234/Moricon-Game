using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Cashbaazi.App.Common;
using Cashbaazi.App.Manager;
using Cashbaazi.App.Helper;



namespace Cashbaazi.App.Screen
{
    public class Screen_Common : ISCREEN
    {
        [Space(20)]
        [SerializeField] Button Btn_Vip;
        [SerializeField] Button Btn_Offer;
        [SerializeField] Button Btn_Menu;
        [SerializeField] Button Btn_Share;
        [SerializeField] Button Btn_Wallet;
        [SerializeField] Button Btn_Profile;
        [SerializeField] Button Btn_Exit;

        [Space]
        [SerializeField] TextMeshProUGUI Txt_Playername;
        [SerializeField] TextMeshProUGUI Txt_PlayerCash;

        [Space]
        [SerializeField] Image Img_Avatar;
        [SerializeField] Color Color_buttonSelected;
        
        [Space]
        public ISCREEN currentScreen;
        bool isProcessing;

        Button currentDownButton;
       

        private void Start()
        {
            Btn_Vip.onClick.AddListener(OnClick_Vip);
            Btn_Offer.onClick.AddListener(OnClick_Offer);
            Btn_Menu.onClick.AddListener(OnClick_Menu);
            Btn_Share.onClick.AddListener(OnClick_Share);
            Btn_Wallet.onClick.AddListener(OnClick_Wallet);
            Btn_Exit.onClick.AddListener(Onclick_Exit);
            Btn_Profile.onClick.AddListener(OnClick_Profile);
        }

        public override void Show()
        {
            ApiManager.instance.API_GetUserDetails(
                () =>
                {
                    Txt_Playername.text = AppManager.instance.Get_PlayerData().name;
                    Txt_PlayerCash.text = AppManager.instance.Get_PlayerWallet().ToString();
                    Img_Avatar.sprite = AvatarManager.instance.Get_AvatarSprite(AppManager.instance.Get_PlayerData().avtar);

                    base.Show();
                    ScreenManager.instance.SwitchScreen(SCREEN_TYPE.MENU);
                    SwitchButtonDownAnimation(Btn_Menu);
                },
                () =>
                {
                    Toast.ShowToast("Something went wrong! Please re-open the app");
                });

            // AdsManager.instance.ShowBanner();
        }

        public void WalletUpdate()
        {
            Txt_PlayerCash.text = AppManager.instance.Get_PlayerWallet().ToString();
        }


        private void SwitchButtonDownAnimation(Button btnToShow)
        {
            if (currentDownButton != null)
            {
                currentDownButton.GetComponentInChildren<Animator>().SetBool("IsSelected", false);
                currentDownButton.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                currentDownButton.transform.GetChild(0).GetChild(1).GetComponent<Image>().color = Color.white;
                if (currentDownButton.transform.GetChild(0).childCount >= 3)
                    currentDownButton.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.white;
            }
            currentDownButton = btnToShow;
            currentDownButton.GetComponentInChildren<Animator>().SetBool("IsSelected", true);
            currentDownButton.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            currentDownButton.transform.GetChild(0).GetChild(1).GetComponent<Image>().color = Color_buttonSelected;
            if (currentDownButton.transform.GetChild(0).childCount >= 3)
                currentDownButton.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().color = Color_buttonSelected;
        }



        private void OnClick_Vip()
        {
            if (isProcessing)
                return;

            if (currentScreen.screenType == SCREEN_TYPE.VIP)
                return;

            isProcessing = true;
            Timer.Schedule(this, Core.Screen_FadeTime * 2f, () => isProcessing = false);

            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.VIP, currentScreen == null ? SCREEN_TYPE.none : currentScreen.screenType);
            SwitchButtonDownAnimation(Btn_Vip);
        }

        private void OnClick_Offer()
        {
            if (isProcessing)
                return;

            if (currentScreen.screenType == SCREEN_TYPE.OFFER)
                return;

            isProcessing = true;
            Timer.Schedule(this, Core.Screen_FadeTime * 2f, () => isProcessing = false);

            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.OFFER, currentScreen == null ? SCREEN_TYPE.none : currentScreen.screenType);
            SwitchButtonDownAnimation(Btn_Offer);
        }

       public void OnClick_Menu()
        {
            if (isProcessing)
                return;

            if (currentScreen.screenType == SCREEN_TYPE.MENU)
                return;

            isProcessing = true;
            Timer.Schedule(this, Core.Screen_FadeTime * 2f, () => isProcessing = false);

            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.MENU, currentScreen == null ? SCREEN_TYPE.none : currentScreen.screenType);
            SwitchButtonDownAnimation(Btn_Menu);
        }

        private void OnClick_Share()
        {
            if (isProcessing)
                return;

            if (currentScreen.screenType == SCREEN_TYPE.SHARE)
                return;

            isProcessing = true;
            Timer.Schedule(this, Core.Screen_FadeTime * 2f, () => isProcessing = false);

            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.SHARE, currentScreen == null ? SCREEN_TYPE.none : currentScreen.screenType);
            SwitchButtonDownAnimation(Btn_Share);
        }

        private void OnClick_Wallet()
        {
            if (isProcessing)
                return;

            if (currentScreen.screenType == SCREEN_TYPE.WALLET)
                return;

            isProcessing = true;
            Timer.Schedule(this, Core.Screen_FadeTime * 2f, () => isProcessing = false);

            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WALLET, currentScreen == null ? SCREEN_TYPE.none : currentScreen.screenType);
            SwitchButtonDownAnimation(Btn_Wallet);
            
        }

        private void OnClick_Profile()
        {
            if (isProcessing)
                return;

            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.USER_PROFILE, currentScreen.screenType);
            base.Hide();
        }

        private void Onclick_Exit()
        {
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.EXIT);
        }
    }
}