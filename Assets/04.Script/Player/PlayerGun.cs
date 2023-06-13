using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [Header("Gun")]
    public float magazine;
    public float maxBullets = 30;
    public float nowBullet;
    public float firePower = 1;

    [Header("Value")]
    [SerializeField] float shootDelayTime = 0.1f;
    [SerializeField] float reloadTime = 2f;
    [SerializeField] Vector3 rangeBox;

    [Header("Particle")]
    [SerializeField] ParticleSystem gunFireParticle;

    [Header("Pooling")]
    [SerializeField] PoolingManager smokeManager;
    [SerializeField] PoolingManager bloodManager;

    [Header("Other")]
    [SerializeField] GameObject panel;

    public bool canShoot = true;
    public bool holdTurret = false;
    bool starthoot = true;
    bool reloading = false;

    Ray ray;
    RaycastHit hit;

    Camera cam;
    Animator animator;
    Vector2 ScreenCenter;

    void Awake()
    {
        cam = Camera.main;
        animator = gameObject.GetComponent<Animator>();
        nowBullet = maxBullets;
        ScreenCenter = new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2);
    }

    void Update()
    {
        Shoot();
        Animing();
        ReLoad();
        TurretInstallation();
    }

    void Shoot()
    {
        if (Input.GetButton("Fire1") && nowBullet > 0 && canShoot && starthoot && !reloading && !holdTurret)
        {
            GunRay();

            nowBullet--;

            StartCoroutine(ShootDelay(shootDelayTime));
        }
    }

    void GunRay()
    {
        ray = cam.ScreenPointToRay(ScreenCenter);

        Quaternion thisRot = Quaternion.Euler(transform.parent.transform.eulerAngles.x - 180, transform.parent.parent.transform.eulerAngles.y, 0);
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Enemy")))
        {
            //적 체력 가져와서 죽여
            hit.transform.GetComponent<Living>().OnDmage(firePower);
            bloodManager.PopSmoke(hit.point, thisRot);
            //아마?
        }
        else if (Physics.Raycast(ray, out hit, 100))
        {
            smokeManager.PopSmoke(hit.point, thisRot);
        }

    }

    void Animing()
    {
        if (Input.GetButtonDown("Fire1") && !reloading && canShoot && !holdTurret)
        {
            starthoot = true;
            gunFireParticle.Play();
            animator.SetBool("Shoot", true);
        }
        if (Input.GetButtonUp("Fire1") || nowBullet <= 0)
        {
            starthoot = false;
            gunFireParticle.Stop();
            animator.SetBool("Shoot", false);
        }
    }

    void ReLoad()
    {
        if (Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            animator.SetBool("Reload", true);
            StartCoroutine(ReloadDelay(reloadTime));
        }
    }

    void TurretInstallation()
    {
        //설치가능 범위지정
        Transform turretPos = transform.parent.parent.GetChild(1);
        Collider[] col = Physics.OverlapBox(turretPos.position, rangeBox, Quaternion.identity);

        //if (col.Length > 0)
        //{
        //    Debug.Log($"개수 : {col.Length}");
        //    for (int i = 0; i < col.Length; i++)
        //    {
        //        Debug.Log(col[i].name);
        //    }
        //}

        if (Input.GetButtonDown("Fire1") && holdTurret && !panel.activeSelf && col.Length <= 4)
        {
            holdTurret = false;
            transform.parent.parent.GetChild(1).GetChild(0).GetChild(0).GetComponent<TurretFire>().enabled = true;
            transform.parent.parent.GetChild(1).GetChild(0).parent = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.parent.parent.GetChild(1).position, rangeBox);
    }

    IEnumerator ShootDelay(float time)
    {
        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;
    }

    IEnumerator ReloadDelay(float time)
    {
        starthoot = false;
        reloading = true;
        yield return new WaitForSeconds(time);

        if (magazine >= maxBullets)
        {
            magazine -= (maxBullets - nowBullet);
            nowBullet = maxBullets;
        }
        else
        {
            nowBullet += magazine;
            magazine = 0;
        }

        reloading = false;
        animator.SetBool("Reload", false);
    }
}
