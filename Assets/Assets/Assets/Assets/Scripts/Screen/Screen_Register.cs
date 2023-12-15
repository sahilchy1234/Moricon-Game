using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cashbaazi.App.Common;
using Cashbaazi.App.Manager;
using Cashbaazi.App.Helper;

namespace Cashbaazi.App.Screen
{
    public class Screen_Register : ISCREEN
    {
        [Space(20)]
        [SerializeField] Button Btn_Login;
        [SerializeField] Button Btn_Next;
        [SerializeField] Button Btn_TermsAndCondition;

        [Space]
        [SerializeField] TMP_InputField Inpt_Number;

        [Space]
        [SerializeField] Toggle Toggle_TermsCondition;

        private void Start()
        {
            Btn_Login.onClick.AddListener(OnClick_Login);
            Btn_Next.onClick.AddListener(OnClick_Next);
            Btn_TermsAndCondition.onClick.AddListener(OnClick_TermsAndCondition);
            Toggle_TermsCondition.onValueChanged.AddListener(OnValueChanged_TermsCondition);
        }


        public override void Show()
        {
            base.Show();
            Btn_Next.interactable = Toggle_TermsCondition.isOn;

            ScreenManager.instance.AddScreenToStack(this);
        }



        private void OnClick_Login()
        {
            
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.LOGIN, this.screenType);
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
                    Toast.ShowToast("Something went wrong");
                });
        }
        private void OnClick_TermsAndCondition()
        {
            Application.OpenURL("https://morcoin.in/site/TermsAndConditions");
        }
        private void OnValueChanged_TermsCondition(bool arg0)
        {
            Btn_Next.interactable = arg0;
        }
    }
}