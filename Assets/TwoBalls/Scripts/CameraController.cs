using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /*�J�����̋������擾*/
    public int cameraDistans;

    // Update is called once per frame
    void Update()
    {
        /*�J�����̈ʒu���w��(*/
        transform.position = CameraPosition(
            /*�J�������g�̈ʒu, �Ώۂ�User�̈ʒu, �J������User�̋���*/
            transform, UserController.user[0], cameraDistans
        );
    }

    /*�J������Position���֐��Ōv�Z*/
    public static Vector3 CameraPosition(
        /*�����F�J�������g��Transform, �B�e����target��Transform, �J������target�̋���*/
        Transform camera, Transform targetBall, int cameraDistans
    )
    {
        /*camera��Position��������*/
        Vector3 cameraPosition = Vector3.zero;

        /*camera��Z����target��Z����������̋��������������̂���*/
        cameraPosition.z = targetBall.position.z - cameraDistans;

        /*camera��Position��Ԃ�*/
        return cameraPosition;
    }
}
