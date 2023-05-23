using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] float magazine;
    [SerializeField] float maxBullets = 30;
    [SerializeField] float nowBullet;

    [SerializeField] float shootDelayTime = 0.1f;
    [SerializeField] float reloadTime = 2f;

    bool canShoot = true;
    bool reloading = false;

    Ray ray;
    RaycastHit hit;

    Camera cam;
    Vector2 ScreenCenter;

    void Awake()
    {
        cam = Camera.main;
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
        if (Input.GetButton("Fire1") && nowBullet > 0 && canShoot && !reloading)
        {
            GunRay();

            nowBullet--;

            StartCoroutine(ShootDelay(shootDelayTime));
        }
    }

    void GunRay()
    {
        //ray = new Ray(transform.position, cam.ScreenPointToRay(Input.mousePosition).direction);
        //ray = cam.ScreenPointToRay(ScreenCenter);
        //Debug.DrawRay(ray);
        ray = cam.ScreenPointToRay(ScreenCenter);
        Debug.Log(Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Enemy")));
        Debug.DrawRay(transform.position, -transform.right, Color.red, 100);
        
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Enemy")))
        {
            //적 체력 가져와서 죽여
            hit.transform.GetComponent<Living>().OnDmage(1);
        }
    }

    void Animing()
    {
        //if(Input.GetButtonDown("Fire1"))
    }

    void ReLoad()
    {
        if (Input.GetKeyDown(KeyCode.R) && !reloading)
        {
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
        reloading = true;
        yield return new WaitForSeconds(time);

        magazine -= (maxBullets - nowBullet);
        if (magazine >= maxBullets)
            nowBullet = maxBullets;
        else
            nowBullet = magazine;

        reloading = false;
    }
}
