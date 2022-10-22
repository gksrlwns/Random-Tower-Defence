using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : MonoBehaviour
{
    public enum TowerEnum
    {
        tower1,
        tower2
    }
    public TowerEnum _towerEnum;
    public Tower tower;
    public Tower2 tower2;
    public int tower_damage;
    public GameObject bulletEffect;
    public float explosionRadius;
    private Transform target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
        {
            Destroy(this.gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * 30f * Time.deltaTime, Space.World);
        
    }
    
    
    public void Seek(Transform _target)
    {
        target = _target;
    }
    public void Damege(int damage)
    {
        tower_damage = damage;
    }
    void Explode()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            collider.GetComponent<Enemy>().Damaged(tower_damage);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider collider in colliders)
            {
                collider.GetComponent<Enemy>().Damaged(tower_damage);
                Destroy(this.gameObject);
            }
        }

    }
        void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
