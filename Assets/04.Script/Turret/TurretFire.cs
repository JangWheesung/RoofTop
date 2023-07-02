using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class TurretFire : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject text;
    [SerializeField] protected ParticleSystem firePart;
    [SerializeField] private GameObject sponPart;
    [SerializeField] private GameObject installPart;
    [Header("Stats")]
    [SerializeField] protected float radius;
    [SerializeField] protected float firePower;
    [SerializeField] protected float delayTime;
    [SerializeField] protected int cost;

    public GameObject target;
    private PoolingManager bloodManager;
    private LineRenderer lineRenderer;
    protected AudioSource audioSource;
    private AudioSource installSource;
    private Vector3[] linePoints = new Vector3[2];
    protected bool attackDelay;

    bool install;

    private void Start()
    {
        bloodManager = GameObject.FindWithTag("Blood").GetComponent<PoolingManager>();
        lineRenderer = firePos.GetComponent<LineRenderer>();
        audioSource = gameObject.GetComponent<AudioSource>();
        installSource = transform.parent.GetComponent<AudioSource>();

        sponPart.SetActive(false);
        installPart.SetActive(true);

        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (!install)
        {
            install = true;
            installSource.Play();
        }

        ZombieRange();
        Fire();
        TurretSell();
    }

    protected void ZombieRange()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Enemy"));
        Collider[] notCol = Physics.OverlapSphere(transform.position, 0.7f, LayerMask.GetMask("Enemy"));
        if (col.Length - notCol.Length > 0)//아무 좀비나 사정거리에 들어왔다
        {
            foreach (Collider c in col)
            {
                bool notCatch = false;

                foreach (Collider nc in notCol)//너무 가까이 있으면 감지 안됨
                {
                    if (c == nc)
                        notCatch = true;
                }

                if (target == null && !notCatch)
                {
                    target = c.gameObject;
                }

                if (!notCatch)
                {
                    float rangeDistance = Vector3.Distance(transform.position, c.transform.position);
                    float targetDistance = Vector3.Distance(transform.position, target.transform.position);
                    if (rangeDistance < targetDistance)//가장 가까이 있는 좀비 찾기
                    {
                        target = c.gameObject;
                    }
                }
            }
        }
        else
        {
            target = null;
            lineRenderer.enabled = false;
            firePart.Stop();
            audioSource.Stop();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    protected virtual void Fire()
    {
        if (target != null)
        {
            //방향보기
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;

            Quaternion thisRot = Quaternion.Euler(0, transform.eulerAngles.y - 180, 0);

            if (!attackDelay)
            {
                StartCoroutine(Delay(delayTime));

                Vector3 hitPoint = new Vector3(target.transform.position.x, firePos.position.y, target.transform.position.z);//

                linePoints[0] = firePos.position;
                linePoints[1] = hitPoint;
                lineRenderer.SetPositions(linePoints);

                firePart.Play();
                audioSource.Play();

                target.transform.GetComponent<Living>().OnDmage(firePower);
                bloodManager.PopSmoke(hitPoint, thisRot);
            }
        }
    }

    protected void TurretSell()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, 1, LayerMask.GetMask("Player"));
        if (col.Length > 0)
        {
            text.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                MoneyManager.instance.money += cost;
                Destroy(transform.parent.gameObject);
            }
        }
        else
            text.SetActive(false);
    }

    protected IEnumerator Delay(float time)
    {
        attackDelay = true;
        for (int i = 0; i < 4; i++)
        {
            lineRenderer.enabled = true;
            yield return new WaitForSeconds(time / 8);
            lineRenderer.enabled = false;
            yield return new WaitForSeconds(time / 8);
        }
        attackDelay = false;
    }
}
