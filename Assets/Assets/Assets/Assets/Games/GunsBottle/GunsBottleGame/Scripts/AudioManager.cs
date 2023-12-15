﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Cashbaazi.Game.GunsBottleGame
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        public Sounds[] sounds;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                //  DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            foreach (Sounds s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }


        }
        void Start()
        {
            Play("theme");
        }

        public void Play(string name)
        {
            Sounds s = System.Array.Find(sounds, sound => sound.name == name);
            if (s == null)

                return;
            s.source.Play();
        }
    }
}
