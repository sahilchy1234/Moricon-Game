using Cashbaazi.App.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Cashbaazi.App.Helper;
using Cashbaazi.App.Manager;

namespace Cashbaazi.App.Screen
{
    public class Screen_AddWallet : ISCREEN
    {
        [Space(20)]
        [SerializeField] TMP_InputField Inpt_Amount;
        [SerializeField] TextMeshProUGUI Txt_offer;
        [SerializeField] Button Btn_Add;
        [SerializeField] Button Btn_Back;

        [Header("Refrences")]
        [SerializeField] Screen_Common commonScreen;
        [SerializeField] Screen_Offer offerScreen;

       // [SerializeField] GameObject iconImage;

        UniWebView webView;
        const string SuccessURL = "https://morcoin.in/payment/PaymentSuccess";
        const string FailURL = "https://morcoin.in/payment/PaymentFailed";


        private void Start()
        {
            Btn_Add.onClick.AddListener(OnClick_Add);
            Btn_Back.onClick.AddListener(OnClick_Back);
         //   LeanTween.moveLocal(iconImage, new Vector3(0f, -672f, 2f), 0.7f).setDelay(.2f).setEase(LeanTweenType.easeInOutCubic);
        }


        public override void Show()
        {
            Txt_offer.text = offerScreen.selected_offer == null || string.IsNullOrEmpty(offerScreen.selected_offer.Promocode) ? "" : "promocode applied #" + offerScreen.selected_offer.Promocode;
         //   LeanTween.moveLocal(iconImage, new Vector3(0f, -672f, 2f), 0.7f).setDelay(2f).setEase(LeanTweenType.easeInOutCubic);
            base.Show();
            ScreenManager.instance.AddScreenToStack(this);
        }


        private void OnClick_Add()
        {
            if (string.IsNullOrEmpty(Inpt_Amount.text))
            {
                Toast.ShowToast("Please input valid amount");
                return;
            }

            if (offerScreen.selected_offer == null || string.IsNullOrEmpty(offerScreen.selected_offer.Promocode))
            {
                ApiManager.instance.API_SaveTempPayment(Inpt_Amount.text,false,
                    () =>
                    {
                        var webViewGameObject = new GameObject("UniWebView");
                        webView = webViewGameObject.AddComponent<UniWebView>();
                        webView.Frame = new Rect(0, 0, UnityEngine.Screen.width, UnityEngine.Screen.height);
                        webView.Load("https://morcoin.in/payment/UPIAPIPaymentProcessing/" + ApiManager.instance.tempPaymentData.TransactionId);
                        webView.Show();

                        webView.OnPageFinished += WebView_OnPageFinished;


                        Inpt_Amount.text = string.Empty;
                       // Toast.ShowToast("amount added! ENJOY");
                        commonScreen.WalletUpdate();
                    },
                    () =>
                    {
                        Toast.ShowToast("Something went wrong");
                    });
            }
            else
            {
                ApiManager.instance.API_AppyPromocode(Inpt_Amount.text, offerScreen.selected_offer.Promocode,
                () =>
                {
                    ApiManager.instance.API_SaveTempPayment(Inpt_Amount.text, true,
                    () =>
                    {
                        offerScreen.selected_offer = new Responce_Offer();
                        var webViewGameObject = new GameObject("UniWebView");
                        webView = webViewGameObject.AddComponent<UniWebView>();
                        webView.Frame = new Rect(0, 0, UnityEngine.Screen.width, UnityEngine.Screen.height);
                        webView.Load("https://morcoin.in/payment/UPIAPIPaymentProcessing/" + ApiManager.instance.tempPaymentData.TransactionId);
                        webView.Show();

                        webView.OnPageFinished += WebView_OnPageFinished;
                        Inpt_Amount.text = string.Empty;
                       // Toast.ShowToast("Winning amount added! ENJOY");
                        commonScreen.WalletUpdate();
                    },
                    () =>
                    {
                        Toast.ShowToast("Something went wrong");
                    });
                },
                () =>
                {
                    Toast.ShowToast("Please enter exact offer amount");
                });
            }
        }
        private void OnClick_Back()
        {
            ScreenManager.instance.HideScreen(this.screenType);
          //  LeanTween.moveLocal(iconImage, new Vector3(0f, -1520f, 2f), 0.7f).setDelay(.2f).setEase(LeanTweenType.easeInOutCubic);
        }


        private void WebView_OnPageFinished(UniWebView webView, int statusCode, string url)
        {
            if (SuccessURL == url)
            {
                webView.OnPageFinished -= WebView_OnPageFinished;

                ApiManager.instance.API_GetUserDetails(() =>
                {
                    Inpt_Amount.text = string.Empty;
                    Toast.ShowToast("Amount added successfully! ENJOY");
                    commonScreen.WalletUpdate();
                },
                () =>
                {
                    Toast.ShowToast("Something went wrong");
                });


                webView.Hide();
                Destroy(webView.gameObject);
            }
            else if (FailURL == url)
            {
                webView.OnPageFinished -= WebView_OnPageFinished;
                Toast.ShowToast("Transaction failed! Please try again");

                webView.Hide();
                Destroy(webView.gameObject);
            }
        }
    }
}