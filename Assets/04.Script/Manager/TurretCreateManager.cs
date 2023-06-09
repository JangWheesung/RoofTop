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
    [SerializeField] private GameObject panel;

    GameObject player;
    Transform playerTurretPos;
    PlayerMovement playerMovement;
    PlayerGun playerGun;
    AudioSource audioSource;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerTurretPos = player.transform.GetChild(1);
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerGun = player.transform.GetChild(0).GetChild(0).GetComponent<PlayerGun>();

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void BuyTurret(int i)
    {
        if (playerGun.holdTurret)
            return;

        Time.timeScale = 1;
        playerMovement.canMove = true;

        if (MoneyManager.instance.money >= costs[i - 1])
        {
            MoneyManager.instance.money -= costs[i - 1];

            GameObject turret = Instantiate(turrets[i - 1], playerTurretPos.position, Quaternion.identity, player.transform.GetChild(1));
            turret.transform.localRotation = Quaternion.identity;

            playerGun.holdTurret = true;
            panel.SetActive(false);

            audioSource.Play();
        }
    }
}
