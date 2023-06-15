using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelUpGraph
{
    public int cost;
    public float increaseAmount;
}

public class GunLevelManager : MonoBehaviour
{
    PlayerGun playerGun;
    AudioSource audioSource;

    public List<LevelUpGraph> maxBulletLevelCostume;
    public List<LevelUpGraph> magazineLevelCostume;
    public List<LevelUpGraph> firepowerLevelCostume;

    public int maxBulletLevel = 1;
    public int magazineLevel = 1;
    public int firepowerLevel = 1;

    private void Awake()
    {
        playerGun = FindObjectOfType<PlayerGun>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void MaxbulletLevelUp()
    {
        if (maxBulletLevel >= 9)//최대 레벨 도달
            return;

        if (MoneyManager.instance.money >= maxBulletLevelCostume[maxBulletLevel - 1].cost)
        {
            MoneyManager.instance.money -= maxBulletLevelCostume[maxBulletLevel - 1].cost;
            maxBulletLevel++;
            playerGun.maxBullets += maxBulletLevelCostume[maxBulletLevel - 1].increaseAmount;

            audioSource.Play();
        }
    }

    public void MagazineLevelUp()
    {
        if (magazineLevel >= 18)//이거 반복
        {
            if (MoneyManager.instance.money >= magazineLevelCostume[18].cost)
            {
                MoneyManager.instance.money -= magazineLevelCostume[18].cost;
                magazineLevel++;
                playerGun.magazine += magazineLevelCostume[18].increaseAmount;

                audioSource.Play();
            }
        }
        else if(MoneyManager.instance.money >= magazineLevelCostume[magazineLevel - 1].cost)
        {
            MoneyManager.instance.money -= magazineLevelCostume[magazineLevel - 1].cost;
            magazineLevel++;
            playerGun.magazine += magazineLevelCostume[magazineLevel - 1].increaseAmount;

            audioSource.Play();
        }
    }

    public void PowerLevelUp()
    {
        if (MoneyManager.instance.money >= 10 + ((int)Mathf.Floor((firepowerLevel - 1) / 2) * 3))
        {
            MoneyManager.instance.money -= 10 + ((int)Mathf.Floor((firepowerLevel - 1) / 2) * 3);
            firepowerLevel++;
            playerGun.firePower += 0.2f;

            audioSource.Play();
        }
    }
}
