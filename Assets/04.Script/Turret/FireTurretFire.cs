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
                firePart.Play();
                audioSource.Play();
                playNow = true;
            }

            //방향보기
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5f * Time.deltaTime);
        }
        else
            playNow = false;
    }
}
