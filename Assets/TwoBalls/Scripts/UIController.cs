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
    /*ゲーム中のUserが使うUI*/
    [SerializeField]
    private GameObject UserUITemp;
    public static GameObject UserUI;

    /*結果を表示するときのUI*/
    [SerializeField]
    private GameObject ResultUITemp;
    public static GameObject ResultUI;

    /*結果を表示するときのUI*/
    [SerializeField]
    private GameObject PauseUITemp;
    public static GameObject PauseUI;
    
    /*Userを置く配列*/
    public GameObject[] User;

    /*Textの変数*/
        /*ゲーム中表示されるScoreのText*/
    public Text scoreText;
        /*結果画面のUI*/
    public Text resultScoreText;
        /*ベストスコアのText*/
    public Text bestScoreText;
        /*speedを表示するText*/
    public Text speedUI;
        /*その時のスピードを表示するText*/
    public Text SpeedZUI;
        /*プレイ画面にスピードを表示するUI*/
    public Text playSpeedZUI;
        /*rankingを表示するText*/
    [SerializeField]
    private Text rankingTemp;
    public static Text ranking;
        /*countDownを表示するText*/
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

        /*エラーを消すための処理*/
        NativeLeakDetection.Mode = NativeLeakDetectionMode.EnabledWithStackTrace;

        /*最初のUIのアクティブ設定*/
            /*UserUIをアクティブ化*/
        UserUI.SetActive(true);
            /*PauseCountTextを非アクティブ化*/
        PauseCountText.SetActive(false);
            /*ResultUIを非アクティブ化*/
        ResultUI.SetActive(false);

        PauseUI.SetActive(false);
        
        /*userStopをfalseにして動けるようにする*/
        UserController.userStop = false;

        ranking = rankingTemp;

        Debug.Log(PlayerPrefs.GetString("Level"));

    }

    // Update is called once per frame
    void Update()
    {
        /*デバッグ用*/
            /*もしEnterキーを入力されたらリスタート*/
        if (Input.GetKeyDown(KeyCode.Return)) OnRestartButtonClicked();
        
        /*現在のスコアを画面上に表示*/
        scoreText.text = "Score:" + CalcScore() + "m";

        playSpeedZUI.text = UserController.userSpeedZ.ToString("000");

        /*もしuserStopがtrue(Userが障害物に当たった)ならば*/
        if (UserController.userStop)
        {
            /*アクティブ設定の切り替え*/
                /*UserUIを非アクティブ化*/
            UserUI.SetActive(false);
                /*ResultUIをアクティブ化*/
            ResultUI.SetActive(true);

            if (PauseUI.activeSelf)
            {
                /*UserUIを非アクティブ化*/
                UserUI.SetActive(false);
                /*ResultUIをアクティブ化*/
                ResultUI.SetActive(false);
            }

            if (PauseCountText.activeSelf)
            {
                /*UserUIをアクティブ化*/
                UserUI.SetActive(true);
                /*ResultUIを非アクティブ化*/
                ResultUI.SetActive(false);
                /*PauseUIを非アクティブ化*/
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

            /*speedUIのtextに記憶させたSpeedZに書く*/
            speedUI.text = PlayerPrefs.GetInt("SpeedZ").ToString("000");

            /*結果のスコアをTextに表示*/
            resultScoreText.text = CalcScore() + "m";

            /*その時のスピードをTextに表示*/
            SpeedZUI.text = $"最終的なスピード\n{UserController.userSpeedZ}";

            /*BestScoreの設定*/
            /*もし結果がHighScoreより高ければ*/


            HighScore(PlayerPrefs.GetString("Level"));

            /*ベストスコアをTextに表示*/
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

        // マウスの右クリックでツイート画面を開く場合


    }

    /*ホームボタンをクリックされた時の処理*/
    public void OnHomeButtonClicked()
    {
        /*アクティブ設定の切り替え*/
            /*UserUIをアクティブ化*/
        UserUI.SetActive(true);


            /*ResultUIを非アクティブ化*/
        ResultUI.SetActive(false);

        /*Titleシーンに切り替え*/
        SceneManager.LoadScene("Title");
    }

    /*再生ボタン(リスタート)をクリックされたとき*/
    public void OnRestartButtonClicked()
    {
        /*Mainシーン(同じシーン)に切り替え*/
        TitleUIController.ChangeToMain();
        
    }

    /*点数の計算*/
    public static int CalcScore()
    {
        /*User(Left)のZ軸を返す*/
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
