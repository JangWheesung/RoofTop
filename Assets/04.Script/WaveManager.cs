using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [SerializeField] Transform enemySponer;
    [SerializeField] GameObject enemy;

    public int wave = 0;
    float enemyCount = 10;
    float enemyAttack = 1;
    float enemyHp = 5;

    void Awake()
    {
        instance = this;
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
        yield return new WaitForSeconds(time);
        GameObject em = Instantiate(enemy, enemySponer);
    }
}
