using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cashbaazi.App.Common
{
    public class NoInternet : MonoBehaviour
    {
        [SerializeField] GameObject panel;

        private void Update()
        {
            panel.SetActive(Application.internetReachability == NetworkReachability.NotReachable);
        }
    }
}