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
public class Screen_UpdateBankDetails : ISCREEN
{ 
    [SerializeField] TMP_InputField inputYourNameHere;
    [SerializeField] TMP_InputField inputBankAccountNum;
    [SerializeField] TMP_InputField inputBankName;
    [SerializeField] TMP_InputField inputBankBranch;
    [SerializeField] TMP_InputField inputIfscCode;
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
        if (string.IsNullOrEmpty(inputYourNameHere.text))
        {
            Toast.ShowToast("Please enter valid Name ");
            return;
        }

        if (string.IsNullOrEmpty(inputBankAccountNum.text))
        {
            Toast.ShowToast("Please enter valid account number");
            return;
        }

        if (string.IsNullOrEmpty(inputBankName.text))
        {
            Toast.ShowToast("Please enter valid bank name");
            return;
        }

        if (string.IsNullOrEmpty(inputBankBranch.text))
        {
            Toast.ShowToast("Please enter valid branch name");
            return;
        }
        if (string.IsNullOrEmpty(inputIfscCode.text))
         {
             Toast.ShowToast("Please enter valid ifsc code");
             return;
         }

            ApiManager.instance.API_UpdateBankDetails(inputYourNameHere.text,inputBankAccountNum.text,inputBankName.text,inputBankBranch.text,inputIfscCode.text,
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
