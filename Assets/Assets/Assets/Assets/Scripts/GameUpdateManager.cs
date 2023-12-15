using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cashbaazi.App.Screen
{
    public class GameUpdateManager : MonoBehaviour
    {
      //  public string CurrentVer;
      //  public string URL;
       // public string LatesttVer;
        [SerializeField] GameObject UpdateScreen;
        [SerializeField] Button Btn_Update;
        void Start()
        {
            Btn_Update.onClick.AddListener(OnClick_UpdateNow);
            // StartCoroutine(LoadTextData(URL));

        }

        //public void CheckVersion()
        //{
        //    if (CurrentVer == LatesttVer)
        //    {
        //        UpdateScreen.SetActive(false);
        //    }

        //    else
        //    {
        //        UpdateScreen.SetActive(true);
        //    }
        //    Debug.Log("Cver " + CurrentVer + "  lver" + LatesttVer);
        //}


        //IEnumerator LoadTextData(string url)
        //{
        //    WWW www = new WWW(url);
        //    yield return www;
        //    LatesttVer = www.text;
        //    CheckVersion();
        //}

        public  void OnClick_UpdateNow()
        {
            Application.OpenURL("https://morcoin.in/site/DownloadApp");
        }
    }
}