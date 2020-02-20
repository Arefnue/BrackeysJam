using System;
using UnityEngine;

namespace Controllers
{
    public class PropController : MonoBehaviour
    {
        public float spawnPointY;

        public int hungerValue;

        private bool playerHit = true;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                if (playerHit)
                {
                    playerHit = false;
                    GameMaster.instance.AddHungerToPlayer(hungerValue,gameObject);
                }
                
                
            }
        }

        private void LateUpdate()
        {
            playerHit = true;
        }
    }
}
