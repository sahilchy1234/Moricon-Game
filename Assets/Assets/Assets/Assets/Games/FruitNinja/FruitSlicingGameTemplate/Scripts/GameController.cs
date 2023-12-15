using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Cashbaazi.Game.Common;

/// <summary>
/// This is an Enum that contains the different possible types of Bombs/Power-ups
/// </summary>
namespace Cashbaazi.Game.FruitNinja
{ 
public enum GameModes
{
    Classic,
    Arcade,
    Relax
}

/// <summary>
/// GameController Class handles all of the starting and stopping of rounds.  It also constantly monitors the game
/// state so that as soon as a lose/time up conditions is met... it'll end the game.  This class also keeps the UI
/// text fields updated with the score.
/// </summary>
public class GameController : MonoBehaviour
{
    public static GameController GameControllerInstance;        //static reference to this game controller
    private CountdownTimer roundTimer;                          //a reference to the countdown timer(connected to same parent body)
    [Header("Select GameMode for Testing Purposes.")]
    public GameModes gameModes;                                 //our game controllers GameModes Enum variable... gameModes
    [Header("Drop the 'GameOverCanvas' Here")]
    public GameObject gameOverPanel;                            //the gameOverPanel that hold the Game Over UI Image
  //  public GameObject[] blueXClassicMode;                       //classic mode blue x's.  The 3 X's on the UI that you start with in Classic Mode
  //  public GameObject[] redXClassicMode;                        //Classic mode red x's.  The XXX on the UI (under the Blue X's), that are show when you miss fruit.
 //   public Text classicText;                                    //the txt that is the classic mode current store
    public Text arcadeText;                                     //the txt that is the arcade mode current store
 //   public Text relaxText;                                      //the txt that is the relax mode current store
  //  public Text classicHighestText;                             //the txt that is the classic mode highest store
  //  public Text arcadeHighestText;                              //the txt that is the arcade mode highest store
  //  public Text relaxHighestText;                               //the txt that is the relax mode highest store
    public GameObject slicerGO;                                 //a reference to our FNCTouchSlicer
    private bool gameHasStarted;                                //boolean game has started???
    public bool gameIsRunning;                                 //boolean game is running??
    public float waitForMenuAtEnd;                              //how long to wait before the settings/pause menu pops up?

    // Use this for pre-initialization
    void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeRight;
        //our instance reference to this GameController is assigned THIS.
        GameControllerInstance = this;
        //roundTimer reference is assigned the CountdownTimer that is connected to this gameobject.
        roundTimer = GetComponent<CountdownTimer>();
        //we make sure the gameOverPanel is inactive... (the game just started, son!)
        gameOverPanel.SetActive(false);
        //boolean gameIsRunning is True.
        gameIsRunning = true;
    }


    //OnEnable is called when the object becomes enabled and active
    void OnEnable()
    {
        //we zero out all of our static variables dealing with score.
        GameVariables.FruitMissed = 0;
        GameVariables.ClassicModeScore = 0;
        GameVariables.ArcadeModeScore = 0;
        GameVariables.RelaxModeScore = 0;
        //then we RESET the splatterQuadSpawnDistance to 55f.  This static var is reset every round.  we increment it when we spawn a splatter, so that
        //they always spawn on top of the previous one... they stop around 45f worse case scenario, and in orthographic mode that is still acceptable.
        GameVariables.splatterQuadSpawnDistance = 55f;
    }

    // Use this for initialization
    void Start()
    {
        //start coroutine... ChooseGameModeAndCallRoundStart()... Long Name
        StartCoroutine(ChooseGameModeAndCallRoundStart());

        //and we start FNC_SlowUpdate.  We are trying to offload some secondary methods/workloads that don't need as frequent of updates.
        InvokeRepeating("FNC_SlowUpdate", 0.33f, 0.33f);
		
    }

