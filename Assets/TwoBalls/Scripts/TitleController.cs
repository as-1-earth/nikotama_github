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

public class TitleController : MonoBehaviour
{
    /*User�̖��O����͂���e�L�X�g*/
    [SerializeField]
    private Text userNameTemp;
    public static Text userName;

    // Start is called before the first frame update
    void Start()
    {
        /*User�̖��O��Inspecter��ł������悤�ɂ���*/
        userName = userNameTemp;

        /*�Ȃ񂩃G���[���������*/
        NativeLeakDetection.Mode = NativeLeakDetectionMode.EnabledWithStackTrace;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (textName != userName)
        {

            PlayFabClientAPI.LoginWithCustomID(
            new LoginWithCustomIDRequest
            {
                TitleId = PlayFabSettings.TitleId,
                CustomId = TitleController.userName.text,
                CreateAccount = true,
            }
            , result =>
            {
                Debug.Log("���O�C�������I");

            *//*foreach(string name in LeaderboardName)*//*

                UpdateUserName();
            }
            , errorCallback =>
            {
                Debug.Log("���O�C�����s�I");
            });

            PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
            {
                StatisticName = "HighScore"
            }, result =>
            {
                foreach (var item in result.Leaderboard)
                {
                    item.DisplayName = TitleController.userName.text;
                }
            }, error =>
            {
                Debug.Log(error.GenerateErrorReport());
            });

            textName = userName;
        }*/
    }

    /*�Đ��{�^�����N���b�N���ꂽ���̏���*/
    public void OnPlayButtonClicked()
    {
        /*Main�V�[��(�Q�[�����)�ւ̐؂�ւ�*/
        TitleUIController.ChangeToMain();
    }
/*
    public void UpdateUserName()
    {
        //���[�U�����w�肵�āAUpdateUserTitleDisplayNameRequest�̃C���X�^���X�𐶐�
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = userName.text
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
    }*/
}
