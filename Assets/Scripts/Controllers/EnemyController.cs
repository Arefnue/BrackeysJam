using UnityEngine;
using UnityEngine.AI;

namespace Controllers
{
    public class EnemyController : MonoBehaviour
    {    
        [Header("EnemyStats")]
        #region EnemyStats
        
        public int hungerValue;

        #endregion
        
        [Header("Navigation")]
        #region Navigation
        
        private Transform playerTransform;
        private Transform holwyTransform;
    
        private NavMeshAgent agent;
        private Transform targetTransform;
        
        [HideInInspector]
        public Vector3 spawnPosition;
        
        public float attackRangeRadius;
        
        #endregion
        
        #region EnemyBehaviour
        
        //State Machine for enemy 
        [HideInInspector]public enum State
        {
            Move,
            Attack,
            Busy,
            Destroy
        
        }
        
        [HideInInspector]public State state = State.Busy;

        #endregion
        

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
            DetermineState(); //Düşmanımızın davranışını burda belirliyoruz
            
            
            //Düşmanların hepsini öldürür
            if (Input.GetKeyDown(KeyCode.K))
            {
                state = State.Destroy;
                Destroy(gameObject);
            }
            
            //State machine
            switch (state)
            {
                case State.Move:
                    
                    //Hareket animasyonları vs. buraya gelecek
                    
                    agent.SetDestination(targetTransform.position);
                    break;
                case State.Attack:
                    
                    //Buraya düşmanın saldırı animasyonu ve particleı gelecek
                
                    //Saldırı sonrası statslar güncellenecek
                    
                    Debug.Log("Attack!");
                    break;
                case State.Busy:
                    
                    //Animasyonlar için zaman gerektiğinde kullanmak için
                    
                    Debug.Log("Busy!");
                    break;
                case State.Destroy:
                    
                    //Öldüğünde olacak şeyler
                    
                    break;
                default:
                    Debug.Log("Default!");
                    break;
            }
        
        
        }

        //Düşmanların ne yapacağını belirler
        private void DetermineState()
        {
            //Saldırı menzilindeyse saldır
            if (Vector3.Distance(transform.position, playerTransform.position) <= attackRangeRadius)
            {
                state = State.Attack;
            }
            else
            {
                targetTransform = playerTransform; //Düşmanların hedefini playera odaklar. Zaman yeterse AI güçlendirip belli kosullarda bunu Holwy'e yönlendireceğiz
                state = State.Move;
            }
        }

        
        //Bir şeye çarpınca çalışır
        private void OnTriggerEnter(Collider other)
        {
            //Eğer mermiye çarparsa
            if (other.CompareTag("Bullet"))
            {
                //Portal ile ölme animasyonu, particleı vs. buraya gelecek
                
                Debug.Log("Enemy is dead");
                Destroy(gameObject);
            }
        }

        //Düşman destroy olduğunda çalışır
        private void OnDestroy()
        {
            GameMaster.instance.enemyList.Remove(this);//Düşman listesinden bunu çıkarır
        }
    }
}
