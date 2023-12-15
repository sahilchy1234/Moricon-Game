using Cashbaazi.App.Common;
using Cashbaazi.App.Helper;
using Cashbaazi.App.Screen;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cashbaazi.App.Screen
{
    public class Update_PaytmDetails : ISCREEN
{

    [SerializeField] TMP_InputField inputPaytmNum;
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
        if (string.IsNullOrEmpty(inputPaytmNum.text))
        {
            Toast.ShowToast("Please enter valid  number");
            return;
        }
        ApiManager.instance.API_PayoutRequestPaytm(inputPaytmNum.text,
           () =>
           {
               Toast.ShowToast("Data Saved successfully");
               ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WITHDRAW_PAYTM, this.screenType);
           },
           () =>
           {
               //Toast.ShowToast("Your account is not active, for more information kindly contact us!");
               Toast.ShowToast("Some error occured while fetching data.");
           });
    }
}
}
