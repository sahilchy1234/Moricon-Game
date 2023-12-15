using Cashbaazi.App.Common;
using Cashbaazi.App.Helper;
using Cashbaazi.App.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cashbaazi.App.Screen
{
    public class Screen_TransactionHistory : ISCREEN
    {
        [Space]
        [SerializeField] ItemTransaction item_preab;
        [SerializeField] Transform item_parent;

        [Space]
        [SerializeField] Button btnBack;

        [Header("Refrence")]
        [SerializeField] Screen_Common commonScreen;

        private void Start()
        {
            btnBack.onClick.AddListener(OnClick_Back);
        }


        public override void Show()
        {
            for (int i = 0; i < item_parent.childCount; i++)
                Destroy(item_parent.GetChild(i).gameObject);

            base.Show();

            ApiManager.instance.API_GetTransactionHistory(
                () => 
                {
                    foreach (var item in ApiManager.instance.responce_transactionHistory)
                    {
                        ItemTransaction transaction = Instantiate(item_preab, item_parent);
                        transaction.Init(item);
                    }
                }, 
                () => 
                {
                    Toast.ShowToast("Could not get transaction history at this moment");
                });
        }


        private void OnClick_Back()
        {
            commonScreen.WalletUpdate();
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.WALLET, this.screenType);
            
           // Hide();
        }
    }
}