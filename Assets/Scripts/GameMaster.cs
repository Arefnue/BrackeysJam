using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance = null;

    public List<EnemyController> enemyList;

    public int enemyLimit;

    public int hungerOnPlayer;

    public int hungerLimit;

    public int hungerOnHolwy;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


    public void AddHungerToPlayer(int value,GameObject prop)
    {
        if (hungerOnPlayer + value >= hungerLimit)
        {
            OnCantCarry();
        }
        else
        {
            hungerOnPlayer += value;
            Destroy(prop);
        }
    }

    private void OnCantCarry()
    {
        Debug.Log("Cant Carry");
    }
    
    
}
