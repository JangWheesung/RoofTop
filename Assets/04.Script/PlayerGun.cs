using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [Header("Gun")]
    [SerializeField] float magazine;
    [SerializeField] float maxBullets = 30;
    [SerializeField] float nowBullet;

    [Header("Value")]
    [SerializeField] float shootDelayTime = 0.1f;
    [SerializeField] float reloadTime = 2f;

    [Header("particle")]
    [SerializeField] ParticleSystem gunFireParticle;
    [SerializeField] ParticleSystem gunSmokeParticle;

    bool canShoot = true;
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
    }

    void Shoot()
    {
        if (Input.GetButton("Fire1") && nowBullet > 0 && canShoot && starthoot && !reloading)
        {
            GunRay();

            nowBullet--;

            StartCoroutine(ShootDelay(shootDelayTime));
        }
    }

    void GunRay()
    {
        ray = cam.ScreenPointToRay(ScreenCenter);
        Debug.DrawRay(transform.position, -transform.right, Color.red, 100);

        Quaternion thisRot = Quaternion.Euler(transform.parent.transform.eulerAngles.x - 180, transform.parent.parent.transform.eulerAngles.y, 0);
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Enemy")))
        {
            //적 체력 가져와서 죽여
            hit.transform.GetComponent<Living>().OnDmage(1);
            SmokeManager.instance.PopSmoke(hit.point, thisRot);
            //아마?
        }
        else if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.Log(hit);
            SmokeManager.instance.PopSmoke(hit.point, thisRot);
        }

    }

    void Animing()
    {
        if (Input.GetButtonDown("Fire1") && !reloading)
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

        magazine -= (maxBullets - nowBullet);
        if (magazine >= maxBullets)
            nowBullet = maxBullets;
        else
            nowBullet = magazine;

        reloading = false;
        animator.SetBool("Reload", false);
    }
}
