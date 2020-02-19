using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Vector3 moveDirection;

    private void Start()
    {
        Invoke("KillThisBullet",3f);
    }

    private void Update()
    {
        transform.Translate(moveDirection * (speed * Time.deltaTime));
    }

    void KillThisBullet()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Bullet in Enemy");
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Debug.Log("Bullet in Ground");
            Destroy(gameObject);
        }
    }
}
