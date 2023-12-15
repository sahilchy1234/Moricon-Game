using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Cashbaazi.Game.GunsBottleGame
{
    public class Effects : MonoBehaviour
    {
        public float rotateSpeed;

        void Update()
        {
            transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
        }
    }
}