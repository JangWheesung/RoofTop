using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject iconE;
    [SerializeField] private float radius;
    bool range;

    PlayerMovement playerMovement;
    PlayerGun playerGun;

    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerGun = FindObjectOfType<PlayerGun>();
    }

    void Update()
    {
        Range();
    }

    void Range()
    {
        if (Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Player")).Length > 0)
        {
            range = true;
            iconE.SetActive(true);
            playerGun.canShoot = false;
            if (Input.GetKeyDown(KeyCode.E))
            {
                panel.SetActive(panel.activeSelf == true ? false : true);

                if (panel.activeSelf)
                {
                    Time.timeScale = 0;
                    playerMovement.canMove = false;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Time.timeScale = 1;
                    playerMovement.canMove = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }
        else
        {
            if (range)
            {
                range = false;
                playerGun.canShoot = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                iconE.SetActive(false);
                panel.SetActive(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
