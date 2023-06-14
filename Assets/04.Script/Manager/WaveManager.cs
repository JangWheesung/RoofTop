using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    private PlayerHP playerHp;

    [SerializeField] Transform[] enemySponer;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject pressText;

    public float enemyCount;
    float enemyAttack = 1;
    float enemyHp = 5;

    public int wave = 0;
    public bool isWaving;

    void Awake()
    {
        instance = this;
        playerHp = FindObjectOfType<PlayerHP>();
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
        if (playerHp.health > 30)
            playerHp.health = 50;
        else
            playerHp.health += 20;

        wave++;
        enemyCount = 7 + (wave * 3);
        enemyAttack = 1 + (wave * 0.125f);
        enemyHp = 5 + (wave);

        StartCoroutine(Spon(0.5f));
    }

    IEnumerator Spon(float time)
    {
        float cnt = enemyCount;
        for (int i = 0; i < cnt; i++)
        {
            yield return new WaitForSeconds(time);
            Transform sponTrs = enemySponer[Random.Range(0, enemySponer.Length)];
            GameObject em = Instantiate(enemy, sponTrs);
            em.transform.GetComponent<ZombieHP>().health = enemyHp;
            em.transform.GetComponent<ZombieMovement>().attack = enemyAttack;
        }
    }
}
