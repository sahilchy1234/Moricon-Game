using Cashbaazi.Game.Common;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Cashbaazi.Game.GunsBottleGame
{
    public class BotMind_GunsBottleGame : BotMind
    {
        [SerializeField] float coolDown_TimeMin = 0.5f;
        [SerializeField] float coolDown_TimeMax = 2f;
        [SerializeField] float coolDown_TimeLeft;


        [Space]
        [SerializeField] int[] increaseScore_posibility  = new int[] { 2, -2 , 3, 1, 2, 4, -3, 1, 3 };
        [SerializeField] int[] increaseScore_posibilityHighScore  = new int[] { 5,4,-4,4,-2,-2,4,1,5,3 };
        public void Update()
        {
            if (!isMindActivated)
                return;

            if (!PhotonNetwork.IsMasterClient)
                return;

            coolDown_TimeLeft -= Time.deltaTime;
            if (coolDown_TimeLeft <= 0f)
                CalculateAndSendScore();

        }

        private void CalculateAndSendScore()
        {
            coolDown_TimeLeft = UnityEngine.Random.Range(coolDown_TimeMin, coolDown_TimeMax);

            int scoreToUpdate = increaseScore_posibilityHighScore[UnityEngine.Random.Range(0, increaseScore_posibilityHighScore.Length)];
            SendScoreToPlayer(scoreToUpdate);
        }

      /*  private void CalculateAndSendHighScore()
        {
            coolDown_TimeLeft = UnityEngine.Random.Range(coolDown_TimeMin, coolDown_TimeMax);

            int scoreToUpdate = increaseScore_posibility[UnityEngine.Random.Range(50, increaseScore_posibility.Length)];
            SendScoreToPlayer(scoreToUpdate);
        }*/
      
        public override void ActivateMind()
        {
            coolDown_TimeLeft = UnityEngine.Random.Range(coolDown_TimeMin, coolDown_TimeMax);
            base.ActivateMind();
        }
    }
}