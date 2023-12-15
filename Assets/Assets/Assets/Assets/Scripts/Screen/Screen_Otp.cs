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
    public class Screen_Otp : ISCREEN
    {
        [Space(20)]
        [Space]
        [SerializeField] TextMeshProUGUI Txt_OtpSentNumber;
        [SerializeField] TextMeshProUGUI Txt_ResendTime;
        [SerializeField] TextMeshProUGUI Txt_TempOtp;

        [Space]
        [SerializeField] Button Btn_ChangeNumber;
        [SerializeField] Button Btn_SendOtpAgain;
        [SerializeField] Button Btn_Next;

        [Space]
        [SerializeField] TMP_InputField Inpt_OTP;

        [Space]
        [SerializeField] float Resend_MaxTime;
        [SerializeField] float Resend_TimeLeft;

        private void Start()
        {
            Btn_ChangeNumber.onClick.AddListener(OnClick_ChangeNumber);
            Btn_SendOtpAgain.onClick.AddListener(OnClick_SendOtpAgain);
            Btn_Next.onClick.AddListener(OnClick_Next);
        }
        private void Update()
        {
            Resend_TimeLeft -= Time.deltaTime;
            if (Resend_TimeLeft < 0) Resend_TimeLeft = 0;
            Txt_ResendTime.text = string.Format("Didn't get OTP?\nTry Again in : {0:00} seconds", (int)Resend_TimeLeft);

            Btn_SendOtpAgain.gameObject.SetActive(Resend_TimeLeft <= 0);
        }



        public override void Show()
        {
            Resend_TimeLeft = Resend_MaxTime;
            Txt_OtpSentNumber.text = string.Format("OTP sent to {0}", ApiManager.instance.responce_loginsignup.phoneno);
            Txt_TempOtp.text = ApiManager.instance.responce_loginsignup.tempotp.ToString();

            base.Show();

            ScreenManager.instance.AddScreenToStack(this);
        }



        private void OnClick_ChangeNumber()
        {
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.LOGIN, this.screenType);
        }

        private void OnClick_SendOtpAgain()
        {
            Resend_TimeLeft = Resend_MaxTime;
            ApiManager.instance.API_LoginSignup(ApiManager.instance.responce_loginsignup.phoneno,
                () =>
                {
                    Toast.ShowToast("OTP sent again successfully");
                },
                () =>
                {
                    Toast.ShowToast("Something went wrong");
                });
        }

        private void OnClick_Next()
        {
            if (Inpt_OTP.text != ApiManager.instance.responce_loginsignup.tempotp.ToString())
            {
                Toast.ShowToast("OTP does not match");
                return;
            }

            ApiManager.instance.API_OtpLogin(Inpt_OTP.text, ApiManager.instance.responce_loginsignup.phoneno,
                () =>
                {
                    if(string.IsNullOrEmpty(ApiManager.instance.responce_userdata.name))
                        ScreenManager.instance.SwitchScreen(SCREEN_TYPE.USER_PROFILE_UPDATE, this.screenType);
                    else
                        SceneHandler.instance.SwitchScene(SCENE_TYPE.MENU.ToString());
                },
                () =>
                {
                    Toast.ShowToast("Something went wrong");
                });
        }
    }
}