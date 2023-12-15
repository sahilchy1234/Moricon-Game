using Cashbaazi.App.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Cashbaazi.App.Manager;
using Cashbaazi.App.Helper;

namespace Cashbaazi.App.Screen
{
    public class Screen_Withdraw_Paytm : ISCREEN
    {
        [Space] 
        [SerializeField] TMP_InputField inpt_amount;
        [SerializeField] TextMeshProUGUI txt_amount;
        [SerializeField] TextMeshProUGUI txt_fee;
        [SerializeField] TextMeshProUGUI txt_total;
        [SerializeField] Button Btn_Warning;
        [SerializeField] GameObject Obj_Warning;
        [SerializeField] GameObject iconImage;
        [SerializeField] TextMeshProUGUI Txt_WinningCash;

        [Space]
        [SerializeField] Button btn_Withdraw;
        [SerializeField] Button btn_Back;
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
          // AdsManager.instance.Show_Rewarded(() =>
          //  {
                if (string.IsNullOrEmpty(inpt_amount.text))
                {
                    Toast.ShowToast("Please input valid amount ");
                    return;
                }
                ApiManager.instance.API_PayoutRequestAmazonPay(inpt_amount.text, 2, () =>
                {
                   // Toast.ShowToast("Withdraw Success");
                    commonScreen.WalletUpdate();
                   // OnClick_Back();
                    LeanTween.moveLocal(iconImage, new Vector3(0f, 0f, 2f), 0.7f).setDelay(.01f).setEase(LeanTweenType.easeInOutCubic);
                    audioSource.PlayOneShot(soundSuccessful);
                    // ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WITHDRAWL_REQUEST);
                },
                () =>

                {
                   // Toast.ShowToast("");
                });
          
            // });
        }
        private void OnClick_SuccessBtn()
        {           
            ScreenManager.instance.HideScreen(this.screenType);        
        }

    }
}