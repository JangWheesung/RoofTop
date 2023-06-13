using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Living : MonoBehaviour
{
    public float health;
    public bool die;

    public virtual void OnDmage(float dmg)
    {
        health -= dmg;

        if (health <= 0 && !die)
        {
            die = true;
            Die();
        }
    }

    protected virtual void Die() { }
}
