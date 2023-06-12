using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurretInformation
{
    public int cost;
    public GameObject turret;
}

public class TurretCreateManager : MonoBehaviour
{
    [SerializeField] private int[] costs;
    [SerializeField] private GameObject[] turrets;

    GameObject player;
    Transform playerTurretPos;
    PlayerGun playerGun;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerTurretPos = player.transform.GetChild(1);
        playerGun = player.transform.GetChild(0).GetChild(0).GetComponent<PlayerGun>();
    }

    public void BuyTurret(int i)
    {
        if (MoneyManager.instance.money >= costs[i - 1])
        {
            MoneyManager.instance.money -= costs[i - 1];
            Instantiate(turrets[i - 1], playerTurretPos.position, Quaternion.identity, player.transform.GetChild(1));

            playerGun.holdTurret = true;
        }
    }
}
