using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cashbaazi.App.Helper
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T instance;

        [SerializeField] bool DontDestroy;

        public virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                if (DontDestroy)
                    DontDestroyOnLoad(this.gameObject);
            }
            else
                Destroy(this.gameObject);
        }
    }
}