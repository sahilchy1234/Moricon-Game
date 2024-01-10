using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
[Serializable]
public class LeaderboardEntry
{
    public int Position;
    public string PlayerName;
    public int Score;
}

public class Playfab_Leaderboard : MonoBehaviour
{
    public static Playfab_Leaderboard instance;

    public Transform leaderboardContainer;
    public GameObject leaderboardEntryPrefab;

    private LeaderboardEntry[] leaderboardEntries = new LeaderboardEntry[10]; // Assuming a fixed size of 10 entries

    void Start()
    {
        instance = this;
        // GetLeaderboard();
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "MorcoinScore",
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfully Leaderboard Updated");
        // Refresh the leaderboard after updating
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {

        InvokeRepeating("ShowLeaders", 2f, 0.2f);

        var request = new GetLeaderboardRequest
        {
            StatisticName = "MorcoinScore",
            StartPosition = 1,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        ClearLeaderboardEntries();

        int i = 0;
        foreach (var item in result.Leaderboard)
        {
            leaderboardEntries[i] = new LeaderboardEntry
            {
                Position = item.Position,
                PlayerName = item.DisplayName,
                Score = item.StatValue
            };

            // CreateLeaderboardEntry(leaderboardEntries[i]);
            i++;
        }
    }

    void ClearLeaderboardEntries()
    {
        foreach (Transform child in leaderboardContainer)
        {
            Destroy(child.gameObject);
        }
    }

    int k;

    void ShowLeaders()
    {
        if (leaderboardEntries.Length > k)
        {
            if (leaderboardEntries[k] != null)
            {
                CreateLeaderboardEntry(leaderboardEntries[k]);
                k++;
            }
        }
    }

    void CreateLeaderboardEntry(LeaderboardEntry entry)
    {
        GameObject entryObject = Instantiate(leaderboardEntryPrefab, leaderboardContainer);
        TMP_Text positionText = entryObject.transform.Find("Position").GetComponent<TMP_Text>();
        TMP_Text playerNameText = entryObject.transform.Find("PlayerName").GetComponent<TMP_Text>();
        TMP_Text scoreText = entryObject.transform.Find("Score").GetComponent<TMP_Text>();

        positionText.text = entry.Position.ToString();
        playerNameText.text = entry.PlayerName;
        scoreText.text = entry.Score.ToString();
    }

    void OnError(PlayFabError error)
    {
        Debug.LogError("PlayFab Error: " + error.ErrorMessage);
        // Handle errors appropriately in your game
    }
}
