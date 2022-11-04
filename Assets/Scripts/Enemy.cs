using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    public float timer;
    public float speed = 10f;
    public bool hpHit;
    private GameManager _gameManager;
    private Transform target;
    private int wavePointIndex = 0;
    private Vector3 dir;

    [Header("체력")]
    //public Canvas hpCanvas;
    public GameObject enemyRt;
    public Image healthBar;
    public Image BackhealthBar;
    public float hp; // 300
    public int moneyToClear;
    private float startHp;

    protected void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    protected void Start()
    {
        startHp = hp;
        target = WayPoint.wayPoints[0];
        hpHit = false;
    }
    protected void OnDestroy()
    {
        //몬스터 수
        _gameManager.clearMonsterNum++;

    }

    protected void FixedUpdate()
    {
        if (_gameManager.isPause == true) return;

        if (_gameManager.isGameEnd == true) return;
        dir = target.position - transform.position;
        transform.Translate(dir.normalized* Time.deltaTime * speed, Space.World);

        //healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, hp / startHp, Time.deltaTime * 5f);
        healthBar.fillAmount = hp / startHp;

        if (hpHit)
        {
            BackhealthBar.fillAmount = Mathf.Lerp(BackhealthBar.fillAmount, healthBar.fillAmount, Time.deltaTime * 10f);//hp/startHp;
            if (healthBar.fillAmount >= BackhealthBar.fillAmount - 0.01f)
            {
                hpHit = false;
                BackhealthBar.fillAmount = healthBar.fillAmount;
            }
        }
        if (hp <= 0)
        {
            //처치 보상
            _gameManager.money += moneyToClear;
            //몬스터 삭제
            Destroy(this.gameObject);
        }
        //if(Vector3.Distance(transform.position, target.position) <= 0.2f)
        //{
        //    GetNextWayPoint();
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //print(other.name +"와 접촉함");
        //if (other.CompareTag("WayPoint"))
        //{
        //    transform.Rotate(new Vector3(0,90,0));
        //}
        //if (other.CompareTag("WayPoint1"))
        //{
        //    transform.Rotate(new Vector3(0, -90, 0));
        //}
        if(other.CompareTag("WayPoint"))
        {
            GetNextWayPoint();
        }

        if(other.CompareTag("End"))
        {
            Destroy(this.gameObject);
            target = null;
            _gameManager.restLife--;
        }
    }
    void GetNextWayPoint()
    {
        wavePointIndex++;
        if (wavePointIndex > WayPoint.wayPoints.Length) return;
        target = WayPoint.wayPoints[wavePointIndex];
        enemyRt.transform.Rotate(_gameManager.EnemyRotation);
        //hpCanvas.transform.Rotate(_gameManager.EnemyRotation);
    }

    public void Damaged(int damage)
    {
        hp -= damage;
        Invoke("BackHpHit", 0.5f);
    }
    void BackHpHit()
    {
        hpHit = true;
    }
}
