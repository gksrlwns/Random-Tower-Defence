using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerData
{
    int stage;
    int money;
    int restLife;   
}

public class TowerData
{
    Vector3 towerPosition;
    int towerType;
}

public class GameManager : MonoBehaviour
{
    [Header("게임 영향")]
    public int restLife = 20;
    public int money = 500;
    public int maxMonsterNum = 50;
    [Header("시간")]
    public float spawnTimer;
    public float StageTimer;
    public float restTimer;
    [Header("몬스터")]
    public int monsterNum = 0;
    public int clearMonsterNum;
    public Transform enemyTr;
    public GameObject[] enemyPrefabs;
    //public GameObject enemyPrefab1;
    //public GameObject enemyPrefab2;
    //public GameObject enemyPrefab3;
    [Header("타워빌드")]
    public Transform towerTr;
    private Node[] nodes;
    public GameObject Tower2Clone;

    public GameObject[] Tower2Prefab;
    //public GameObject Tower2Prefab1;
    //public GameObject Tower2Prefab2;
    //public List<Transform> TowerTr1;
    //public List<Transform> TowerTr2;
    public List<Transform>[] TowerTr;
    //public int node_TC1;
    //public int node_TC2;
    [Header("나머지")]
    public bool isCreate;
    public bool isDelete;
    public bool isStageClear;
    public bool isGameEnd;
    public bool isPannelMove;
    public bool isPause;
    public Button mainBtn;
    public Button puaseBtn;
    private GameObject enemy;
    private CameraController _cameraController;   
    public GameObject Startobj;
    public GameObject Endobj;
    public GameObject PanelObj;
    public GameObject clearPanelObj;
    public GameObject gameOverPanelObj;
    public GameObject pausePanel;
    private Animation anim;
    public Vector3 EnemyPosition;
    public Vector3 EnemyRotation;
    public Animation _ani;
    public Text restTimeText;
    public Text stageTimeText;
    public Text moneyText;
    public Text restLifeText;
    public Text gameOverText;
    public Text gameEndText;
    public Sprite pauseImage;
    public Sprite resumeImage;
    public Sprite restImgae;
    public PlayerPrefsVector3 _playerPrefsVector3;
    
    
    //public Tower.TowerKind towerKind;

    public enum StageEnum
    {
        stage1 = 0,
        stage2,
        stage3,
        clear,
    }

    public StageEnum _stageEnum;
    

    private void Awake()
    {
        nodes = FindObjectsOfType<Node>();
        //_cameraController = FindObjectOfType<CameraController>();
        anim = PanelObj.GetComponent<Animation>();

    }
    void Start()
    {
        //if (PlayerPrefs.HasKey("stage"))
        //{
        //    PlayerPrefsGetStage();
        //}
        //else
        //{
        //    _stageEnum = StageEnum.stage1;
        //}
        _playerPrefsVector3 = new PlayerPrefsVector3();
        TowerTr = new List<Transform>[(int)Tower.TowerType.kindnum];
        for (int i = 0; i < TowerTr.Length; i++ )
        {
            TowerTr[i] = new List<Transform>();
        }
        isPannelMove = false;
        //node_TC1 = 0;
        //node_TC2 = 0;
        monsterNum = 0;
        //maxMonsterNum = 50;
        //money = 500;
        StageTimer = 0;
        restTimer = 30f;
        isCreate = false;
        isStageClear = true;
        isGameEnd = false;
        isPause = false;
        _stageEnum = StageEnum.stage1;
        print($"{(int)_stageEnum}");
        //_cameraController.enabled = true;
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameEnd == true) return;
        //print($"{(int)_stageEnum}");
        if(isPause == true)
        {
            this.enabled = false;
            return;
        }
        
