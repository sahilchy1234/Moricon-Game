using Cashbaazi.App.Common;
using Cashbaazi.App.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cashbaazi.App.Screen
{
    public class Screen_WithdrawType : ISCREEN
    {
        [Space]
        [SerializeField] Button Btn_Paytm;
        [SerializeField] Button Btn_UPI;
        [SerializeField] Button Btn_Bank;
        [SerializeField] Button Btn_Back;
        [SerializeField] Button Btn_AmazonPay;
        private void Start()
        {
            Btn_Paytm.onClick.AddListener(OnClick_Paytm);
            Btn_UPI.onClick.AddListener(OnClick_Upi);
            Btn_Bank.onClick.AddListener(OnClick_Bank);
            Btn_Back.onClick.AddListener(OnClick_Back);
            Btn_AmazonPay.onClick.AddListener(OnClick_AmazonPay);
        }


        private void OnClick_Paytm()
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
                    if (string.IsNullOrEmpty(ApiManager.instance.responce_userdata.paytmNo))
                        ScreenManager.instance.SwitchScreen(SCREEN_TYPE.UPDATE_PAYTMNUM, this.screenType);
                    else
                        ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WITHDRAW_PAYTM, this.screenType);
                },
                () =>
                {
                    ApiManager.instance.SessionToken = string.Empty;

                    base.Show();
                    ScreenManager.instance.AddScreenToStack(this);
                });
            }
            //ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WITHDRAW_PAYTM);
            //Hide();
        }

        private void OnClick_Upi()
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
                    if (string.IsNullOrEmpty(ApiManager.instance.responce_userdata.upi))
                        ScreenManager.instance.SwitchScreen(SCREEN_TYPE.UPDATE_UPI_DETAILS, this.screenType);
                    else
                        ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WITHDRAW_UPI, this.screenType);
                },
                () =>
                {
                    ApiManager.instance.SessionToken = string.Empty;

                    base.Show();
                    ScreenManager.instance.AddScreenToStack(this);
                });
            }
            //ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WITHDRAW_UPI);
            //Hide();
        }

        private void OnClick_Bank()
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
                    if (string.IsNullOrEmpty(ApiManager.instance.responce_userdata.accountHolderName ))
                    {
                        ScreenManager.instance.SwitchScreen(SCREEN_TYPE.UPDATE_BANK_DETAILS, this.screenType);
                    }
                    else if  (string.IsNullOrEmpty(ApiManager.instance.responce_userdata.bankAccountNo))
                    {
                        ScreenManager.instance.SwitchScreen(SCREEN_TYPE.UPDATE_BANK_DETAILS, this.screenType);
                    }
                    else if (string.IsNullOrEmpty(ApiManager.instance.responce_userdata.bankName))
                    {
                        ScreenManager.instance.SwitchScreen(SCREEN_TYPE.UPDATE_BANK_DETAILS, this.screenType);
                    }
                       else if (string.IsNullOrEmpty(ApiManager.instance.responce_userdata.bankBranch))
                    {
                        ScreenManager.instance.SwitchScreen(SCREEN_TYPE.UPDATE_BANK_DETAILS, this.screenType);
                    }
                    else if (string.IsNullOrEmpty(ApiManager.instance.responce_userdata.ifsc))
                    {
                        ScreenManager.instance.SwitchScreen(SCREEN_TYPE.UPDATE_BANK_DETAILS, this.screenType);
                    }
                    else
                        ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WITHDRAW_BANK, this.screenType);
                },
                () =>
                {
                    ApiManager.instance.SessionToken = string.Empty;

                    base.Show();
                    ScreenManager.instance.AddScreenToStack(this);
                });
            }
        }

        private void OnClick_AmazonPay()
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
                    if (string.IsNullOrEmpty(ApiManager.instance.responce_userdata.amazonPayNo))
                        ScreenManager.instance.SwitchScreen(SCREEN_TYPE.UPDATE_AMAZONPAYNUM, this.screenType);
                    else
                        ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WITHDRAW_AMAZONPAY, this.screenType);
                },
                () =>
                {
                    ApiManager.instance.SessionToken = string.Empty;

                    base.Show();
                    ScreenManager.instance.AddScreenToStack(this);
                });
            }
        }    //  ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WITHDRAW_AMAZONPAY);
            //  Hide();


            private void OnClick_Back()
        {
            Hide();
        }
    }
}