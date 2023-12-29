using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftUserController : MonoBehaviour
{
    /*右側のボールをすべて配列で取得*/
    [SerializeField]
    private Transform[] leftBallTemp;
    public static Transform[] leftBall;

    /*レーン関連の値*/
        /*レーンの移動スピード*/
    public float speedLane;

    // Start is called before the first frame update
    void Start()
    {
        leftBall = leftBallTemp;
    }

    // Update is called once per frame
    void Update()
    {
        /*画面をタッチされた時の処理*/
        /*0～画面をタッチされてる指の本数分繰り返す*/
        for (int i = 0; i < Input.touchCount; i++)
        {
            /*もし画面の右側をタッチされているなら*/
            if (Input.touches[i].position.x < Screen.width / 2)
            {
                /*UserのPositionを自作のUserTouchPosition関数を用いて計算*/
                /*UserTouchPosition：タッチされた場所からUserのPositionを計算する関数*/
                /*引数：タッチされている指のVector3, 移動の速さ, すべてのボールが入っている配列のTransform, UserのTransform*/
                transform.position = UserController.UserTouchPosition(Input.touches[i].position, leftBall, transform);
            }
        }

        /*もしUserが壁に衝突していなければ*/
        if (!UserController.userStop)
        {
            /*上矢印を入力された時*/
            if (Input.GetKey(KeyCode.W))
            {
                /*userPositionをuserのPositionを入れて生成*/
                Vector3 userPosition = transform.position;

                /*targetとして現在の位置にスピードを足し合わせた値を変数として生成*/
                float target = transform.position.y + speedLane;

                /*userのY軸のPositionを上と下の上限を設定*/
                userPosition.y = Mathf.Clamp(target, leftBall[0].position.y, leftBall[2].position.y);

                /*x=a*y^2+qの二次関数としてaとqが以下の定数*/
                const float a = 1.85f / 10.24f;
                const float q = 3.7f;

                /*userのX軸のPositionを計算*/
                userPosition.x = a * Mathf.Pow(userPosition.y, 2) - q;

                /*計算した座標をtransformのPositionに入れる*/
                transform.position = new Vector3(userPosition.x, userPosition.y, userPosition.z);
            }

            /*下矢印を入力された時*/
            if (Input.GetKey(KeyCode.S))
            {
                /*userPositionをuserのPositionを入れて生成*/
                Vector3 userPosition = transform.position;

                /*targetとして現在の位置にスピードを足し合わせた値を変数として生成*/
                float target = transform.position.y - speedLane;

                /*userのY軸のPositionを上と下の上限を設定*/
                userPosition.y = Mathf.Clamp(target, leftBall[0].position.y, leftBall[2].position.y);

                /*x=a*y^2+qの二次関数としてaとqが以下の定数*/
                const float a = 1.85f / 10.24f;
                const float q = 3.7f;

                /*userのX軸のPositionを計算*/
                userPosition.x = a * Mathf.Pow(userPosition.y, 2) - q;

                /*計算した座標をtransformのPositionに入れる*/
                transform.position = new Vector3(userPosition.x, userPosition.y, userPosition.z);
            }
        }
    
        /*もしUserが壁に当たっていなければZ軸を進める*/
        if (!UserController.userStop) transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z + ( Time.deltaTime * UserController.userSpeedZ)
        );
    }

    /*UserがWallに当たった時の処理*/
    private void OnTriggerEnter(Collider wall)
    {
        /*もしWallタグのTriggerに当たったら止まる*/
            /*userStop：userが止まった時trueになるbool型変数*/
        if (wall.CompareTag("Wall")) UserController.userStop = true;
    }
}
