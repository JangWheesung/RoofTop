using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieParticleEmpact : MonoBehaviour
{
    [Header("Material")]
    [SerializeField] private Material zombieMaterial;
    [SerializeField] private Material coolingMaterial;

    [Header("Expact")]
    private float fireDamage;
    private float iceSlowing;
    public float[] fireDamageGraph;
    public float[] iceSlowingGragh;

    [Space(10f)]
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
        FireTurretFire turretFire = other.transform.parent.parent.parent.GetComponent<FireTurretFire>();
        ParticleSystem particleSystem = other.GetComponent<ParticleSystem>();

        switch (particleSystem.main.duration)
        {
            case 5: //화염포탑(during값이 "5")
                burningTime = 5;
                Hunted(turretFire.firePower);
                if(fireDamage < fireDamageGraph[turretFire.level - 1])
                    fireDamage = fireDamageGraph[turretFire.level - 1];
                break;
            case 3: //빙결포탑(during값이 "3")
                coolingTime = 5;
                Hunted(turretFire.firePower);
                if (iceSlowing < iceSlowingGragh[turretFire.level - 1])
                    iceSlowing = iceSlowingGragh[turretFire.level - 1];
                break;
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

            fireDamage = 0;
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
            zombieMovement.moveSpeed = orgMovespeed / iceSlowing;
        }
        else
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().material = zombieMaterial;
            }
            zombieMovement.moveSpeed = orgMovespeed;
            iceSlowing = 1;
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
