using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    /*ドアのスピード*/
    public float speedDoor;

    /*ステージ関連*/
    /*ステージの長さ*/
    [SerializeField]
    private int StageChipSizeTemp;
    public static int stageChipSize;
        /*その時に使用しているステージの数*/
    int currentChipIndex;
        /*開始位置のStageのChipのIndex番号*/
    public int startChipIndex;
        /*生成しておくステージの枚数*/
    public int preInstantiate;
    /*現在生成されているステージを入れるリスト*/
    [SerializeField]
    private List<GameObject> generatedStageListTemp = new List<GameObject>();
    public static List<GameObject> generatedStageList = new List<GameObject>();
        /*ステージと壁の配列は対応させる*/
            /*複製するステージの配列*/
    public GameObject[] randomStageChips;
            /*ステージの壁を入れる配列*/
    public GameObject[] stageWallChips;
            /*ステージと壁を対応させたDictionaryを作成*/
    public Dictionary<GameObject, GameObject> stageWallDictionary = new Dictionary<GameObject, GameObject>();

    /*壁の場所を入れる配列*/
        /*左の壁*/
    public Transform[] leftWallPosition;
        /*右の壁*/
    public Transform[] rightWallPosition;

    /*左右の壁の枚数を操作する変数*/
    public int min;
    public int max;

    // Start is called before the first frame update
    void Start()
    {
        stageChipSize = StageChipSizeTemp;
        generatedStageList = generatedStageListTemp;

        /*0~ステージの枚数分だけ繰り返す*/
        for(int i = 0; i < randomStageChips.Length; i++)
        {
            /*ステージと壁をDirectionaryないに対応させる*/
            stageWallDictionary.Add(randomStageChips[i], stageWallChips[i]);
        }

        /*ステージを消すから消す前に一個残すやつ*/
        currentChipIndex = startChipIndex - 1;
        
        /*preInstantiateの枚数だけステージを生成*/
            /*UpdateStage：指定のIndexまでのステージチップを生成して管理下におく自作関数*/
                /*引数：生成するステージのIndex番号*/
        UpdateStage(preInstantiate);
    }

    // Update is called once per frame
    void Update()
    {
        /*Userの位置から現在のステージチップのインデックスを計算*/
        int userPositionIndex = (int)(UserController.user[0].position.z / stageChipSize);

        /*次のステージチップに入ったらステージ更新処理を行う*/
        if (userPositionIndex + preInstantiate > currentChipIndex)
        {
            /*charaPositionIndex+preInstantiateをIndex番号としてステージを生成*/
                /*UpdateStage：指定のIndexまでのステージチップを生成して管理下におく自作関数*/
                    /*引数：生成するステージのIndex番号*/
            UpdateStage(userPositionIndex + preInstantiate);

        }

        /*ドアを開く処理*/
            /*OpenDoor：ドアを開ける処理を行う自作関数*/
                /*引数：開きたいドアをもっているGameObject*/
                /*generatedStageListの３つ目のドアを開ける*/
        OpenDoor(generatedStageList[2]);
    }

    /*指定のIndexまでのステージチップを生成して管理下におく*/
    void UpdateStage(int toChipIndex)
    {
        /*指定されたIndexが実際のステージの枚数より少ない場合はそのまま返す*/
        if (toChipIndex <= currentChipIndex) return;

        /*指定のステージチップまでを生成*/
        for(int i = currentChipIndex + 1; i <= toChipIndex; i++)
        {
            /*ランダムなステージを生成*/
                /*GenerateStage：指定のインデックス位置にStageオブジェクトをランダムに生成する自作関数*/
                    /*引数：ステージを生成したい場所のIndex番号*/
            GameObject stageObject = GenerateStage(i, min, max, stageWallDictionary, randomStageChips, stageChipSize, rightWallPosition, leftWallPosition);

            /*生成したステージチップを管理リストに追加*/
            generatedStageList.Add(stageObject);
        }

        /*ステージ保持上限内になるまで古いステージを削除*/
        while (generatedStageList.Count > preInstantiate + 2) DestroyOldestStage(generatedStageList);

        /*実際のステージの枚数を変更*/
        currentChipIndex = toChipIndex;
    }

    /*指定のインデックス位置にStageオブジェクトをランダムに生成*/
    public static GameObject GenerateStage(
        int chipIndex, int min, int max, Dictionary<GameObject, GameObject> wall, GameObject[] randomStageChips, 
        int StageChipSize, Transform[] rightWallPosition, Transform[] leftWallPosition
    )
    {
        /*ステージの枚数の中からランダムで数字を生成*/
        int nextStageChip = Random.Range(0, randomStageChips.Length);

        /*ランダムで指定したステージを指定の場所に複製*/
        GameObject stageObject = Instantiate(
            /*生成するステージのプレハブ*/
            randomStageChips[nextStageChip],
            /*生成するステージの場所*/
            new Vector3(0, 0, chipIndex * StageChipSize),
            /*回転なしの設定*/
            Quaternion.identity
        );

        /*右側の壁の枚数をランダムで指定する変数*/
        int rightWallNomber = Random.Range(min, max);

        /*右の壁をプレハブとしてステージの子要素に生成する自作関数*/
        PutWall(rightWallNomber, rightWallPosition.Length, rightWallPosition, chipIndex, wall, nextStageChip, stageObject, StageChipSize, randomStageChips);
        
        /*左側の壁の枚数をランダムで指定する変数*/
        int leftWallNomber = Random.Range(min, max);

        /*左の壁をプレハブとしてステージの子要素に生成する自作関数*/
        PutWall(leftWallNomber, leftWallPosition.Length, leftWallPosition, chipIndex, wall, nextStageChip, stageObject, StageChipSize, randomStageChips);

        /*生成するステージを返す*/
        return stageObject;
    }

    /*一番古いステージを削除*/
    public static void DestroyOldestStage(List<GameObject> generatedStageList)
    {
        /*一番古いステージを取得*/
        GameObject oldStage = generatedStageList[0];
        
        /*リストから一番古いステージを削除*/
        generatedStageList.RemoveAt(0);
        
        /*画面から一番古いステージを削除*/
        Destroy(oldStage);
    }

    /*ドアを開ける処理を行う自作関数*/
        /*引数：開くドア開きたいドアをもっているGameObject*/
    void OpenDoor(GameObject nextStage)
    {
        /*ステージのプレハブの”最初”の子要素の”最初の”子要素をステージの”次の”子要素の”最初の”子要素の位置に線形補間で移動する*/
        /*Right側のドアの移動*/
        nextStage.transform.GetChild(0).GetChild(0).position = Vector3.Lerp(
            nextStage.transform.GetChild(0).GetChild(0).position,
            nextStage.transform.GetChild(1).GetChild(0).position,
            Time.deltaTime * speedDoor
        );

        /*ステージのプレハブの”最初”の子要素の”次の”子要素をステージの”次の”子要素の”次の”子要素の位置に線形補間で移動する*/
        /*Left側のドアの移動*/
        nextStage.transform.GetChild(0).GetChild(1).position = Vector3.Lerp(
            nextStage.transform.GetChild(0).GetChild(1).position,
            nextStage.transform.GetChild(1).GetChild(1).position,
            Time.deltaTime * speedDoor
        );
    }

    /*壁のプレハブをステージの子要素に生成する自作関数*/
    public static void PutWall(
        int wallNomber, int wallPositionLength, Transform[] wallPosition, int chipIndex, Dictionary<GameObject, GameObject> wall, int nextStageChip, 
        GameObject stageObject, int StageChipSize, GameObject[] randomStageChips
    )
    {
        for(int i = 0; i < wallNomber; i++)
        {
            /*壁の場所の中からどこに置くかをランダムで生成*/
            int wallPositionIndex = Random.Range(0, wallPositionLength);

            /*Z軸の座標をステージのサイズ×(何枚目か＋１)/(壁の枚数＋１)で指定*/
            float wallPositionZ = StageChipSize * (i + 1) / (wallNomber + 1);

            /*wallObjectのインスタンス化*/
            GameObject[] wallObject = new GameObject[wallNomber];

            /*i番目の壁をプレハブとして複製*/
            wallObject[i] = Instantiate(
                /*複製するもの*/
                /*現在生成されているステージに対応する壁をDirectionaryから*/
                wall[randomStageChips[nextStageChip]],
                /*複製する場所
                /*新しいVector3をインスタンス化*/
                new Vector3(
                    /*壁のプレハブからランダムで指定した場所のX座標*/
                    wallPosition[wallPositionIndex].position.x,
                    wallPosition[wallPositionIndex].position.y,
                    chipIndex * StageChipSize + wallPositionZ
                ),
                /*回転*/
                    /*壁のプレハブからランダムで指定した場所の回転*/
                wallPosition[wallPositionIndex].rotation
            );

            /*壁のオブジェクトをステージの子要素にする*/
            wallObject[i].transform.parent = stageObject.transform;
        }
    }
}
