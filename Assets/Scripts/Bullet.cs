using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int tower_damage;
    public GameObject bulletEffect;
    public float explosionRadius;
    private Transform target;
    private Transform lockOn;
    public Tower.TowerType type = Tower.TowerType.MachineGunTower;
    public bool catapult;
    public bool isLockOn;
    
    void Start()
    {
        if (type == Tower.TowerType.CannonTower)
        {
            isLockOn = true;
        }

        //if (type == Tower.TowerType.MachineGunTower)
        //{
        //    Vector3 dir = target.position - transform.position;
        //    transform.rotation = Quaternion.LookRotation(dir);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (type == Tower.TowerType.CannonTower)
        {
            if (isLockOn)
            {
                if (!target) return;
                Vector3 Vo = CalculateCatapult(target.transform.position, transform.position, 1);
                transform.GetComponent<Rigidbody>().velocity = Vo;
                isLockOn = false;
            }
        }
        else if (type == Tower.TowerType.MachineGunTower)
        {
            if (!target)
            {
                Destroy(this.gameObject);
                return;
            }
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * 30f * Time.deltaTime, Space.World);
        }
        //if (lockOn)
        //{
        //    //if (!target) return;
        //    Vector3 Vo = CalculateCatapult(target.transform.position, transform.position, 1);

        //    transform.GetComponent<Rigidbody>().velocity = Vo;
        //    lockOn = false;
        //}
        //if (!target)
        //{
        //    Destroy(this.gameObject);
        //    return;
        //}
        //Vector3 dir = target.position - transform.position;
        //transform.Translate(dir.normalized * 30f * Time.deltaTime, Space.World);


    }
    Vector3 CalculateCatapult(Vector3 target, Vector3 origen, float time)
    {
        Vector3 distance = target - origen;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    //x = S.x + V.x * t;
    //y = S.y + (V.y * t) - ((1/2) * g * t * t);
    //z = S.z + V.z * t;
    //t는 시간S는 시작위치g는 중력 가속도이지만
    //반드시 9.8일 필요는 없고 실제 게임에서 자연스럽게 보이는 값을 지정.
    //특정시간은 t의 값에 그 시간을 넣고 x,y,z를 최종위치값으로 넣어서
    //V값을 역산해 결정한 다음매 프레임마다 t값을 갱신해 가면서 x, y, z를 구하시면 됨
   
    }


    public void Seek(Transform _target)
    {
        target = _target;
    }
    public void Damege(int damage)
    {
        tower_damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if(type == Tower.TowerType.MachineGunTower)
            {
                other.GetComponent<Enemy>().Damaged(tower_damage);
                var effectClone = Instantiate(bulletEffect, transform.position, transform.rotation);
                Destroy(effectClone, 0.5f);
                Destroy(this.gameObject);
            }

        }
        if(other.CompareTag("Ground"))
        {
            if (type == Tower.TowerType.CannonTower)
            {
                var effectClone = Instantiate(bulletEffect, transform.position, transform.rotation);
                Destroy(effectClone, 0.5f);
                Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        collider.GetComponent<Enemy>().Damaged(tower_damage);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
