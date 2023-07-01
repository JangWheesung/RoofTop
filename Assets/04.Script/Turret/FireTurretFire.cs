using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTurretFire : TurretFire
{
    bool playNow;

    protected override void Fire()
    {
        if (target != null)
        {
            //불 발사파티클 + 소리
            if (!playNow)
            {
                Debug.Log("시발 발사");
                firePart.Play();
                audioSource.Play();
                playNow = true;
            }

            //방향보기
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5f * Time.deltaTime);

            Quaternion thisRot = Quaternion.Euler(0, transform.eulerAngles.y - 180, 0);

            if (!attackDelay)
            {
                StartCoroutine(Delay(delayTime));

                target.transform.GetComponent<Living>().OnDmage(firePower);
            }
        }
        else
            playNow = false;
    }
}
