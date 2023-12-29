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

    /*playFab��1�x�������������߂�Bool�^�ϐ�*//*
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
        //GetLeaderboardRequest�̃C���X�^���X�𐶐�
        var request = new GetLeaderboardRequest
        {
            //�����L���O��(���v���)
            StatisticName = PlayerPrefs.GetString("Level"),
            //���ʈȍ~�̃����L���O���擾���邩
            StartPosition = 0,
            //�����L���O�f�[�^�������擾���邩(�ő�100)
            MaxResultsCount = 5
        };



        *//*OnGetLeaderboardSuccess(new GetLeaderboardResult a){
            a=
        }*//*

        //�����L���O(���[�_�[�{�[�h)���擾
        Debug.Log($"�����L���O(���[�_�[�{�[�h)�̎擾�J�n");
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnGetLeaderboardFailure);
    }

    //�����L���O(���[�_�[�{�[�h)�̎擾����
    private void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    {
        Debug.Log($"�����L���O(���[�_�[�{�[�h)�̎擾�ɐ������܂���");
        rankingText.text += "\n�����L���O\n";

        //result.Leaderboard�Ɋe���ʂ̏��(PlayerLeaderboardEntry)�������Ă���
        foreach (var entry in result.Leaderboard)
        {
            rankingText.text += $"{entry.Position + 1}��:{entry.StatValue}   {entry.DisplayName}\n";
        }
    }

    //�����L���O(���[�_�[�{�[�h)�̎擾���s
    private void OnGetLeaderboardFailure(PlayFabError error)
    {
        Debug.LogError($"�����L���O(���[�_�[�{�[�h)�̎擾�Ɏ��s���܂���\n{error.GenerateErrorReport()}");
    }


    public void GetLeaderboardAroundPlayer()
    {
        //GetLeaderboardAroundPlayerRequest�̃C���X�^���X�𐶐�
        var request = new GetLeaderboardAroundPlayerRequest
        {
            //�����L���O��(���v���)
            StatisticName = PlayerPrefs.GetString("Level"),
            //�������܂ߑO�㉽���擾���邩
            MaxResultsCount = 1
        };

        //�����̏��ʎ��ӂ̃����L���O(���[�_�[�{�[�h)���擾
        Debug.Log($"�����̏��ʎ��ӂ̃����L���O(���[�_�[�{�[�h)�̎擾�J�n");
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetLeaderboardAroundPlayerSuccess, OnGetLeaderboardAroundPlayerFailure);
    }

    //�����̏��ʎ��ӂ̃����L���O(���[�_�[�{�[�h)�̎擾����
    private void OnGetLeaderboardAroundPlayerSuccess(GetLeaderboardAroundPlayerResult result)
    {
        Debug.Log($"�����̏��ʎ��ӂ̃����L���O(���[�_�[�{�[�h)�̎擾�ɐ������܂���");

        //result.Leaderboard�Ɋe���ʂ̏��(PlayerLeaderboardEntry)�������Ă���
        foreach (var entry in result.Leaderboard)
        {
            rankingText.text += "\n���Ȃ��̏���\n";
            rankingText.text += $"{entry.Position + 1}��:{entry.StatValue}  {entry.DisplayName}\n";
        }
    }

    //�����̏��ʎ��ӂ̃����L���O(���[�_�[�{�[�h)�̎擾���s
    private void OnGetLeaderboardAroundPlayerFailure(PlayFabError error)
    {
        Debug.LogError($"�����̏��ʎ��ӂ̃����L���O(���[�_�[�{�[�h)�̎擾�Ɏ��s���܂���\n{error.GenerateErrorReport()}");
    }

    *//*���[�U�[�l�[���̍X�V*//*
    public void UpdateUserName()
    {
        //���[�U�����w�肵�āAUpdateUserTitleDisplayNameRequest�̃C���X�^���X�𐶐�
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = PlayerPrefs.GetString("userName")
        };

        //���[�U���̍X�V
        Debug.Log($"���[�U���̍X�V�J�n");
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUpdateUserNameSuccess, OnUpdateUserNameFailure);
    }

    //���[�U���̍X�V����
    private void OnUpdateUserNameSuccess(UpdateUserTitleDisplayNameResult result)
    {
        //result.DisplayName�ɍX�V������̃��[�U���������Ă�
        Debug.Log($"���[�U���̍X�V���������܂��� : {result.DisplayName}");
    }

    //���[�U���̍X�V���s
    private void OnUpdateUserNameFailure(PlayFabError error)
    {
        Debug.LogError($"���[�U���̍X�V�Ɏ��s���܂���\n{error.GenerateErrorReport()}");
    }

    *//*�X�R�A�̍X�V*//*
    public void UpdatePlayerStatistics()
    {
        //UpdatePlayerStatisticsRequest�̃C���X�^���X�𐶐�
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate{
                    //�����L���O��(���v���)
                    StatisticName = PlayerPrefs.GetString("Level"),   
                    //�X�R�A(int)
                    Value = PlayerPrefs.GetInt(PlayerPrefs.GetString("Level")),
                }
            }
        };

        //���[�U���̍X�V
        Debug.Log($"�X�R�A(���v���)�̍X�V�J�n");
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdatePlayerStatisticsSuccess, OnUpdatePlayerStatisticsFailure);
    }

    //�X�R�A(���v���)�̍X�V����
    private void OnUpdatePlayerStatisticsSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log($"�X�R�A(���v���)�̍X�V���������܂���");
    }

    //�X�R�A(���v���)�̍X�V���s
    private void OnUpdatePlayerStatisticsFailure(PlayFabError error)
    {
        Debug.LogError($"�X�R�A(���v���)�X�V�Ɏ��s���܂���\n{error.GenerateErrorReport()}");
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
            Debug.Log($"�X�R�A {playerScore} ���M�����I");
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
