using Cashbaazi.App.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cashbaazi.App.Common
{
    public class Loading : Singleton<Loading>
    {



        void Start()
        {
            Invoke("disableLoadingScreen",1f);
        }

        void disableLoadingScreen(){
            gameObject.SetActive(false);
        }

        [SerializeField] GameObject panel;

        public void ShowLoading()
        {
            panel.SetActive(true);
        }
        public void HideLoading()
        {
            panel.SetActive(false);
        }
    }
}