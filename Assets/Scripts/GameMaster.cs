using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Controllers;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance = null;

    public List<EnemyController> enemyList;

    public int enemyLimit;

    public int hungerOnPlayer;

    public int hungerLimit;

    public int hungerOnHolwy;

    public Transform levelHolder;

    public bool canAttack;
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

    private void Start()
    {
        UiMaster.instance.carrySlider.SetMaxHealth(hungerLimit);
        UiMaster.instance.carrySlider.SetHealth(0);
    }


    //Proplara çarpınca açlık değerini alır
    public void AddHungerToPlayer(int value,GameObject prop)
    {
        //Eğer almaya çalıştığımız şeyi taşıyamıyorsak
        if (hungerOnPlayer + value > hungerLimit)
        {
            OnCantCarry();
        }
        else
        {
            //Taşıyabiliyorsak
            
            hungerOnPlayer += value;//Envantere ekle
            UiMaster.instance.carrySlider.SetHealth(hungerOnPlayer);
            Destroy(prop);//Çarptığımız objeyi sil
            
        }
    }

    private void OnCantCarry()
    {
        //Taşıyamadığı zaman olacak şeyler buraya
        
        Debug.Log("Cant Carry");
    }
    
    
}
