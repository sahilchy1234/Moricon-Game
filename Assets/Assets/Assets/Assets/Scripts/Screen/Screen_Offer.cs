using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cashbaazi.App.Common;
using Cashbaazi.App.Manager;
using Cashbaazi.App.Items;
using Cashbaazi.App.Helper;

namespace Cashbaazi.App.Screen
{
    public class Screen_Offer : ISCREEN
    {
        [Space]
        [SerializeField] ItemOffer offer_prefab;
        [SerializeField] Transform offer_parent;

        [Header("Refrence")]
        [SerializeField] Screen_Common commonScreen;

        public Responce_Offer selected_offer;

        public override void Show()
        {
            selected_offer = null;

            base.Show();
            commonScreen.currentScreen = this;

            ScreenManager.instance.AddScreenToStack(this);

            if (offer_parent.childCount == 0)
            {
                ApiManager.instance.API_GetOfferList(() =>
                {
                    foreach (var item in ApiManager.instance.responce_offers)
                    {
                        ItemOffer offer = Instantiate(offer_prefab, offer_parent);
                        offer.InitData(item, this);
                    }

                    ApiManager.instance.API_GetUsedOfferList(() =>
                    {
                        foreach (var item in ApiManager.instance.responce_usedoffers)
                        {
                            for (int i = 0; i < offer_parent.childCount; i++)
                            {
                                if (offer_parent.GetChild(i).name == item.OfferId.ToString())
                                    offer_parent.GetChild(i).GetComponent<CanvasGroup>().interactable = false;
                                else
                                    offer_parent.GetChild(i).GetComponent<CanvasGroup>().interactable = true;
                            }
                        }
                    });
                },
                () =>
                {
                    Toast.ShowToast("Can't fetch offer for you now! Please try again later");
                });
            }
            else
            {
                ApiManager.instance.API_GetUsedOfferList(() =>
                {
                    foreach (var item in ApiManager.instance.responce_usedoffers)
                    {
                        for (int i = 0; i < offer_parent.childCount; i++)
                        {
                            if (offer_parent.GetChild(i).name == item.OfferId.ToString())
                                offer_parent.GetChild(i).GetComponent<CanvasGroup>().interactable = false;
                            else
                                offer_parent.GetChild(i).GetComponent<CanvasGroup>().interactable = true;
                        }
                    }
                });
            }
        }


        public void Apply_Offer(Responce_Offer offer)
        {
            selected_offer = offer;
            ScreenManager.instance.SwitchScreen(SCREEN_TYPE.ADD_WALLET);
        }
    }
}