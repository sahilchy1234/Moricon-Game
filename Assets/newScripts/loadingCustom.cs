using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingCustom : MonoBehaviour
{
    public static loadingCustom instance;
    public GameObject loadingSystem;

     void Awake() {
        instance = this;
    }

    public void ShowLoading () {
        loadingSystem.SetActive(true);
    }

      public void HideLoading () {
           loadingSystem.SetActive(false);
    }
}
