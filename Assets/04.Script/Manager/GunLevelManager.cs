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

    public List<LevelUpGraph> maxBulletLevelCostume;
    public List<LevelUpGraph> magazineLevelCostume;
    public List<LevelUpGraph> firepowerLevelCostume;

    public int maxBulletLevel = 1;
    public int magazineLevel = 1;
    public int firepowerLevel = 1;

    private void Awake()
    {
        playerGun = FindObjectOfType<PlayerGun>();
    }

    public void MaxbulletLevelUp()
    {
        if (MoneyManager.instance.money >= maxBulletLevelCostume[maxBulletLevel].cost)
        {
            MoneyManager.instance.money -= maxBulletLevelCostume[maxBulletLevel].cost;
            maxBulletLevel++;
            playerGun.maxBullets += maxBulletLevelCostume[maxBulletLevel].increaseAmount;
        }
    }

    public void MagazineLevelUp()
    {
        if (MoneyManager.instance.money >= magazineLevelCostume[magazineLevel].cost)
        {
            MoneyManager.instance.money -= magazineLevelCostume[magazineLevel].cost;
            magazineLevel++;
            playerGun.magazine += magazineLevelCostume[magazineLevel].increaseAmount;
        }
    }

    public void PowerLevelUp()
    {
        if (MoneyManager.instance.money >= firepowerLevelCostume[firepowerLevel].cost)
        {
            MoneyManager.instance.money -= firepowerLevelCostume[firepowerLevel].cost;
            firepowerLevel++;
            playerGun.firePower += firepowerLevelCostume[firepowerLevel].increaseAmount;
        }
    }
}
