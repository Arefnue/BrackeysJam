using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

public class PropThrower : MonoBehaviour
{
    
    #region Singleton
    public static PropThrower instance;
    
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
    

    #endregion
    
    public PropController propController;

    public float force;


    public void ThrowProp(Vector3 position)
    {
        var instance = Instantiate(propController, position, Quaternion.identity);

        Rigidbody rb = instance.GetComponent<Rigidbody>();
        
        var dir = new Vector3(Random.Range(-1,1),1,Random.Range(-1,1));
        rb.AddForce(dir*force);
    }
    
}
