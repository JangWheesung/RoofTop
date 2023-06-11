using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretFire : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private Transform firePos;
    [SerializeField] private ParticleSystem firePart;
    [Header("Value")]
    [SerializeField] private float radius;

    private GameObject target;

    void Update()
    {
        ZombieRange();
        Fire();
    }

    void ZombieRange()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Enemy"));
        if (col.Length > 0)//아무 좀비나 사정거리에 들어왔다
        {
            foreach (Collider c in col)
            {
                if (target == null)
                {
                    target = c.gameObject;
                }

                float rangeDistance = Vector3.Distance(transform.position, c.transform.position);
                float targetDistance = Vector3.Distance(transform.position, target.transform.position);
                if (rangeDistance < targetDistance)//가장 가까이 있는 좀비 찾기
                {
                    target = c.gameObject;
                }
            }
        }
        else
        {
            target = null;
        }
    }

    void Fire()
    {
        if (target != null)
        {
            transform.LookAt(target.transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