    /// <summary>
    /// FNC_SlowUpdate Method again... Run Unimportant stuff here.  In this class at the least the UI updates will be in here.
    /// </summary>
    private void FNC_SlowUpdate()
    {
        //if game is running we will...
        if (gameIsRunning)
        {
            //call the updateUIText method.
            UpdateUIText();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if game is running we will...
        if (gameIsRunning)
        {
            MonitorGameState();
            //UpdateUIText();//moved to SlowUpdat()
        }

    }


    /// <summary>
    /// UpdateUIText does exactly what you would imagine.  It updates UI Text.
    /// </summary>
    private void UpdateUIText()
    {
        //we update all the current scores.
        arcadeText.text = GameVariables.ArcadeModeScore.ToString();
       
    }


    /// <summary>
    /// The UpdatePlayerExperienceAndLevel method adds the destroyed fruit the players "Experience". and
    /// then Stores the new experience in PlayerPrefs/
    /// </summary>
    /// <param name="fruitDestroyed"></param>
    private void UpdatePlayerExperienceAndLevel(int fruitDestroyed)
    {
        //update Experience with FruitDestroyed value
        GameVariables.Experience += fruitDestroyed;
        //save Experience in PlayerPrefs
        PlayerPrefs.SetInt(Tags.experience, GameVariables.Experience);

    }


    /// <summary>
    /// This UpdateHighestScore takes in the ending round score(if it is higher than the current highest score) and then depending
    /// on the selected mode/variable It will write the update to PlayerPrefs.
    /// </summary>
    /// <param name="amt"></param>
   

    /// <summary>
    /// This method stops the game time.
    /// </summary>
    private void StopTime()
    {
        //set Time.timeScale to 0f;
        Time.timeScale = 0.0f;
    }


    /// <summary>
    /// A redundant local private method to access our settings/menu instance var(and the method CallPauseAndMenu()).  So that we can invoke this
    /// Method about a half a second after round end. 
    /// </summary>
    private void LocalPauseAndSettingsMenuCall()
    {
        //SettingsAndPauseMenu.instance.CallPauseAndMenu();

        //call the settings menu onto the screen(without the pause).
       // SettingsAndPauseMenu.instance.CallMenuOnly();
    }


    /// <summary>
    /// MonitorGameState is the main Method in the GameController Class.  The Method 
    /// </summary>
    private void MonitorGameState()
    {

        


        
        if (gameModes == GameModes.Classic)
        {
            //the switch monitors how many missed fruit there are... It monitors the Static MissedFruit in GameVariables
            switch (GameVariables.FruitMissed)
            {
                //if zero fruit have been missed...
                case (0):
                    //Debug.Log("lost none yet");

                    //No fruit have been lost yet... Continue as you were...
                    //Access the LaunchControllers static Instance and call ReduceLaunchTimersAndLaunchObjects...
                    LauncherController.LaunchControllerInstance.ReduceLaunchTimersAndLaunchObjects();

                    break;
                //if one fruit have been missed...
                case (1):
                    //Debug.Log("lost one so far");

                    //remove blueX 0 and set the redX 0 active... When the red X is activated its wiggle animation will play.
                 //   blueXClassicMode[0].SetActive(false);
                  //  redXClassicMode[0].SetActive(true);

                    //Access the LaunchControllers static Instance and call ReduceLaunchTimersAndLaunchObjects...
                    LauncherController.LaunchControllerInstance.ReduceLaunchTimersAndLaunchObjects();

                    break;
                //if two fruit have been missed...
                case (2):
                    //Debug.Log("lost two so far");

                    //move on to the second X's - Disable Blue, and Activate Red
                //    blueXClassicMode[1].SetActive(false);
                 //   redXClassicMode[1].SetActive(true);
                    //Access the LaunchControllers static Instance and call ReduceLaunchTimersAndLaunchObjects...
                    LauncherController.LaunchControllerInstance.ReduceLaunchTimersAndLaunchObjects();


                    break;
                //if three or more have been lost the game is over....
                default:
                        //Debug.Log("3 or some other amount...");

                        //disable the final blue x, and enable the final red x.
                        //  blueXClassicMode[2].SetActive(false);
                        //  redXClassicMode[2].SetActive(true);

                        //Debug.Log("Game Over");

                        //activate the gameOverPanel
                        Screen.orientation = ScreenOrientation.Portrait;
                        gameOverPanel.SetActive(true);
                       
                        //disable the FNCTouchSlicer so the player does not pick-up any more fruit(monitoring/scoring are about to be turned off)
                        slicerGO.SetActive(false);

                    //invoke our settings/menu canvas to provoke a response...
                    Invoke("LocalPauseAndSettingsMenuCall", waitForMenuAtEnd);

                    //if we access the GameVariables Static variables, and check... Is the HighestClassicModeScore less than the current ClassicModeScore??

                    //and we update Player Experience by calling UpdatePlayerExperienceAndLevel and pass in GameVariables.ClassicModeScore.
                    UpdatePlayerExperienceAndLevel(GameVariables.ClassicModeScore);

                    //set gameIsRunning to false.
                    gameIsRunning = false;

                    //and Break! we are done with this case.
                    break;
            }
        }


        ////////////////////////////////
        ////_____ARCADE-MODE_______////
        ///////////////////////////////


        //if the current game mode is Arcade...
        if (gameModes == GameModes.Arcade)
        {
            //if roundTimer.timeLeft is less than or equal to 0.01 && gameHasStarted
            if (roundTimer.timeLeft <= 0.01 && gameHasStarted)
            {
                    //Debug.Log("Game Over");

                    //activate the gameOverPanel
                    // gameOverPanel.SetActive(true);

                    //FNCTouchSlicer gets disabled
                    Screen.orientation = ScreenOrientation.Portrait;
                    slicerGO.SetActive(false);

                //invoke our settings/menu canvas to provoke a response...
              //  Invoke("LocalPauseAndSettingsMenuCall", waitForMenuAtEnd);

                //if GameVariables.ArcadeModeHighestScore is less than GameVariables.ArcadeModeScore then clearly we need to update the highest score
                

                //We also need to update player experience... we pass in our new score to do that.
                UpdatePlayerExperienceAndLevel(GameVariables.ArcadeModeScore);

                //and we set gameIsRunning to false
                gameIsRunning = false;

            }
            //else... game clock is still running, and we should still be launching...
            else
            {
                //Access the LaunchControllers static Instance and call ReduceLaunchTimersAndLaunchObjects...
                LauncherController.LaunchControllerInstance.ReduceLaunchTimersAndLaunchObjects();
            }
        }


        ////////////////////////////////
        ////______RELAX-MODE_______////
        ///////////////////////////////


        //if the current game mode is Arcade...
        if (gameModes == GameModes.Relax)
        {
            //if roundTimer.timeLeft is less than or equal to 0.01 && gameHasStarted
            if (roundTimer.timeLeft <= 0.01 && gameHasStarted)
            {
                //Debug.Log("Game Over");

                //activate the gameOverPanel
                gameOverPanel.SetActive(true);

                //FNCTouchSlicer gets disabled
                slicerGO.SetActive(false);

                //invoke our settings/menu canvas to provoke a response...
                Invoke("LocalPauseAndSettingsMenuCall", waitForMenuAtEnd);



                //We also need to update player experience... we pass in our new score to do that.
                UpdatePlayerExperienceAndLevel(GameVariables.RelaxModeScore);

                //and we set gameIsRunning to false
                gameIsRunning = false;

            }
            //else... game clock is still running, and we should still be launching...
            else
            {
                //Access the LaunchControllers static Instance and call ReduceLaunchTimersAndLaunchObjects...
                LauncherController.LaunchControllerInstance.ReduceLaunchTimersAndLaunchObjects();

            }
        }
    }


    /// <summary>
    /// Method called by Coroutine to start a Classic Mode round.  If hasTimer is true then we call StartTimer on our CountdownTimer Class, and we
    /// pass the amount of time that should be on the clock.  Then we change the boolean "gameHasStarted" which will remain true until round end.
    /// </summary>
    /// <param name="hasTimer"></param>
    /// <param name="gameTime"></param>

    /// <summary>
    /// Method called by Coroutine to start a Arcade Mode round.  If hasTimer is true then we call StartTimer on our CountdownTimer Class, and we
    /// pass the amount of time that should be on the clock.  Then we change the boolean "gameHasStarted" which will remain true until round end.
    /// </summary>
    /// <param name="hasTimer"></param>
    /// <param name="gameTime"></param>
    private void StartArcadeModeGame(bool hasTimer, float gameTime)
    {
        //if has timer is true then we...  **(arcade mode does)
        if (hasTimer)
        {
            //will call StartTimer and pass in the gameTime(60f) for Arcade Mode...
            roundTimer.StartTimer(gameTime);

        }

        //then we set gameHasStarted to true, and start slicing!
        gameHasStarted = true;
    }

    /// <summary>
    /// Method called by Coroutine to start a Relax Mode round.  If hasTimer is true then we call StartTimer on our CountdownTimer Class, and we
    /// pass the amount of time that should be on the clock.  Then we change the boolean "gameHasStarted" which will remain true until round end.
    /// </summary>
    /// <param name="hasTimer"></param>
    /// <param name="gameTime"></param>
    private void StartRelaxModeGame(bool hasTimer, float gameTime)
    {
        //if has timer is true then we...  **(relax mode does)
        if (hasTimer)
        {
            //will call StartTimer and pass in the gameTime(60f) for Arcade Mode...
            roundTimer.StartTimer(gameTime);

        }

        //then we set gameHasStarted to true, and start slicing!
        gameHasStarted = true;

    }

    /// <summary>
    /// This Method Zeros out the "timeLeft" and disables the FNCTouchSlicer.
    /// </summary>
    private void ZeroGameTimeAndEndGame()
    {
        //assign a value of 0f to roundTimer.timeLeft.
        roundTimer.timeLeft = 0f;

        //disable FNC.touchSlicer GO
        slicerGO.SetActive(false);

    }


    /// <summary>
    /// This method Ends the Game. It activates all the Red X's, Disables the Blue X's, and sets FruitMissed to 5.
    /// </summary>
    public void TakeFruitAndEndGame()
    {
        //set our static var FruitMissed to 5
        GameVariables.FruitMissed = 5;

        //if the gameModes is equal to GameModes.Classic
        if (gameModes == GameModes.Classic)
        {
            //deactivate the slicer Game Object
            slicerGO.SetActive(false);

            //use a for loop to go through all of the Blue x's...
         //   for (int i = 0; i < blueXClassicMode.Length; i++)
            {
                ///if we find a blue X that is active then we...
          //      if (blueXClassicMode[i].activeInHierarchy)
                {
                    //disable it..
            //        blueXClassicMode[i].SetActive(false);
                }
            }
            //use for loop to go through all of the Red x's...
          //  for (int i = 0; i < redXClassicMode.Length; i++)
            {
                //if we find a Red X that is active then we...
         //       if (!redXClassicMode[i].activeInHierarchy)
                {
                    //disable it...
           //         redXClassicMode[i].SetActive(true);
                }
            }
        }
    }



    /// <summary>
    /// This Method is called at the start of the scene... It starts the game with the appropriate gameObjects/Settings
    /// </summary>
    /// <returns></returns>
    IEnumerator ChooseGameModeAndCallRoundStart()
    {
        switch (gameModes)
        {


            //if we are in GameModes.Classic then we need to...




            //if we are in GameModes.Arcade then we need to...
            case GameModes.Arcade:

                //wait for a few seconds while "60 seconds" and "Go!!" text slide on screen and then fade.
                yield return new WaitForSeconds(3.5f);

                    //our Static variable GameVariables.ArcadeModeScore needs to be set to zero (to make sure it is cleared)
                    PhotonGameController.instance.UpdateScore(0);

                    //then we call StartArcadeModeGame() and pass in true(hasTimer), and 60f(TimerTime)
                    StartArcadeModeGame(true, 60f);

                //break... we are done.
                break;



            //if we are in GameModes.Relax then we need to...
           


     

        }

        //yield return null... this coroutine is over!
        yield return null;
    }

}
    }