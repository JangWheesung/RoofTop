using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [SerializeField] Transform enemySponer;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject pressText;

    public float enemyCount = 10;
    float enemyAttack = 1;
    float enemyHp = 5;

    public int wave = 0;
    public bool isWaving;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && !isWaving)
            NextWave();
    }

    private void LateUpdate()
    {
        isWaving = enemyCount <= 0 ? false : true;
        pressText.SetActive(!isWaving);
    }

    void NextWave()
    {
        wave++;
        enemyCount = 7 + (wave * 3);
        enemyAttack = 1 + (wave * 0.125f);
        //enemyHp = 5 + (Mathf.Sqrt(wave) * ((wave + 1) / 2));
        enemyHp = (wave * 5);

        StartCoroutine(Spon(0.5f));
    }

    IEnumerator Spon(float time)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(time);
            GameObject em = Instantiate(enemy, enemySponer);
            em.transform.GetComponent<ZombieHP>().health = enemyHp;
            em.transform.GetComponent<ZombieMovement>().attack = enemyAttack;
        }
    }
}
