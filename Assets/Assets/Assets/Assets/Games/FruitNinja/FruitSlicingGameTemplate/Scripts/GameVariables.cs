﻿using UnityEngine;
using Cashbaazi.Game.Common;
/// <summary>
/// Static Class that holds some of the Game/Player Data.  At this Time we save some of these
/// values in PlayerPrefs.  Later I will setup a BinaryFormatter/FileStream to save the 
/// necessary data to a bin file.
/// </summary>
namespace Cashbaazi.Game.FruitNinja
{
    public static class GameVariables
    {

        public static int FruitMissed;                                                              //The number of fruit missed in a given round(important for classicMode).
        public static int Experience = PlayerPrefs.GetInt(Tags.experience);                         //the players experience which is stored via PlayerPrefs
        public static int Level                                                                     //the players level which is the players experience divided by 500
        {
            get
            {
                return Experience / 500;
            }

            set
            {
                Experience = value * 500;
            }
        }
        public static float splatterQuadSpawnDistance = 55f;                                        //this is the distance from 0,0,0 on the z-axis that the fruit splatters are spawned
        public static int ArcadeModeScore;                                                          //the score var used for each round of arcade mode
        public static int ArcadeModeHighestScore = PlayerPrefs.GetInt(Tags.highestArcadeScore);     //the Highest Score achieved in arcadeMode which is stored via PlayerPrefs
        public static int ClassicModeScore;                                                         //the score var used for each round of classic mode
        public static int ClassicModeHighestScore = PlayerPrefs.GetInt(Tags.highestClassicScore);   //the Highest Score achieved in classicMode which is stored via PlayerPrefs
        public static int RelaxModeScore;                                                           //the score var used for each round of relax mode
        public static int RelaxModeHighestScore = PlayerPrefs.GetInt(Tags.highestRelaxScore);       //the Highest Score achieved in relaxMode which is stored via PlayerPrefs
        public static float soundVolume = 0.8f;                                                     //global sound volume
        public static int mutedVolume = PlayerPrefs.GetInt("mutedAudio");                           //Saves state of the Muted Audio Boolean

        public static int dojoBGNum = PlayerPrefs.GetInt("BGint");                                  // the int for the selectedBg(we get the last stored val).  We set this value in the UIDojoSelector


        public static GameObject screenFaderPrefab =                                                //Reference to the location of ScreenFader Prefab(for editor use only)
    Resources.Load("Prefabs/MODFaderPrefab(DontDestroyOnLoad)") as GameObject;


        /*"Prefabs/FaderCanvas(DontDestroyOnLoad)"*/

        //****Notes****
        //The reference to the Screen Fader Prefab(in Resources) is there so
        //that the game can be started from any scene, and the prefab will
        //be created if its needed and missing from scene(it normally lives in scene0)
        //and persists from that point on...  The game SHOULD be started from Scene0
        //for a proper experience.  Things may go wrong if not... 

        //In editor... hard to do.  We just want to check out individual scenes and hit play.  

        //In Release/Build Time.... easy to do... it starts from the beginning every time.

        //Shouldn't be an issue anymore... there is a fade caller on every camera... 6/16/16


    }
}