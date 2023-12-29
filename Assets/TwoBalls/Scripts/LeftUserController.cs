using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftUserController : MonoBehaviour
{
    /*�E���̃{�[�������ׂĔz��Ŏ擾*/
    [SerializeField]
    private Transform[] leftBallTemp;
    public static Transform[] leftBall;

    /*���[���֘A�̒l*/
        /*���[���̈ړ��X�s�[�h*/
    public float speedLane;

    // Start is called before the first frame update
    void Start()
    {
        leftBall = leftBallTemp;
    }

    // Update is called once per frame
    void Update()
    {
        /*��ʂ��^�b�`���ꂽ���̏���*/
        /*0�`��ʂ��^�b�`����Ă�w�̖{�����J��Ԃ�*/
        for (int i = 0; i < Input.touchCount; i++)
        {
            /*������ʂ̉E�����^�b�`����Ă���Ȃ�*/
            if (Input.touches[i].position.x < Screen.width / 2)
            {
                /*User��Position�������UserTouchPosition�֐���p���Čv�Z*/
                /*UserTouchPosition�F�^�b�`���ꂽ�ꏊ����User��Position���v�Z����֐�*/
                /*�����F�^�b�`����Ă���w��Vector3, �ړ��̑���, ���ׂẴ{�[���������Ă���z���Transform, User��Transform*/
                transform.position = UserController.UserTouchPosition(Input.touches[i].position, leftBall, transform);
            }
        }

        /*����User���ǂɏՓ˂��Ă��Ȃ����*/
        if (!UserController.userStop)
        {
            /*�������͂��ꂽ��*/
            if (Input.GetKey(KeyCode.W))
            {
                /*userPosition��user��Position�����Đ���*/
                Vector3 userPosition = transform.position;

                /*target�Ƃ��Č��݂̈ʒu�ɃX�s�[�h�𑫂����킹���l��ϐ��Ƃ��Đ���*/
                float target = transform.position.y + speedLane;

                /*user��Y����Position����Ɖ��̏����ݒ�*/
                userPosition.y = Mathf.Clamp(target, leftBall[0].position.y, leftBall[2].position.y);

                /*x=a*y^2+q�̓񎟊֐��Ƃ���a��q���ȉ��̒萔*/
                const float a = 1.85f / 10.24f;
                const float q = 3.7f;

                /*user��X����Position���v�Z*/
                userPosition.x = a * Mathf.Pow(userPosition.y, 2) - q;

                /*�v�Z�������W��transform��Position�ɓ����*/
                transform.position = new Vector3(userPosition.x, userPosition.y, userPosition.z);
            }

            /*��������͂��ꂽ��*/
            if (Input.GetKey(KeyCode.S))
            {
                /*userPosition��user��Position�����Đ���*/
                Vector3 userPosition = transform.position;

                /*target�Ƃ��Č��݂̈ʒu�ɃX�s�[�h�𑫂����킹���l��ϐ��Ƃ��Đ���*/
                float target = transform.position.y - speedLane;

                /*user��Y����Position����Ɖ��̏����ݒ�*/
                userPosition.y = Mathf.Clamp(target, leftBall[0].position.y, leftBall[2].position.y);

                /*x=a*y^2+q�̓񎟊֐��Ƃ���a��q���ȉ��̒萔*/
                const float a = 1.85f / 10.24f;
                const float q = 3.7f;

                /*user��X����Position���v�Z*/
                userPosition.x = a * Mathf.Pow(userPosition.y, 2) - q;

                /*�v�Z�������W��transform��Position�ɓ����*/
                transform.position = new Vector3(userPosition.x, userPosition.y, userPosition.z);
            }
        }
    
        /*����User���ǂɓ������Ă��Ȃ����Z����i�߂�*/
        if (!UserController.userStop) transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z + ( Time.deltaTime * UserController.userSpeedZ)
        );
    }

    /*User��Wall�ɓ����������̏���*/
    private void OnTriggerEnter(Collider wall)
    {
        /*����Wall�^�O��Trigger�ɓ���������~�܂�*/
            /*userStop�Fuser���~�܂�����true�ɂȂ�bool�^�ϐ�*/
        if (wall.CompareTag("Wall")) UserController.userStop = true;
    }
}
