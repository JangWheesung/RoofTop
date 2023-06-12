using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretFire : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private Transform firePos;
    [SerializeField] private ParticleSystem firePart;
    [Header("Stats")]
    [SerializeField] private float radius;
    [SerializeField] private float firePower;
    [SerializeField] private float delayTime;

    private GameObject target;
    private bool attackDelay;

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
            firePart.Stop();
        }
    }

    void Fire()
    {
        if (target != null)
        {
            //방향보기
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;

            //발사
            Ray ray = new Ray(firePos.position, direction + new Vector3(0, 0.4f, 0));
            RaycastHit hit;
            bool lineRange = Physics.Raycast(ray, out hit, radius, LayerMask.GetMask("Enemy"));
            if (lineRange && !attackDelay)
            {
                StartCoroutine(Delay(delayTime));

                hit.transform.GetComponent<Living>().OnDmage(firePower);
                firePart.Play();
            }
        }
    }

    private IEnumerator Delay(float time)
    {
        attackDelay = true;
        yield return new WaitForSeconds(time);
        attackDelay = false;
    }
}
