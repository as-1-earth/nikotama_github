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

public class UIController : MonoBehaviour
{
    /*�Q�[������User���g��UI*/
    [SerializeField]
    private GameObject UserUITemp;
    public static GameObject UserUI;

    /*���ʂ�\������Ƃ���UI*/
    [SerializeField]
    private GameObject ResultUITemp;
    public static GameObject ResultUI;

    /*���ʂ�\������Ƃ���UI*/
    [SerializeField]
    private GameObject PauseUITemp;
    public static GameObject PauseUI;
    
    /*User��u���z��*/
    public GameObject[] User;

    /*Text�̕ϐ�*/
        /*�Q�[�����\�������Score��Text*/
    public Text scoreText;

    public Text LevelText;
        /*���ʉ�ʂ�UI*/
    public Text resultScoreText;
        /*�x�X�g�X�R�A��Text*/
    public Text bestScoreText;
        /*speed��\������Text*/
    public Text speedUI;
        /*���̎��̃X�s�[�h��\������Text*/
    public Text SpeedZUI;
        /*�v���C��ʂɃX�s�[�h��\������UI*/
    public Text playSpeedZUI;
        /*ranking��\������Text*/
    [SerializeField]
    private Text rankingTemp;
    public static Text ranking;
        /*countDown��\������Text*/
    [SerializeField]
    private GameObject PauseCountTextTemp;
    public static GameObject PauseCountText;


    double countDown = 4.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        UserUI = UserUITemp;

        ResultUI = ResultUITemp;

        PauseUI = PauseUITemp;

        PauseCountText = PauseCountTextTemp;

        /*�G���[���������߂̏���*/
        NativeLeakDetection.Mode = NativeLeakDetectionMode.EnabledWithStackTrace;

        /*�ŏ���UI�̃A�N�e�B�u�ݒ�*/
            /*UserUI���A�N�e�B�u��*/
        UserUI.SetActive(true);
            /*PauseCountText���A�N�e�B�u��*/
        PauseCountText.SetActive(false);
            /*ResultUI���A�N�e�B�u��*/
        ResultUI.SetActive(false);

        PauseUI.SetActive(false);
        
        /*userStop��false�ɂ��ē�����悤�ɂ���*/
        UserController.userStop = false;

        ranking = rankingTemp;

        Debug.Log(PlayerPrefs.GetString("Level"));

    }

    // Update is called once per frame
    void Update()
    {
        /*�f�o�b�O�p*/
            /*����Enter�L�[����͂��ꂽ�烊�X�^�[�g*/
        if (Input.GetKeyDown(KeyCode.Return)) OnRestartButtonClicked();
        
        /*���݂̃X�R�A����ʏ�ɕ\��*/
        scoreText.text = "Score:" + CalcScore() + "m";

        /*playSpeedZUI.text = UserController.userSpeedZ.ToString("000");*/

        /*����userStop��true(User����Q���ɓ�������)�Ȃ��*/
        if (UserController.userStop)
        {
            /*�A�N�e�B�u�ݒ�̐؂�ւ�*/
                /*UserUI���A�N�e�B�u��*/
            UserUI.SetActive(false);
                /*ResultUI���A�N�e�B�u��*/
            ResultUI.SetActive(true);

            if (PauseUI.activeSelf)
            {
                /*UserUI���A�N�e�B�u��*/
                UserUI.SetActive(false);
                /*ResultUI���A�N�e�B�u��*/
                ResultUI.SetActive(false);
            }

            if (PauseCountText.activeSelf)
            {
                /*UserUI���A�N�e�B�u��*/
                UserUI.SetActive(true);
                /*ResultUI���A�N�e�B�u��*/
                ResultUI.SetActive(false);
                /*PauseUI���A�N�e�B�u��*/
                PauseUI.SetActive(false);

                countDown -= Time.deltaTime;

                int countDownText = (int)countDown;

                PauseCountText.gameObject.GetComponent<Text>().text = countDownText.ToString();

                if(countDownText == 0)
                {
                    UserController.userStop = false;
                    countDown = 4.0;
                    PauseCountText.SetActive(false);
                }
            }

            /*speedUI��text�ɋL��������SpeedZ�ɏ���*/
            /*speedUI.text = PlayerPrefs.GetInt("SpeedZ").ToString("000");*/

            LevelText.text = PlayerPrefs.GetString("Level");
            
            /*���ʂ̃X�R�A��Text�ɕ\��*/
            resultScoreText.text = CalcScore() + "m";
            /*���̎��̃X�s�[�h��Text�ɕ\��*/
            /*SpeedZUI.text = $"�ŏI�I�ȃX�s�[�h\n{UserController.userSpeedZ}";*/

            /*BestScore�̐ݒ�*/
            /*�������ʂ�HighScore��荂�����*/


            HighScore(PlayerPrefs.GetString("Level"));

            /*�x�X�g�X�R�A��Text�ɕ\��*/
            bestScoreText.text = "Best:" + PlayerPrefs.GetInt(PlayerPrefs.GetString("Level")) + "m";

            /*if (playFabBool)
            {
                PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
                {
                    CustomId = TitleController.userName.text,
                    CreateAccount = true
                }
                , result =>
                {
                    Debug.Log("Success LOGIN");
                    UpdateUserName();
                    UpdatePlayerStatistics();
                    GetLeaderboard();
                    GetLeaderboardAroundPlayer();
                    ranking.text = $"Score:  {CalcScore()}\n";
                }
                , error =>
                {
                    Debug.Log(error.GenerateErrorReport());
                });

                playFabBool = false;
            }*/

        }

        // �}�E�X�̉E�N���b�N�Ńc�C�[�g��ʂ��J���ꍇ


    }

    /*�z�[���{�^�����N���b�N���ꂽ���̏���*/
    public void OnHomeButtonClicked()
    {
        /*�A�N�e�B�u�ݒ�̐؂�ւ�*/
            /*UserUI���A�N�e�B�u��*/
        UserUI.SetActive(true);


            /*ResultUI���A�N�e�B�u��*/
        ResultUI.SetActive(false);

        /*Title�V�[���ɐ؂�ւ�*/
        SceneManager.LoadScene("Title");
    }

    /*�Đ��{�^��(���X�^�[�g)���N���b�N���ꂽ�Ƃ�*/
    public void OnRestartButtonClicked()
    {
        /*Main�V�[��(�����V�[��)�ɐ؂�ւ�*/
        TitleUIController.ChangeToMain();
        
    }

    /*�_���̌v�Z*/
    public static int CalcScore()
    {
        /*User(Left)��Z����Ԃ�*/
        return (int)UserController.user[0].position.z;
    }

    void HighScore(String level)
    {
        if (PlayerPrefs.GetInt(level) < CalcScore())
        {
            PlayerPrefs.SetInt(level, CalcScore());
        }
    }

}
