using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    public int money;
    public int totalMoney { get; private set; }

    private void Awake()
        => instance = this;

    private void Update()
    {
        if(totalMoney < money)
            totalMoney = money;
    }
}
