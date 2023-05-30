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
    public List<LevelUpGraph> maxBulletLevelCostume;
    public List<LevelUpGraph> magazineLevelCostume;
    public List<LevelUpGraph> firepowerLevelCostume;

    public int maxBulletLevel = 1;
    public int magazineLevel = 1;
    public int firepowerLevel = 1;
}
