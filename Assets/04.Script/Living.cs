using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Living : MonoBehaviour
{
    [SerializeField] protected float health;

    public virtual void OnDmage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
            Die();
    }

    protected virtual void Die() { }
}
