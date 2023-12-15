using System.Collections;
using System.Collections.Generic;
using Cashbaazi.Game.Common;
using UnityEngine;

namespace Cashbaazi.Game.KnifeHit
{ 
     public class GameStarter_KnifeHit : Base_GameStarter
{
    public GameObject[] objectsForGames;

    public override void InitGame()
    {
        base.InitGame();

        foreach (var item in objectsForGames)
        {
            item.SetActive(true);
        }
    }

    public override void StopGame()
    {
        base.StopGame();

        foreach (var item in objectsForGames)
        {
            item.SetActive(false);
        }
    }
}

}
