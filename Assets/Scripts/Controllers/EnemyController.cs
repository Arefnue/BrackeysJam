using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform playerTransform;
    private Transform holwyTransform;
    
    private NavMeshAgent agent;
    private Transform targetTransform;

    public float attackRangeRadius;

    public Vector3 spawnPosition;

    public int hungerValue;
    
    
    [HideInInspector]public enum State
    {
        Move,
        Attack,
        Busy,
        Destroy
        
    }

    public float moveSpeed;

    [HideInInspector]public State state = State.Busy;
    
    
    private void Start()
    {
        GameMaster.instance.enemyList.Add(this);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        holwyTransform = GameObject.FindGameObjectWithTag("Holwy").transform;
       
        agent = GetComponent<NavMeshAgent>();

        agent.stoppingDistance = attackRangeRadius;

        spawnPosition = transform.position;

    }

    private void Update()
    {
        DetermineState();

        if (Input.GetKeyDown(KeyCode.K))
        {
            state = State.Destroy;
            Destroy(gameObject);
        }
        
        switch (state)
        {
            case State.Move:
                agent.SetDestination(targetTransform.position);
                break;
            case State.Attack:
                Debug.Log("Attack!");
                break;
            case State.Busy:
                Debug.Log("Busy!");
                break;
            case State.Destroy:
                break;
            default:
                Debug.Log("Default!");
                break;
        }
        
        
    }

    private void DetermineState()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRangeRadius)
        {
            
            state = State.Attack;
        }
        else
        {
            targetTransform = playerTransform;
            state = State.Move;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Enemy is dead");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameMaster.instance.enemyList.Remove(this);
    }
}
