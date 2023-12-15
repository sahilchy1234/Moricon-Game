using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cashbaazi.App.Common;
using Cashbaazi.App.Manager;
using UnityEngine.UI;
using System;
using TMPro;
using Cashbaazi.App.Helper;

namespace Cashbaazi.App.Screen
{
    public class Screen_Wallet : ISCREEN
    {
        [Space(20)]
        [SerializeField] TMP_InputField  Inpt_RefralCode;
        [SerializeField] TextMeshProUGUI  Txt_PlayerCash;
        [SerializeField] Button Btn_AddWallet;
        [SerializeField] Button Btn_VerifyReferral;
        [SerializeField] Button Btn_Withdraw;
        [SerializeField] Button Btn_TransactionHistory;
        [SerializeField] Button Btn_Warning;
        [SerializeField] Button Btn_WinningInfoWarning;
        [SerializeField] Text Txt_DepositCash;
        [SerializeField] Text Txt_WinningCash;

        [Space]
        [SerializeField] GameObject Obj_Warning;
        [SerializeField] GameObject Obj_WinningInfoWarning;

        [Header("Refrence")]
        [SerializeField] Screen_Common commonScreen;

        private void Start()
        {
            Btn_AddWallet.onClick.AddListener(OnClick_AddWallet);
            Btn_VerifyReferral.onClick.AddListener(OnClick_VerifyRefral);
            Btn_Withdraw.onClick.AddListener(OnClick_Withdraw);
            Btn_TransactionHistory.onClick.AddListener(OnClick_TransactionHistory);
            Btn_Warning.onClick.AddListener(OnClick_Warning);
            Btn_WinningInfoWarning.onClick.AddListener(OnClick_WinningInfoWarning);
        }


        public override void Show()
        {
            UpdateWallet();

            base.Show();
            commonScreen.currentScreen = this;

            ScreenManager.instance.AddScreenToStack(this);
        }


        public void UpdateWallet()
        {
            Txt_PlayerCash.text = AppManager.instance.Get_PlayerWallet().ToString();
            Txt_DepositCash.text = ApiManager.instance.responce_userdata.MainWallet.ToString();
            Txt_WinningCash.text = ApiManager.instance.responce_userdata.WinningWallet.ToString();
        }
        


        private void OnClick_AddWallet()
        {
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.ADD_WALLET);
            
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
                   // ApiManager.instance.responce_userdata.MainWallet;
                });
            });
        }
        private void OnClick_Withdraw()
        {
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WITHDRAW_TYPE);
            
        }

        private void OnClick_TransactionHistory()
        {
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.TRANSACTION_HISTORY);
        }
        private void OnClick_Warning()
        {
            //Obj_Warning.SetActive(!Obj_Warning.activeInHierarchy);
            StartCoroutine(HideText());
        }
        IEnumerator HideText()
        {
            Obj_Warning.SetActive(true);          
            yield return new WaitForSeconds(3f);
            Obj_Warning.SetActive(false);
           
        }
        private void OnClick_WinningInfoWarning()
        {
            StartCoroutine(HideWinningInfoText());
        }

        IEnumerator HideWinningInfoText()
        {
            Obj_WinningInfoWarning.SetActive(true);
            yield return new WaitForSeconds(3f);
            Obj_WinningInfoWarning.SetActive(false);

        }
    }
}