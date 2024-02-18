using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;/*
using PlayFab;
using PlayFab.ClientModels;*/


public class UserController : MonoBehaviour
{
    /*UserのTransformを取得*/
        /*0がLeft,1がRight*/
    [SerializeField]
    private Transform[] userTemp;
    public static Transform[] user;

    /*Userが止まった時にtrueになるbool型変数*/
    public static bool userStop = false;

    /*UserのスピードをInspecter上で操作する*/
    [SerializeField]
    private int userSpeedZTemp;
    public static int userSpeedZ;

    bool speedUpCheck = true;

/*    [SerializeField] GetPlayerCombinedInfoRequestParams InfoRequestParams;*/
    // Start is called before the first frame update
    void Start()
    {
        /*userをInspecter上で操作できるようにするための操作*/
        user = userTemp;

        /*userSpeedZの値をSpeedZに記憶させる*/
        userSpeedZ = PlayerPrefs.GetInt("SpeedZ");

        /* PlayerPrefs.SetInt("SpeedZ", 15);*/

/*
        
        PlayFabAuthService.Instance.InfoRequestParams = InfoRequestParams; // ここを追加!!
        PlayFabAuthService.OnLoginSuccess += PlayFabLogin_OnLoginSuccess;
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Mathf.CeilToInt(user[0].position.z) % (StageGenerator.stageChipSize * (Mathf.CeilToInt(userSpeedZ / 10))) >= (StageGenerator.stageChipSize * ((Mathf.CeilToInt(userSpeedZ / 10))) - 10) && userSpeedZ<30)
        {
            if (speedUpCheck)
            {
                
                userSpeedZ+=2;
                speedUpCheck = false;
                Debug.Log("userSpeedZ:" + userSpeedZ);

            }
        }*/
        if (Mathf.CeilToInt(user[0].position.z) % (StageGenerator.stageChipSize * (Mathf.CeilToInt(userSpeedZ / 10))) >= (StageGenerator.stageChipSize * ((Mathf.CeilToInt(userSpeedZ / 10))) - 10)/* && userSpeedZ >= 30*/)
        {
            if (speedUpCheck)
            if (speedUpCheck)
            {
                userSpeedZ++;
                speedUpCheck = false;
                Debug.Log("userSpeedZ:" + userSpeedZ);
            }
        }
        if (Mathf.CeilToInt(user[0].position.z) % StageGenerator.stageChipSize <= 10)
        {
            if (!speedUpCheck) speedUpCheck = true;
        }

        if (userStop)
        {
/*            RankingController.SubmitScore(1001, PlayerPrefs.GetString("Level"));*/
        }

    }

    /*UserのPositionを関数で計算*/
        /*引数：画面をタッチされている場所, Z軸を移動するスピード, ボールのTransform, ユーザーのTransform*/
    public static Vector3 UserTouchPosition(Vector3 touchPosition, Transform[] Ball, Transform user)
    {
        /*もしUserが止まった時userのpositionをその場にとどめる処理*/
        if (userStop) return user.position;

        /*ユーザーのpositionを左右のボール1のx,y座標と現在のUserのZ座標に移動*/
        user.position = new Vector3(Ball[1].position.x, Ball[1].position.y, user.position.z);

        /*UserPositionをUserの位置に初期化*/
        Vector3 userPosition = user.position;

        /*x=a*y^2+qの二次関数としてaとqが以下の定数*/
        const float a = 1.85f / 10.24f;
        const float q = 3.7f;

        /*touchPositionのZ座標は少し離さなきゃ反映されない？*/
        touchPosition.z = 10;

        /*targetはスクリーン画面をタッチされた場所をworld座標に変換したもの*/
        Vector3 target = Camera.main.ScreenToWorldPoint(touchPosition);

        /*userPositionのy座標を上下のボール以上に動かせないように指定*/
        userPosition.y = Mathf.Clamp(target.y, Ball[0].position.y, Ball[2].position.y);

        /*もしタッチされた場所が画面上の左側なら*/
            /*userPositionのX座標をx = a * y ^ 2 - qを用いて計算*/
        if (touchPosition.x < Screen.width / 2) userPosition.x = a * Mathf.Pow(userPosition.y, 2) - q;

        /*もしタッチされた場所が画面上の右側なら*/
            /*userPositionのX座標をx = a * y ^ 2 + qを用いて計算*/
        if (touchPosition.x > Screen.width / 2) userPosition.x = -a * Mathf.Pow(userPosition.y, 2) + q;

        /*時間経過によって指定されたスピードでZ軸を進む*/
        userPosition.z = user.position.z;

        /*UserPositionを返す*/
        return userPosition;
    }
    
    /*userSpeedZを1プラスして記憶させる自作関数*/
    public void OnPlaceButton()
    {
        userSpeedZ++;
        PlayerPrefs.SetInt("SpeedZ", userSpeedZ);
    }

    /*userSpeedZを1マイナスして記憶させる自作関数*/
    public void OnMinusButton()
    {
        userSpeedZ--;
        PlayerPrefs.SetInt("SpeedZ", userSpeedZ);
    }

    public static void OnPauseButton()
    {
        userStop = true;
        UIController.PauseUI.SetActive(true);
    }

    public static void OnPauseStartButton()
    {
        UIController.PauseCountText.SetActive(true);

        Text countDownText = UIController.PauseCountText.gameObject.GetComponent<Text>();


    }

    /*void OnEnable()
    {
        PlayFabAuthService.OnLoginSuccess += PlayFabLogin_OnLoginSuccess;
    }
    private void PlayFabLogin_OnLoginSuccess(LoginResult result)
    {
        *//*        Debug.Log(result.InfoResultPayload.UserData["awawawa"].Value);*//*
        Debug.Log("aaa");
    }
    private void OnDisable()
    {
        PlayFabAuthService.OnLoginSuccess -= PlayFabLogin_OnLoginSuccess;
    }*/
}
 