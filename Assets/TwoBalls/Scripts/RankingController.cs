using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;/*
using PlayFab;
using PlayFab.ClientModels;*/
using Unity.Collections;

public class RankingController : MonoBehaviour
{

    /*playFabを1度だけ動かすためのBool型変数*//*
    bool playFabBool = true;


    public Text rankingText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playFabBool)
        {
            PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
            {
                CustomId = PlayerPrefs.GetString("userName"),
                CreateAccount = true
            }
            , result =>
            {
                Debug.Log("Success LOGIN");
                UpdateUserName();
                UpdatePlayerStatistics();
                GetLeaderboard();
                GetLeaderboardAroundPlayer();
                rankingText.text = $"Score:  {UIController.CalcScore()}\n";
            }
            , error =>
            {
                Debug.Log(error.GenerateErrorReport());
            });

            playFabBool = false;
        }
    }

    public void GetLeaderboard()
    {
        //GetLeaderboardRequestのインスタンスを生成
        var request = new GetLeaderboardRequest
        {
            //ランキング名(統計情報名)
            StatisticName = PlayerPrefs.GetString("Level"),
            //何位以降のランキングを取得するか
            StartPosition = 0,
            //ランキングデータを何件取得するか(最大100)
            MaxResultsCount = 5
        };



        *//*OnGetLeaderboardSuccess(new GetLeaderboardResult a){
            a=
        }*//*

        //ランキング(リーダーボード)を取得
        Debug.Log($"ランキング(リーダーボード)の取得開始");
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnGetLeaderboardFailure);
    }

    //ランキング(リーダーボード)の取得成功
    private void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    {
        Debug.Log($"ランキング(リーダーボード)の取得に成功しました");
        rankingText.text += "\nランキング\n";

        //result.Leaderboardに各順位の情報(PlayerLeaderboardEntry)が入っている
        foreach (var entry in result.Leaderboard)
        {
            rankingText.text += $"{entry.Position + 1}位:{entry.StatValue}   {entry.DisplayName}\n";
        }
    }

    //ランキング(リーダーボード)の取得失敗
    private void OnGetLeaderboardFailure(PlayFabError error)
    {
        Debug.LogError($"ランキング(リーダーボード)の取得に失敗しました\n{error.GenerateErrorReport()}");
    }


    public void GetLeaderboardAroundPlayer()
    {
        //GetLeaderboardAroundPlayerRequestのインスタンスを生成
        var request = new GetLeaderboardAroundPlayerRequest
        {
            //ランキング名(統計情報名)
            StatisticName = PlayerPrefs.GetString("Level"),
            //自分を含め前後何件取得するか
            MaxResultsCount = 1
        };

        //自分の順位周辺のランキング(リーダーボード)を取得
        Debug.Log($"自分の順位周辺のランキング(リーダーボード)の取得開始");
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerSuccess, OnGetLeaderboardAroundPlayerFailure);
    }

    //自分の順位周辺のランキング(リーダーボード)の取得成功
    private void OnGetLeaderboardAroundPlayerSuccess(GetLeaderboardAroundPlayerResult result)
    {
        Debug.Log($"自分の順位周辺のランキング(リーダーボード)の取得に成功しました");

        //result.Leaderboardに各順位の情報(PlayerLeaderboardEntry)が入っている
        foreach (var entry in result.Leaderboard)
        {
            rankingText.text += "\nあなたの順位\n";
            rankingText.text += $"{entry.Position + 1}位:{entry.StatValue}  {entry.DisplayName}\n";
        }
    }

    //自分の順位周辺のランキング(リーダーボード)の取得失敗
    private void OnGetLeaderboardAroundPlayerFailure(PlayFabError error)
    {
        Debug.LogError($"自分の順位周辺のランキング(リーダーボード)の取得に失敗しました\n{error.GenerateErrorReport()}");
    }

    *//*ユーザーネームの更新*//*
    public void UpdateUserName()
    {
        //ユーザ名を指定して、UpdateUserTitleDisplayNameRequestのインスタンスを生成
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = PlayerPrefs.GetString("userName")
        };

        //ユーザ名の更新
        Debug.Log($"ユーザ名の更新開始");
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateUserNameSuccess, OnUpdateUserNameFailure);
    }

    //ユーザ名の更新成功
    private void OnUpdateUserNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        //result.DisplayNameに更新した後のユーザ名が入ってる
        Debug.Log($"ユーザ名の更新が成功しました : {result.DisplayName}");
    }

    //ユーザ名の更新失敗
    private void OnUpdateUserNameFailure(PlayFabError error)
    {
        Debug.LogError($"ユーザ名の更新に失敗しました\n{error.GenerateErrorReport()}");
    }

    *//*スコアの更新*//*
    public void UpdatePlayerStatistics()
    {
        //UpdatePlayerStatisticsRequestのインスタンスを生成
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate{
                    //ランキング名(統計情報名)
                    StatisticName = PlayerPrefs.GetString("Level"),   
                    //スコア(int)
                    Value = PlayerPrefs.GetInt(PlayerPrefs.GetString("Level")),
                }
            }
        };

        //ユーザ名の更新
        Debug.Log($"スコア(統計情報)の更新開始");
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdatePlayerStatisticsSuccess, OnUpdatePlayerStatisticsFailure);
    }

    //スコア(統計情報)の更新成功
    private void OnUpdatePlayerStatisticsSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log($"スコア(統計情報)の更新が成功しました");
    }

    //スコア(統計情報)の更新失敗
    private void OnUpdatePlayerStatisticsFailure(PlayFabError error)
    {
        Debug.LogError($"スコア(統計情報)更新に失敗しました\n{error.GenerateErrorReport()}");
    }

    public static void SubmitScore(int playerScore, String Level)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = Level,
                    Value = playerScore
                }
            }
        }, result =>
        {
            Debug.Log($"スコア {playerScore} 送信完了！");
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }

    public void OnHomeBotton()
    {
        SceneManager.LoadScene("Title");
    }
*/
}
