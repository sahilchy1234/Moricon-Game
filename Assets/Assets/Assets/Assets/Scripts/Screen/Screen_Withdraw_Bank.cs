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
    public class Screen_Withdraw_Bank : ISCREEN
    {
        [Space]
        [SerializeField] TMP_InputField inpt_amount;

        [SerializeField] TextMeshProUGUI Txt_WinningCash;

        [Space]
        [SerializeField] Button btn_Withdraw;
        [SerializeField] Button btn_Back;
        [SerializeField] GameObject iconImage;
        [SerializeField] Button Btn_Successful;
        [SerializeField] AudioClip soundSuccessful;
        AudioSource audioSource;

        [Header("Refrences")]
        [SerializeField] Screen_Common commonScreen;
        [SerializeField] Screen_Wallet walletScreen;


        public override void Show()
        {
            LeanTween.moveLocal(iconImage, new Vector3(0f, -2332f, 0f), 0f).setDelay(.0f).setEase(LeanTweenType.easeInOutCubic);
            Txt_WinningCash.text = String.Format("Balance - Rs.{0}", ApiManager.instance.responce_userdata.WinningWallet);
            base.Show();
        }
        private void Start()
        {
            btn_Withdraw.onClick.AddListener(OnClick_Withdraw);
            btn_Back.onClick.AddListener(OnClick_Back);
            Btn_Successful.onClick.AddListener(OnClick_SuccessBtn);
            audioSource = this.GetComponent<AudioSource>();
        }

        private void OnClick_Back()
        {
            ScreenManager.instance.HideScreen(this.screenType);
        }

        private void OnClick_Withdraw()
        {

            if (string.IsNullOrEmpty(inpt_amount.text))
            {
                Toast.ShowToast("Please input valid amount ");
                return;
            }
            ApiManager.instance.API_PayoutRequestAmazonPay(inpt_amount.text, 4, () =>
                {
                   // Toast.ShowToast("Withdraw Success");
                    commonScreen.WalletUpdate();
                    LeanTween.moveLocal(iconImage, new Vector3(0f, 0f, 2f), 0.7f).setDelay(.01f).setEase(LeanTweenType.easeInOutCubic);
                    audioSource.PlayOneShot(soundSuccessful);
                    //ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WITHDRAWL_REQUEST);
                  //  OnClick_Back();
                },
                () =>
                {
                    Toast.ShowToast("Something went wrong");
                });
            
        }

        private void OnClick_SuccessBtn()
        {
            ScreenManager.instance.HideScreen(this.screenType);
        }
    }
}