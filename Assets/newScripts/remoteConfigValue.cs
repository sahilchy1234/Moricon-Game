using UnityEngine;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
using TMPro;
using Cashbaazi.App.Screen;

public class remoteConfigValue : MonoBehaviour
{
    public Screen_BattleSetup script;
    public TMP_Text[] betAmountText;
    public TMP_Text[] betReturnAmountText;

    public float[] betAmount;
    public float[] betReturnAmount;

    public struct userAttributes
    {
        // Optionally declare variables for any custom user attributes:
        // public bool expansionFlag;

        public float bet1;
        public float bet2;
        public float bet3;
        public float bet4;
        public float bet5;
        public float bet6;
        public float bet7;
        public float bet8;
        public float bet9;
    }

    public struct appAttributes
    {
        // Optionally declare variables for any custom app attributes:
        public int level;
        public int score;
        public string appVersion;
    }

    public struct floatAttributes
    {
        // Declare variables for custom float attributes:

        // Add more float attributes as needed
    }

    public struct filterAttributes
    {
        // Optionally declare variables for attributes to filter on any of the following parameters:
        public string[] key;
        public string[] type;
        public string[] schemaId;
    }

    async Task InitializeRemoteConfigAsync()
    {
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    // Retrieve and apply the current key-value pairs from the service on Awake:
    async Task Awake()
    {
        // initialize Unity's authentication and core services, however check for internet connection
        // in order to fail gracefully without throwing an exception if the connection does not exist
        if (Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfigAsync();
        }

        // Add a listener to apply settings when successfully retrieved:
        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteConfig;

        // Fetch configuration settings from the remote service, including floatAttributes
        await RemoteConfigService.Instance.FetchConfigsAsync(new userAttributes(), new appAttributes());
    }

    void ApplyRemoteConfig(ConfigResponse configResponse)
    {
        // Conditionally update settings, depending on the response's origin:


        // Retrieve float values using the keys "bet1" to "bet9"


        ///-----------------------------------------------------BetAmount-----------------------------------------------------
        float bet1Value = RemoteConfigService.Instance.appConfig.GetFloat("bet1");
        float bet2Value = RemoteConfigService.Instance.appConfig.GetFloat("bet2");
        float bet3Value = RemoteConfigService.Instance.appConfig.GetFloat("bet3");
        float bet4Value = RemoteConfigService.Instance.appConfig.GetFloat("bet4");
        float bet5Value = RemoteConfigService.Instance.appConfig.GetFloat("bet5");
        float bet6Value = RemoteConfigService.Instance.appConfig.GetFloat("bet6");
        float bet7Value = RemoteConfigService.Instance.appConfig.GetFloat("bet7");
        float bet8Value = RemoteConfigService.Instance.appConfig.GetFloat("bet8");
        float bet9Value = RemoteConfigService.Instance.appConfig.GetFloat("bet9");



        betAmount[0] = bet1Value;
        betAmount[1] = bet2Value;
        betAmount[2] = bet3Value;
        betAmount[3] = bet4Value;
        betAmount[4] = bet5Value;
        betAmount[5] = bet6Value;
        betAmount[6] = bet7Value;
        betAmount[7] = bet8Value;
        betAmount[8] = bet9Value;

        betAmountText[0].text = bet1Value.ToString();
        betAmountText[1].text = bet2Value.ToString();
        betAmountText[2].text = bet3Value.ToString();
        betAmountText[3].text = bet4Value.ToString();
        betAmountText[4].text = bet5Value.ToString();
        betAmountText[5].text = bet6Value.ToString();
        betAmountText[6].text = bet7Value.ToString();
        betAmountText[7].text = bet8Value.ToString();
        betAmountText[8].text = bet9Value.ToString();

        ///-----------------------------------------------------BetAmount-----------------------------------------------------







        float betReward1Value = RemoteConfigService.Instance.appConfig.GetFloat("betReturn1");
        float betReward2Value = RemoteConfigService.Instance.appConfig.GetFloat("betReturn2");
        float betReward3Value = RemoteConfigService.Instance.appConfig.GetFloat("betReturn3");
        float betReward4Value = RemoteConfigService.Instance.appConfig.GetFloat("betReturn4");
        float betReward5Value = RemoteConfigService.Instance.appConfig.GetFloat("betReturn5");
        float betReward6Value = RemoteConfigService.Instance.appConfig.GetFloat("betReturn6");
        float betReward7Value = RemoteConfigService.Instance.appConfig.GetFloat("betReturn7");
        float betReward8Value = RemoteConfigService.Instance.appConfig.GetFloat("betReturn8");
        float betReward9Value = RemoteConfigService.Instance.appConfig.GetFloat("betReturn9");



        betReturnAmount[0] = betReward1Value;
        betReturnAmount[1] = betReward2Value;
        betReturnAmount[2] = betReward3Value;
        betReturnAmount[3] = betReward4Value;
        betReturnAmount[4] = betReward5Value;
        betReturnAmount[5] = betReward6Value;
        betReturnAmount[6] = betReward7Value;
        betReturnAmount[7] = betReward8Value;
        betReturnAmount[8] = betReward9Value;

        betReturnAmountText[0].text = betReward1Value.ToString();
        betReturnAmountText[1].text = betReward2Value.ToString();
        betReturnAmountText[2].text = betReward3Value.ToString();
        betReturnAmountText[3].text = betReward4Value.ToString();
        betReturnAmountText[4].text = betReward5Value.ToString();
        betReturnAmountText[5].text = betReward6Value.ToString();
        betReturnAmountText[6].text = betReward7Value.ToString();
        betReturnAmountText[7].text = betReward8Value.ToString();
        betReturnAmountText[8].text = betReward9Value.ToString();
        ///


        switch (configResponse.requestOrigin)
        {
            case ConfigOrigin.Default:
                Debug.Log("No settings loaded this session and no local cache file exists; using default values.");
                break;
            case ConfigOrigin.Cached:
                Debug.Log("No settings loaded this session; using cached values from a previous session.");
                break;
            case ConfigOrigin.Remote:
                Debug.Log("New settings loaded this session; update values accordingly.");
                break;
        }

    }


    public void SetBattleFunction(int index)
    {
        int convertedAmount = (int)betAmount[index];
        script.Set_BattleAmount(convertedAmount);
    }

    public void Player2Function()
    {
        betReturnAmountText[0].text = betReturnAmount[0].ToString();
        betReturnAmountText[1].text = betReturnAmount[1].ToString();
        betReturnAmountText[2].text = betReturnAmount[2].ToString();
        betReturnAmountText[3].text = betReturnAmount[3].ToString();
        betReturnAmountText[4].text = betReturnAmount[4].ToString();
        betReturnAmountText[5].text = betReturnAmount[5].ToString();
        betReturnAmountText[6].text = betReturnAmount[6].ToString();
        betReturnAmountText[7].text = betReturnAmount[7].ToString();
        betReturnAmountText[8].text = betReturnAmount[8].ToString();
    }

    public void Player4Function()
    {
        betReturnAmountText[0].text = (betReturnAmount[0] * 2).ToString();
        betReturnAmountText[1].text = (betReturnAmount[1] * 2).ToString();
        betReturnAmountText[2].text = (betReturnAmount[2] * 2).ToString();
        betReturnAmountText[3].text = (betReturnAmount[3] * 2).ToString();
        betReturnAmountText[4].text = (betReturnAmount[4] * 2).ToString();
        betReturnAmountText[5].text = (betReturnAmount[5] * 2).ToString();
        betReturnAmountText[6].text = (betReturnAmount[6] * 2).ToString();
        betReturnAmountText[7].text = (betReturnAmount[7] * 2).ToString();
        betReturnAmountText[8].text = (betReturnAmount[8] * 2).ToString();
    }

}
