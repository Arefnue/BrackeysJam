using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropController : MonoBehaviour
{
    public float spawnPointY;

    public int hungerValue;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameMaster.instance.AddHungerToPlayer(hungerValue,gameObject);
            Debug.Log(GameMaster.instance.hungerOnPlayer);
            
        }
    }
}
