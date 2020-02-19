using UnityEngine;

namespace Controllers
{
    public class PropController : MonoBehaviour
    {
        public float spawnPointY;

        public int hungerValue;


        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                GameMaster.instance.AddHungerToPlayer(hungerValue,gameObject);
                
            }
        }
    }
}
