using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

using Cashbaazi.App.Common;
using Cashbaazi.App.Manager;
using Cashbaazi.App.Helper;

namespace Cashbaazi.App.Screen
{
    public class Screen_Login : ISCREEN
    {
        [Space(20)]
        [SerializeField] Button Btn_NewAccount;
        [SerializeField] Button Btn_Next;

        [Space]
        [SerializeField] TMP_InputField Inpt_Number;
        

        private void Start()
        {
            Btn_NewAccount.onClick.AddListener(OnClick_NewAccount);
            Btn_Next.onClick.AddListener(OnClick_Next);          
        }
        public override void Show()
        {
            if (string.IsNullOrEmpty(ApiManager.instance.SessionToken))
            {
                base.Show();
                ScreenManager.instance.AddScreenToStack(this);
            }
            else
            {
                ApiManager.instance.API_GetUserDetails(() =>
                {
                    if (string.IsNullOrEmpty(ApiManager.instance.responce_userdata.name))
                        ScreenManager.instance.SwitchScreen(SCREEN_TYPE.USER_PROFILE_UPDATE, this.screenType);
                    else
                        SceneHandler.instance.SwitchScene(SCENE_TYPE.MENU.ToString());
                },
                () =>
                {
                    ApiManager.instance.SessionToken = string.Empty;

                    base.Show();
                    ScreenManager.instance.AddScreenToStack(this);
                });
            }
        }
        private void OnClick_NewAccount()
        {
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.REGISTER, this.screenType);
        }

        private void OnClick_Next()
        {
            if (string.IsNullOrEmpty(Inpt_Number.text)) 
            {
                Toast.ShowToast("Please enter valid mobile number");
                return;
            }

            ApiManager.instance.API_LoginSignup(Inpt_Number.text,
                () =>
                {
                    Toast.ShowToast("OTP sent successfully");
                    ScreenManager.instance.SwitchScreen(SCREEN_TYPE.OTP, this.screenType);
                },
                () =>
                {
                    //Toast.ShowToast("Your account is not active, for more information kindly contact us!");
                    Toast.ShowToast("Some error occured while fetching data.");
                });
        }
    }
}