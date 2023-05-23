using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    float magazine;
    readonly float maxBullets;
    float nowBullet;

    float shootDelayTime;
    float reloadTime;

    bool canShoot;
    bool reloading;

    Ray ray;
    RaycastHit hit;

    Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {

    }

    void Shoot()
    {
        if (Input.GetButton("Fire1")  && canShoot && !reloading)
        {
            GunRay();

            nowBullet--;

            StartCoroutine(ShootDelay(shootDelayTime));
        }
    }

    void GunRay()
    {
        ray = new Ray(transform.position, cam.ScreenPointToRay(Input.mousePosition).direction);

        if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Enemy")
        {
            //적 체력 가져와서 죽여
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
        reloading = false;
        yield return new WaitForSeconds(time);

        magazine -= (maxBullets - nowBullet);
        if (magazine >= maxBullets)
            nowBullet = maxBullets;
        else
            nowBullet = magazine;

        reloading = true;
    }
}