        if (isStageClear == false)
        {
            StageTimer += Time.deltaTime;
            //stageTimeText.text = StageTimer.ToString("00");
            //for문 없이
            if (_stageEnum < StageEnum.clear)
            {
                SpawnEnemy(enemyPrefabs[(int)_stageEnum]);
                if (clearMonsterNum >= maxMonsterNum)
                {
                    isStageClear = true;
                    //_stageEnum = StageEnum.stage2;
                    _stageEnum++;
                    StageTimer = 0;
                    clearMonsterNum = 0;
                    monsterNum = 0;
                    stageTimeText.text = "";
                    spawnTimer = 0f;
                    //InsertData();
                    //PlayerPrefsSetStage();
                }
            }
            if (_stageEnum == StageEnum.clear)
            {
                GameEnd();
                //break;
            }
            //for (int i = 0; i < (int)StageEnum.count; i++)
            //{
            //    if(i < (int)StageEnum.count - 1 && (int)_stageEnum == i)
            //    {
            //        SpawnEnemy(enemyPrefabs[i]);
            //        if (clearMonsterNum >= maxMonsterNum)
            //        {
            //            isStageClear = true;
            //            //_stageEnum = StageEnum.stage2;
            //            _stageEnum++;
            //            StageTimer = 0;
            //            clearMonsterNum = 0;
            //            monsterNum = 0;
            //            stageTimeText.text = "";
            //            spawnTimer = 0f;
            //        }
            //    }
            //    else if((int)_stageEnum == (int)StageEnum.count - 1)
            //    {
            //        GameEnd();
            //        //break;
            //    }
            //}
            //switch (_stageEnum)
            //{
            //    case StageEnum.stage1:
            //        {
            //            SpawnEnemy(enemyPrefab1);
            //            if (clearMonsterNum >= maxMonsterNum)
            //            {
            //                isStageClear = true;
            //                _stageEnum = StageEnum.stage2;
            //                StageTimer = 0;
            //                clearMonsterNum = 0;
            //                monsterNum = 0;
            //                stageTimeText.text = "";
            //                spawnTimer = 0f;
            //            }
            //            break;
            //        }
            //    case StageEnum.stage2:
            //        {
            //            SpawnEnemy(enemyPrefab2);
            //            if (clearMonsterNum >= maxMonsterNum)
            //            {
            //                isStageClear = true;
            //                _stageEnum = StageEnum.stage3;
            //                StageTimer = 0;
            //                clearMonsterNum = 0;
            //                monsterNum = 0;
            //                stageTimeText.text = "";
            //                spawnTimer = 0f;
            //            }
            //            break;
            //        }
            //    case StageEnum.stage3:
            //        {
            //            SpawnEnemy(enemyPrefab3);
            //            if (clearMonsterNum >= maxMonsterNum)
            //            {
            //                isStageClear = false;
            //                _stageEnum = StageEnum.clear;
            //                StageTimer = 0;
            //                stageTimeText.text = "";
            //                spawnTimer = 0f;
            //            }
            //            break;
            //        }
            //    case StageEnum.clear:
            //        {
            //            GameEnd();
            //            break;
            //        }
            //}
        }
        else if (isStageClear == true && monsterNum == 0)
        {
            restTimer -= Time.deltaTime;
            restTimeText.text = restTimer.ToString("00");//$"다음 스테이지 시작까지 남은 시간은 {restTimer.ToString("00")}초 입니다.";
            if (restTimer <= 0f)
            {
                isStageClear = false;
                restTimeText.text = "";
                restTimer = 30f;
            }
        }
        moneyText.text = money.ToString();
        restLifeText.text = $"{restLife.ToString()} / 20";// currentRestLife로 바꿀것
        if(restLife == 0)
        {
            GameOver();
        }
        if (isCreate == true || isDelete == true)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].enabled = true;
            }
        }

        for(int i = 0; i < TowerTr.Length; i++)
        {
            if (TowerTr[i].Count >= 3)
            {
                Debug.Log($"{((TowerName)i)} 3개 삭제 후 업그레이드");
                TowerUpgrade(Tower2Prefab[i], TowerTr[i]);
            }
        }

        //if(TowerTr1.Count >= 3)
        //{
        //    Debug.Log($"대포타워 3개 삭제 후 업그레이드");
        //    TowerUpgrade(Tower2Prefab1, TowerTr1);
        //    //node_TC1 = 0;
        //}
        //if (TowerTr2.Count >= 3)
        //{
        //    Debug.Log("공속타워 3개 삭제 후 업그레이드");
        //    TowerUpgrade(Tower2Prefab2, TowerTr2);
        //    //node_TC2 = 0;
        //}

    }

    void GameOver()
    {
        gameOverPanelObj.SetActive(true);
        isGameEnd = true;
        isCreate = false;
        isDelete = false;
        //_cameraController.enabled = false;

    }
    void GameEnd()
    {
        clearPanelObj.SetActive(true);
        isGameEnd = true;
        isCreate = false;
        isDelete = false;
        //_cameraController.enabled = false;
    }
    

    
    void SpawnEnemy(GameObject enemyPrefab)
    {
        spawnTimer += Time.deltaTime;
        
        if(spawnTimer >= 1f) // 1f
        {
           spawnTimer = 0f;
           if (monsterNum == maxMonsterNum) return;
           enemy = Instantiate(enemyPrefab,  Startobj.transform.position + EnemyPosition, Startobj.transform.rotation , enemyTr);
           monsterNum++;
           
        }
    }
    void TowerUpgrade(GameObject TowerPrefab, List<Transform> TowerTr)
    {

        for (int i = 0; i < nodes.Length; i++)
        {
            //Destroy(nodes[i].tower);
            if((nodes[i].transform.position + nodes[i].TowerPosition)== TowerTr[0].position)
            {
                Destroy(nodes[i].tower);
            }
            if ((nodes[i].transform.position + nodes[i].TowerPosition) == TowerTr[1].position)
            {
                Destroy(nodes[i].tower);
            }
            if ((nodes[i].transform.position + nodes[i].TowerPosition) == TowerTr[2].position)
            {
                Destroy(nodes[i].tower);
            }
            
            
        }
        Tower2Clone = Instantiate(TowerPrefab, TowerTr[0].position, TowerTr[0].rotation);
        for (int i=0; i< nodes.Length; i++)
        {
            if ((nodes[i].transform.position + nodes[i].TowerPosition) == TowerTr[0].position)
            {
                nodes[i].tower2 = Tower2Clone;
            }
        }
        
        TowerTr.Clear();

        
    }
    public void InsertData()
    {
        Param param = new Param();
        param.Add("money", money);
        param.Add("life", restLife);
        param.Add("stage", (int)_stageEnum);
        var bro = Backend.GameInfo.Insert("PlayerData", param);
        if (bro.IsSuccess())
        {
            print("데이터 삽입 성공");
            print($"{(int)_stageEnum},{restLife},{money}");
        }
        else
        {
            print("실패");
            print(bro.GetMessage());
        }
    }
    void PlayerPrefsSetStage()
    {
        PlayerPrefs.SetInt("stage", (int)_stageEnum);
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.SetInt("restLife", restLife);
        print($"스테이지 :{(int)_stageEnum}, {money}, {restLife}");
    }
    void PlayerPrefsGetStage()
    {
        PlayerPrefs.GetInt("stage");
        PlayerPrefs.GetInt("money");
        PlayerPrefs.GetInt("restLife");
    }

    public void TowerCreate()
    {
        isCreate = true;
        isDelete = false;
    }
    public void TowerDelete()
    {
        isDelete = true;
        isCreate = false;
    }
    public void NextStage()
    {
        isStageClear = false;
        restTimeText.text = "";
        restTimer = 30f;
    }

    public void MainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        isPause = !isPause;
        if(isPause == true)
        {
            pausePanel.SetActive(true);
            puaseBtn.GetComponent<Image>().sprite = resumeImage;
        }
        else if(isPause == false)
        {
            pausePanel.SetActive(false);
            puaseBtn.GetComponent<Image>().sprite = pauseImage;
            this.enabled = true;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefsGetStage();

    }
    public void PanelAnimationBtn()
    {
        isPannelMove = !isPannelMove;
        if (isPannelMove == false)
        {
            anim.Play("Panel_01");
            return;
        }
        else if (isPannelMove == true)
        {
            anim.Play("Panel_02");
            return;
        }
    }
    
}
