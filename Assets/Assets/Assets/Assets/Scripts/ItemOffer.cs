using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Cashbaazi.App.Helper;
using Cashbaazi.App.Screen;

namespace Cashbaazi.App.Items
{
    public class ItemOffer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI txt_amount;
        [SerializeField] TextMeshProUGUI txt_title;
      //  [SerializeField] TextMeshProUGUI txt_description;
        [SerializeField] TextMeshProUGUI txt_code;
        [SerializeField] TextMeshProUGUI txt_discountAmount;

        

        [Space]
        [SerializeField] Button btn_main;
        [SerializeField] Responce_Offer offer_main;

        Screen_Offer offerScren;

        private void Start()
        {
            btn_main.onClick.AddListener(OnClick_Offer);
        }
        public void InitData(Responce_Offer _offer, Screen_Offer _offerScren)
        {
            offer_main = _offer;
            offerScren = _offerScren;

            txt_amount.text = offer_main.Amount + "";
            txt_title.text = offer_main.OfferName;
            txt_discountAmount.text = offer_main.DiscountAmount + "";
            txt_code.text = "#" + offer_main.Promocode;
           
            gameObject.name = offer_main.OfferId.ToString();
        }

        private void OnClick_Offer()
        {
            offerScren.Apply_Offer(offer_main);
        }
    }
}