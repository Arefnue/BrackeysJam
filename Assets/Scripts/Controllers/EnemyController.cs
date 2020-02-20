using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers
{
    public class EnemyController : MonoBehaviour
    {    
        [Header("EnemyStats")]
        #region EnemyStats
        
        public int hungerValue;

        public int damage;

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
            Destroy,
            Idle
        
        }
        
        [HideInInspector]public State state = State.Idle;
        
        private PlayerMovement playerObject;

        #endregion
        

        private void Start()
        {
            GameMaster.instance.enemyList.Add(this);
            playerObject = FindObjectOfType<PlayerMovement>();
            playerTransform = playerObject.transform;
            holwyTransform = GameObject.FindGameObjectWithTag("Holwy").transform;
            
            agent = GetComponent<NavMeshAgent>();

            agent.stoppingDistance = attackRangeRadius;
            spawnPosition = transform.position;
            state = State.Idle;

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
                    StartCoroutine(Attack());
                    
                    Debug.Log("Attack!");
                    break;
                case State.Busy:
                    
                    //Animasyonlar için zaman gerektiğinde kullanmak için
                    
                    Debug.Log("Busy!");
                    break;
                case State.Destroy:
                    
                    //Öldüğünde olacak şeyler
                    
                    break;
                case State.Idle:
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
            if (state == State.Busy)
            {
                
            }
            else if (Vector3.Distance(transform.position, playerTransform.position) <= attackRangeRadius)
            {
                state = State.Attack;
            }
            else
            {
                targetTransform = playerTransform; //Düşmanların hedefini playera odaklar. Zaman yeterse AI güçlendirip belli kosullarda bunu Holwy'e yönlendireceğiz
                state = State.Move;
            }
        }

        IEnumerator Attack()
        {
            state = State.Busy;
            playerObject.TakeDamage(damage);
            Debug.Log("AAAr");
            yield return new WaitForSeconds(1f);

            state = State.Idle;
        }

        
        //Bir şeye çarpınca çalışır
        private void OnTriggerEnter(Collider other)
        {
            //Eğer mermiye çarparsa
            if (other.CompareTag("Bullet"))
            {
                //Portal ile ölme animasyonu, particleı vs. buraya gelecek
                state = State.Destroy;
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
