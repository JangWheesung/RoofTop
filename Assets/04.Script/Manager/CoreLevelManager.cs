using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum UpgradeList
{
    SPEED,
    HP,
    HEAL
}

public class CoreLevelManager : MonoBehaviour
{
    PlayerHP playerHP;
    PlayerMovement playerMovement;
    AudioSource audioSource;

    public List<float> speedUpgradeGraph;
    public List<int> hpUpgradeGraph;
    public List<int> healUpgradeGraph;

    public int speedLevel = 1;
    public int hpLevel = 1;
    public int healLevel = 1;

    private void Awake()
    {
        playerHP = FindObjectOfType<PlayerHP>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        audioSource = gameObject.GetComponent<AudioSource>();

        playerMovement.walkingSpeed = speedUpgradeGraph[speedLevel - 1];
        playerHP.maxHealth = hpUpgradeGraph[hpLevel - 1];
        playerHP.healing = healUpgradeGraph[healLevel - 1];
    }

    public void SpeedUpgrade()
        => PowerLevelUp(UpgradeList.SPEED);

    public void HpUpgrade()
        => PowerLevelUp(UpgradeList.HP);

    public void HealUpgrade()
        => PowerLevelUp(UpgradeList.HEAL);

    private void PowerLevelUp(UpgradeList list)
    {
        switch (list)
        {
            case UpgradeList.SPEED:
                if (speedLevel <= 4 && MoneyManager.instance.core > 0)
                {
                    MoneyManager.instance.core -= 1;
                    speedLevel++;
                    playerMovement.walkingSpeed = speedUpgradeGraph[speedLevel - 1];
                    audioSource.Play();
                }
                break;
            case UpgradeList.HP:
                if (hpLevel <= 4 && MoneyManager.instance.core > 0)
                {
                    MoneyManager.instance.core -= 1;
                    hpLevel++;
                    playerHP.maxHealth = hpUpgradeGraph[hpLevel - 1];
                    audioSource.Play();
                }
                break;
            case UpgradeList.HEAL:
                if (healLevel <= 4 && MoneyManager.instance.core > 0)
                {
                    MoneyManager.instance.core -= 1;
                    healLevel++;
                    playerHP.healing = healUpgradeGraph[healLevel - 1];
                    audioSource.Play();
                }
                break;
        }
    }
}
