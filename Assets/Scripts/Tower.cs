using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum TowerKind
//{
//    CannonTower = 0,
//    MachineGunTower,
//    kindnum
//}
public enum TowerName
{
    공속타워 = 0,
    대포타워,
    업그레이드공속타워,
    업그레이드대포타워
}

public class Tower : MonoBehaviour
{
    [Header("타워 능력")]
    public float rotateSpeed;
    public float attackRange; //타워의 사거리
    public float turnSpeed;// 10f
    public float attackSpeed;
    public int damage;
    public float timer;


    public Transform target; //타워의 타겟
    public Transform TowerRotateTr;
    public GameObject bulletPrefab;//총알프리펩
    public GameObject shootEff;
    public Transform bulletShootTr; //총알 생성 위치
    protected GameManager _gameManager;
    public List<Enemy> enemiesList;
    public TowerType type = TowerType.MachineGunTower;
    //private Transform lockOnTr;

    public enum TowerType
    {
        CannonTower = 0,
        MachineGunTower,
        kindnum
    }


    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        timer = 0f;
        target = GetComponent<Transform>();
        rotateSpeed = 60f;
        //range = 12f;
        //turnSpeed = 10f;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_gameManager.isPause == true) return;
        if (_gameManager.isGameEnd == true) return;
        timer += Time.deltaTime;
        
        TargetSearch();
        TowerRotation();

        if(target)
        {
            if (timer >= attackSpeed)//0.5f
            {
                timer = 0f;
                Shoot(target);
            }
        }
    }
    void TargetSearch()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        float shortesDistance = Mathf.Infinity;
        GameObject nearEnemy = null;
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].CompareTag("Enemy"))
            {
                float distanceToenemy = Vector3.Distance(transform.position, colliders[i].transform.position);
                if (distanceToenemy < shortesDistance)
                {
                    shortesDistance = distanceToenemy;
                    nearEnemy = colliders[i].gameObject;
                }
            }
        }
        // 가까운 적 + 범위 안 때리던 적 유지
        if (nearEnemy && shortesDistance <= attackRange)
            target = nearEnemy.transform;
        else
            target = null;
        //Enemy[] enemies = FindObjectsOfType<Enemy>();

        //enemiesList
        //for (int i = 0; i < enemiesList.Count; i++)
        //{
        //    if (enemiesList[i] == null) continue;
        //    float distanceToenemy = Vector3.Distance(transform.position, enemiesList[i].transform.position);
        //    if (distanceToenemy < shortesDistance)
        //    {
        //        shortesDistance = distanceToenemy;
        //        nearEnemy = enemiesList[i];
        //    }
        //}



        //else
        //{
        //    target = null;
        //}
    }

    void TowerRotation()
    {
        if (!target) return;
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(TowerRotateTr.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        TowerRotateTr.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        //if (type == TowerType.MachineGunTower)
        //{
        //    TowerRotateTr.forward = dir;
        //}
        //else if (type == TowerType.CannonTower)
        //{
        //    Quaternion lookRotation = Quaternion.LookRotation(dir);
        //    Vector3 rotation = Quaternion.Lerp(TowerRotateTr.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        //    TowerRotateTr.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        //}


    }

    
    void Shoot(Transform attack)
    {
        if(type == TowerType.CannonTower)
        {
            //lockOnTr = attack;
            Instantiate(shootEff, bulletShootTr.transform.position, bulletShootTr.rotation);
            var bulletClone = Instantiate(bulletPrefab, bulletShootTr.position, bulletShootTr.rotation);
            Bullet bullet = bulletClone.GetComponent<Bullet>();
            bullet.Seek(attack);
            bullet.Damege(damage);
        }
        else if(type == TowerType.MachineGunTower)
        {
            Instantiate(shootEff, bulletShootTr.transform.position, bulletShootTr.rotation);
            var bulletClone = Instantiate(bulletPrefab, bulletShootTr.position, bulletShootTr.rotation);
            Bullet bullet = bulletClone.GetComponent<Bullet>();
            //bullet.Seek(target);
            bullet.Seek(attack);
            bullet.Damege(damage);
        }
        


        //Vector3 dir = target.position - bulletShootTr.transform.position;
        //float speed = 70f * Time.deltaTime;
        //bulletClone.transform.Translate(dir.normalized * speed, Space.World);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        enemiesList.Add(other.GetComponent<Enemy>());
    //    }
    //}

}
