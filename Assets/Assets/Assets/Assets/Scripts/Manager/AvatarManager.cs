using Cashbaazi.App.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cashbaazi.App.Manager
{
    public class AvatarManager : Singleton<AvatarManager>
    {
        [Space(20)]
        [SerializeField] Sprite[] allAvatars;

        public Sprite[] Get_All()
        {
            return allAvatars;
        }
        public Sprite Get_AvatarSprite(int _index)
        {
            return allAvatars[_index];
        }
        public Sprite Get_AvatarSprite(string _index)
        {
            return allAvatars[Convert.ToInt32(_index)];
        }
    }
}