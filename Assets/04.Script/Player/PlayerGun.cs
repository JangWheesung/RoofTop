using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] GameObject player;
    [SerializeField] GameObject panel;
    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip reloadSound;

    public float totalDmg;
    public bool canShoot = true;
    public bool holdTurret = false;
    bool starthoot = true;
    bool reloading = false;

    Ray ray;
    RaycastHit hit;

    Camera cam;
    Animator animator;
    AudioSource audioSource;
    Vector2 ScreenCenter;

    void Awake()
    {
        cam = Camera.main;
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip.LoadAudioData();
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

            audioSource.clip = fireSound;
            audioSource.Play();

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
            //�� ü�� �����ͼ� �׿�
            hit.transform.GetComponent<Living>().OnDmage(firePower);
            totalDmg += firePower;

            bloodManager.PopSmoke(hit.point, thisRot); audioSource.Play();
            //�Ƹ�?
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
            audioSource.clip = reloadSound;
            audioSource.Play();

            animator.SetBool("Reload", true);
            StartCoroutine(ReloadDelay(reloadTime));
        }
    }

    void TurretInstallation()
    {
        //��ġ���� ��������
        Transform turretPos = transform.parent.parent.GetChild(1);
        Collider[] col = Physics.OverlapBox(turretPos.position, rangeBox, turretPos.rotation);


        //if (Input.GetButtonDown("Fire1"))
        //{
        //    foreach(Collider co in col)
        //        Debug.Log(co.name);
        //}

        bool installY = player.transform.position.y >= 5.7f && player.transform.position.y <= 5.8f ? true : false;

        if (holdTurret)
        {
            Color green = new Color(0.5f, 1, 0.5f, 0.5f);
            Color red = new Color(1, 0.25f, 0.25f, 0.5f);
            ParticleSystem installRange = transform.parent.parent.GetChild(1).GetChild(0).GetChild(4).GetComponent<ParticleSystem>();
            installRange.startColor = col.Length <= 4 && installY ? green : red;
        }

        if (Input.GetButtonDown("Fire1") && holdTurret && installY && !panel.activeSelf && col.Length <= 4)
        {
            GameObject installRange = transform.parent.parent.GetChild(1).GetChild(0).GetChild(4).gameObject;
            installRange.SetActive(false);

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
