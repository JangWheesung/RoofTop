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
            //�� �߻���ƼŬ + �Ҹ�
            if (!playNow)
            {
                firePart.Play();
                audioSource.Play();
                playNow = true;
            }

            //���⺸��
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0f;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 5f * Time.deltaTime);
        }
        else
            playNow = false;
    }
}
