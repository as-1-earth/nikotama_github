using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartController : MonoBehaviour
{
    [SerializeField]
    private GameObject newStageTemp;
    public static GameObject newStage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        UserController.userStop = false;

        /*アクティブ設定の切り替え*/
        /*UserUIを非アクティブ化*/
        UIController.UserUI.SetActive(true);
        /*ResultUIをアクティブ化*/
        UIController.ResultUI.SetActive(false);

        UserController.user[0].position = new Vector3(
            LeftUserController.leftBall[1].position.x,
            LeftUserController.leftBall[1].position.y,
            UserController.user[0].position.z - (UserController.user[0].position.z % StageGenerator.stageChipSize + StageGenerator.stageChipSize)
        );
        UserController.user[1].position = new Vector3(
            RightUserController.rightBall[1].position.x,
            RightUserController.rightBall[1].position.y,
            UserController.user[1].position.z - (UserController.user[1].position.z % StageGenerator.stageChipSize + StageGenerator.stageChipSize)
        );

        StageGenerator.DestroyOldestStage(StageGenerator.generatedStageList);

        GameObject[] InstantiateStage = new GameObject[2];

        InstantiateStage[0] = Instantiate(newStage, new Vector3(0, 0, UserController.user[0].position.z), Quaternion.identity);
        InstantiateStage[1] = Instantiate(newStage, new Vector3(0, 0, UserController.user[0].position.z-StageGenerator.stageChipSize), Quaternion.identity);

        StageGenerator.generatedStageList.Insert(0, InstantiateStage[0]);
        StageGenerator.generatedStageList.Insert(0, InstantiateStage[1]);

        UserController.userSpeedZ -= 10;
    }
}
