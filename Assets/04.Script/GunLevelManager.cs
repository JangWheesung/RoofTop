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
    MoneyManager moneyManager;

    public List<LevelUpGraph> maxBulletLevelCostume;
    public List<LevelUpGraph> magazineLevelCostume;
    public List<LevelUpGraph> firepowerLevelCostume;

    public int maxBulletLevel = 1;
    public int magazineLevel = 1;
    public int firepowerLevel = 1;

    private void Awake()
    {
        playerGun = FindObjectOfType<PlayerGun>();
        moneyManager = FindObjectOfType<MoneyManager>();
    }

    public void MaxbulletLevelUp()
    {
        if (moneyManager.money >= maxBulletLevelCostume[maxBulletLevel].cost)
        {
            moneyManager.money -= maxBulletLevelCostume[maxBulletLevel].cost;
            maxBulletLevel++;
            playerGun.maxBullets += maxBulletLevelCostume[maxBulletLevel].increaseAmount;
        }
    }

    public void MagazineLevelUp()
    {
        if (moneyManager.money >= magazineLevelCostume[magazineLevel].cost)
        {
            moneyManager.money -= magazineLevelCostume[magazineLevel].cost;
            magazineLevel++;
            playerGun.magazine += magazineLevelCostume[magazineLevel].increaseAmount;
        }
    }

    public void PowerLevelUp()
    {
        if (moneyManager.money >= firepowerLevelCostume[firepowerLevel].cost)
        {
            moneyManager.money -= firepowerLevelCostume[firepowerLevel].cost;
            firepowerLevel++;
            playerGun.firePower += firepowerLevelCostume[firepowerLevel].increaseAmount;
        }
    }
}
