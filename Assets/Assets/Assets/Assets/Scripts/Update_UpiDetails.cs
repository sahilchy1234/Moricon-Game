using Cashbaazi.App.Common;
using Cashbaazi.App.Helper;
using Cashbaazi.App.Screen;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Update_UpiDetails : ISCREEN
{
    [SerializeField] TMP_InputField inputUpi;
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
        if (string.IsNullOrEmpty(inputUpi.text))
        {
            Toast.ShowToast("Please enter valid  number");
            return;
        }
        ApiManager.instance.API_UpdateUpiDetails(inputUpi.text,
           () =>
           {
               Toast.ShowToast("Data Saved successfully");
               ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WITHDRAW_UPI, this.screenType);
           },
           () =>
           {
               //Toast.ShowToast("Your account is not active, for more information kindly contact us!");
               Toast.ShowToast("Some error occured while fetching data.");
           });
    }
}
