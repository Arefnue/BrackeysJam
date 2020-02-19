using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolwyController : MonoBehaviour
{
    [Range(0,0.01f)]
    public float divRate;

    public float minLimit;
    public float maxLimit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EatThisHolwy();
        }

        if (other.CompareTag("Enemy"))
        {
            var enemyController = other.GetComponent<EnemyController>();

            var enemyControllerHungerValue = enemyController.hungerValue;
            Destroy(other.gameObject);
            GameMaster.instance.hungerOnHolwy -= enemyControllerHungerValue;
            
            var holwyRadius = new Vector3(enemyControllerHungerValue*(divRate),0,enemyControllerHungerValue*(divRate));
            transform.localScale -= holwyRadius;
            if (transform.localScale.magnitude <= minLimit)
            {
                Debug.Log("Holwy is dead");
                Destroy(gameObject);
            }

        }
    }

    public void EatThisHolwy()
    {
        if (GameMaster.instance.hungerOnPlayer <= 0)
        {
            Debug.Log("You have nothing...");
        }
        else
        {
            var hungerOnPlayer = GameMaster.instance.hungerOnPlayer;
            GameMaster.instance.hungerOnHolwy += hungerOnPlayer;
            GameMaster.instance.hungerOnPlayer = 0;
            var holwyRadius = new Vector3(hungerOnPlayer*(divRate),0,hungerOnPlayer*(divRate));
            transform.localScale += holwyRadius;
            
            if (transform.localScale.magnitude >= maxLimit)
            {
                Debug.Log("Holwy is big");
                
            }
        }
    }


}
