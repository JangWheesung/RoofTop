using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieParticleEmpact : MonoBehaviour
{
    [SerializeField] private float firePower;
    [SerializeField] private float fireDamage;
    [SerializeField] private float icePower;
    [SerializeField] private Material zombieMaterial;
    [SerializeField] private Material coolingMaterial;
    [SerializeField] private float delayTime;

    private ZombieMovement zombieMovement;
    private ParticleSystem particle;
    bool isHunted;
    bool particleOn;
    bool isBurning;
    float burningTime;
    float coolingTime;
    float orgMovespeed;

    private void Awake()
    {
        zombieMovement = gameObject.GetComponent<ZombieMovement>();
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();

        orgMovespeed = zombieMovement.moveSpeed;
    }

    private void Update()
    {
        burningTime -= Time.deltaTime;
        coolingTime -= Time.deltaTime;
        Burning();
        Colling();
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticleSystem particleSystem = other.GetComponent<ParticleSystem>();

        if (particleSystem.duration == 5)//화염방사기(during값이 "5")
        {
            Debug.Log("Fire?");
            burningTime = 5;
            Hunted(firePower);
        }

        if (particleSystem.duration == 1)
        {
            coolingTime = 5;
            Hunted(icePower);
        }
    }

    void Hunted(float power)
    {
        if (!isHunted)
        {
            StartCoroutine(Delay(delayTime));
            gameObject.GetComponent<Living>().OnDmage(power);
        }
    }

    void Burning()
    {
        if (burningTime > 0f)
        {
            if (!particleOn)
            {
                particleOn = true;
                particle.Play();
            }
            if (!isBurning)
            {
                StartCoroutine(BurnDelay(delayTime));
                gameObject.GetComponent<Living>().OnDmage(fireDamage);
            }
        }
        else
        {
            particleOn = false;
            particle.Stop();
        }
    }

    void Colling()
    {
        if (coolingTime > 0f)
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().material = coolingMaterial;
            }
            zombieMovement.moveSpeed = orgMovespeed / 2;
        }
        else
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().material = zombieMaterial;
            }
            zombieMovement.moveSpeed = orgMovespeed;
        }
    }

    IEnumerator Delay(float time)
    {
        isHunted = true;
        yield return new WaitForSeconds(time);
        isHunted = false;
    }

    IEnumerator BurnDelay(float time)
    {
        isBurning = true;
        yield return new WaitForSeconds(time);
        isBurning = false;
    }
}
