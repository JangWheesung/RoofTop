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
                Debug.Log("�ù� �߻�");
                firePart.Play();
                audioSource.Play();
                playNow = true;
            }

            //���⺸��
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
