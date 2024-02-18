using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;/*
using PlayFab;
using PlayFab.ClientModels;*/


public class UserController : MonoBehaviour
{
    /*User��Transform���擾*/
        /*0��Left,1��Right*/
    [SerializeField]
    private Transform[] userTemp;
    public static Transform[] user;

    /*User���~�܂�������true�ɂȂ�bool�^�ϐ�*/
    public static bool userStop = false;

    /*User�̃X�s�[�h��Inspecter��ő��삷��*/
    [SerializeField]
    private int userSpeedZTemp;
    public static int userSpeedZ;

    bool speedUpCheck = true;

/*    [SerializeField] GetPlayerCombinedInfoRequestParams InfoRequestParams;*/
    // Start is called before the first frame update
    void Start()
    {
        /*user��Inspecter��ő���ł���悤�ɂ��邽�߂̑���*/
        user = userTemp;

        /*userSpeedZ�̒l��SpeedZ�ɋL��������*/
        userSpeedZ = PlayerPrefs.GetInt("SpeedZ");

        /* PlayerPrefs.SetInt("SpeedZ", 15);*/

/*
        
        PlayFabAuthService.Instance.InfoRequestParams = InfoRequestParams; // ������ǉ�!!
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

    /*User��Position���֐��Ōv�Z*/
        /*�����F��ʂ��^�b�`����Ă���ꏊ, Z�����ړ�����X�s�[�h, �{�[����Transform, ���[�U�[��Transform*/
    public static Vector3 UserTouchPosition(Vector3 touchPosition, Transform[] Ball, Transform user)
    {
        /*����User���~�܂�����user��position�����̏�ɂƂǂ߂鏈��*/
        if (userStop) return user.position;

        /*���[�U�[��position�����E�̃{�[��1��x,y���W�ƌ��݂�User��Z���W�Ɉړ�*/
        user.position = new Vector3(Ball[1].position.x, Ball[1].position.y, user.position.z);

        /*UserPosition��User�̈ʒu�ɏ�����*/
        Vector3 userPosition = user.position;

        /*x=a*y^2+q�̓񎟊֐��Ƃ���a��q���ȉ��̒萔*/
        const float a = 1.85f / 10.24f;
        const float q = 3.7f;

        /*touchPosition��Z���W�͏��������Ȃ��ᔽ�f����Ȃ��H*/
        touchPosition.z = 10;

        /*target�̓X�N���[����ʂ��^�b�`���ꂽ�ꏊ��world���W�ɕϊ���������*/
        Vector3 target = Camera.main.ScreenToWorldPoint(touchPosition);

        /*userPosition��y���W���㉺�̃{�[���ȏ�ɓ������Ȃ��悤�Ɏw��*/
        userPosition.y = Mathf.Clamp(target.y, Ball[0].position.y, Ball[2].position.y);

        /*�����^�b�`���ꂽ�ꏊ����ʏ�̍����Ȃ�*/
            /*userPosition��X���W��x = a * y ^ 2 - q��p���Čv�Z*/
        if (touchPosition.x < Screen.width / 2) userPosition.x = a * Mathf.Pow(userPosition.y, 2) - q;

        /*�����^�b�`���ꂽ�ꏊ����ʏ�̉E���Ȃ�*/
            /*userPosition��X���W��x = a * y ^ 2 + q��p���Čv�Z*/
        if (touchPosition.x > Screen.width / 2) userPosition.x = -a * Mathf.Pow(userPosition.y, 2) + q;

        /*���Ԍo�߂ɂ���Ďw�肳�ꂽ�X�s�[�h��Z����i��*/
        userPosition.z = user.position.z;

        /*UserPosition��Ԃ�*/
        return userPosition;
    }
    
    /*userSpeedZ��1�v���X���ċL�������鎩��֐�*/
    public void OnPlaceButton()
    {
        userSpeedZ++;
        PlayerPrefs.SetInt("SpeedZ", userSpeedZ);
    }

    /*userSpeedZ��1�}�C�i�X���ċL�������鎩��֐�*/
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
 