using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject iconE;
    [SerializeField] private float radius;
    bool range;

    PlayerGun playerGun;

    private void Awake()
    {
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
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
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
