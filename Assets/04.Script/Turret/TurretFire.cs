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
        if (col.Length > 0)//�ƹ� ���� �����Ÿ��� ���Դ�
        {
            foreach (Collider c in col)
            {
                if (target == null)
                {
                    target = c.gameObject;
                }

                float rangeDistance = Vector3.Distance(transform.position, c.transform.position);
                float targetDistance = Vector3.Distance(transform.position, target.transform.position);
                if (rangeDistance < targetDistance)//���� ������ �ִ� ���� ã��
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
            //���⺸��
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;

            //�߻�

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
