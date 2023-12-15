using Cashbaazi.App.Common;
using Cashbaazi.App.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Cashbaazi.App.Helper;

namespace Cashbaazi.App.Screen
{
    public class Screen_UpdateProfile : ISCREEN
    {
        [Space(20)]
        [SerializeField] Toggle[] Toggle_Avatars;
        [SerializeField] Toggle[] Toggle_Gender;

        [Space]
        [SerializeField] TMP_InputField Txt_Username;
        [SerializeField] TMP_InputField Txt_Email;
       




        [Space]
        [SerializeField] Button Btn_UpdateProfile;

        private void Start()
        {
            Btn_UpdateProfile.onClick.AddListener(OnClick_UpdateProfile);
        }

        public override void Show()
        {
            for (int i = 0; i < Toggle_Avatars.Length; i++)
                Toggle_Avatars[i].transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = AvatarManager.instance.Get_AvatarSprite(i);

            base.Show();
             
            ScreenManager.instance.AddScreenToStack(this);
        }

        private void OnClick_UpdateProfile()
        {
            if (string.IsNullOrEmpty(Txt_Username.text))
            {
                Toast.ShowToast("Enter valid username");
                return;
            }
            if (string.IsNullOrEmpty(Txt_Email.text))
            {
                Toast.ShowToast("Enter valid email id");
                return;
            }
           

            int avatarIndex = System.Array.FindIndex(Toggle_Avatars, x => x.isOn);
            string gender = System.Array.Find(Toggle_Gender, x => x.isOn).GetComponentInChildren<TextMeshProUGUI>().text;
            ApiManager.instance.API_UpdateProfile(Txt_Username.text, Txt_Email.text, avatarIndex, gender,
                () =>
                {
                    SceneHandler.instance.SwitchScene(SCENE_TYPE.MENU.ToString());
                },
                () =>
                {
                    Toast.ShowToast("Username Already Found,Try Another!");
                }) ;
        }
    }
}