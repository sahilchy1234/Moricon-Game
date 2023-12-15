using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Cashbaazi.App.Common;
using UnityEngine.SceneManagement;

using Cashbaazi.App.Manager;
using Cashbaazi.App.Helper;

namespace Cashbaazi.App.Screen
{
    public class Screen_WithdrawlRequest : ISCREEN
    {
        [SerializeField] GameObject withDrawlPanel;
        [SerializeField] GameObject iconImage;
        [SerializeField] GameObject WithdrawlText;
        [SerializeField] Button Btn_Back;

        [Header("Refrences")]
        [SerializeField] Screen_Common commonScreen;
        [SerializeField] Screen_Wallet walletScreen;

        void Start()
        {
            Btn_Back.onClick.AddListener(OnClick_Back);
          //  LeanTween.scale(iconImage, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic).setOnComplete(LevelComplete);
            LeanTween.moveLocal(iconImage, new Vector3(0f, -786f, 2f), 0.7f).setDelay(.2f).setEase(LeanTweenType.easeInOutCubic);
          //  LeanTween.scale(iconImage, new Vector3(1f, 1f, 1f), 2f).setDelay(1.7f).setEase(LeanTweenType.easeInOutCubic);
        }

        public override void Show()
        {
           // LeanTween.scale(iconImage, new Vector3(1f, 1f, 1f), 2f).setDelay(.5f).setEase(LeanTweenType.easeOutElastic).setOnComplete(LevelComplete);
            LeanTween.moveLocal(iconImage, new Vector3(0f, -672f, 2f), 0.7f).setDelay(.2f).setEase(LeanTweenType.easeInOutCubic);
          //  LeanTween.scale(iconImage, new Vector3(1f, 1f, 1f), 2f).setDelay(1.7f).setEase(LeanTweenType.easeInOutCubic);
            base.Show();
        }

        void LevelComplete()
        {
            LeanTween.moveLocal(WithdrawlText, new Vector3(0f, -267f, 0f), 0.7f).setDelay(.2f).setEase(LeanTweenType.easeOutCirc);
            commonScreen.WalletUpdate();
        }

        private void OnClick_Back()
        {
            // ScreenManager.instance.HideScreen(this.screenType);
            LeanTween.moveLocal(iconImage, new Vector3(0f, -1600f, 2f), 0.7f).setDelay(2f).setEase(LeanTweenType.easeInOutCubic);
            SceneManager.LoadScene("MENU");
        }
    }
}