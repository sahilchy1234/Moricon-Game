using Cashbaazi.App.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cashbaazi.App.Manager;
using Cashbaazi.App.Helper;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

namespace Cashbaazi.App.Screen
{
    public class UserUpdateAmazonPayNumber : ISCREEN
    {
        [SerializeField] TMP_InputField inputAmazonPayNum;
        [SerializeField] Button updateBtn;
        [SerializeField] Button BackBtn;
        private void Start()
        {
            updateBtn.onClick.AddListener(OnClickUpdateBtn);
            BackBtn.onClick.AddListener(OnClickBackBtn);
        }

        private void OnClickBackBtn()
        {
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WALLET, this.screenType);
        }
        private void OnClickUpdateBtn()
        {
            if (string.IsNullOrEmpty(inputAmazonPayNum.text))
            {
                Toast.ShowToast("Please enter valid number");
                return;
            }
            ApiManager.instance.API_UpdateAmazonPayNum(inputAmazonPayNum.text,
               () =>
               {
                   Toast.ShowToast("Data Saved successfully");
                   ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WITHDRAW_AMAZONPAY, this.screenType);
               },
               () =>
               {
                   //Toast.ShowToast("Your account is not active, for more information kindly contact us!");
                   Toast.ShowToast("Some error occured while fetching data.");
               });
        }    
    }
}