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
        public Transform throwHeadPosition;
        
        [HideInInspector]
        public Vector3 spawnPosition;
        
        public float attackRangeRadius;

        public bool isTargetHolwy;
        
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

        private Animator animator;

        public AudioClip enemyMoveSound;
        public AudioClip enemyAttackSound;

        private void Start()
        {
            GameMaster.instance.enemyList.Add(this);
            playerObject = FindObjectOfType<PlayerMovement>();
            playerTransform = playerObject.transform;
            holwyTransform = FindObjectOfType<HolwyController>().transform;
            
            animator = GetComponentInChildren<Animator>();
            
            agent = GetComponent<NavMeshAgent>();

            if (isTargetHolwy != true)
            {
                agent.stoppingDistance = attackRangeRadius;
                targetTransform = playerTransform;

            }
            else
            {
                agent.stoppingDistance = 0;
                targetTransform = holwyTransform;
            }
            
            spawnPosition = transform.position;
            state = State.Idle;

        }
        
        private void Update()
        {
            DetermineState(); //Düşmanımızın davranışını burda belirliyoruz
            
            
            //State machine
            switch (state)
            {
                case State.Move:
                    
                    animator.SetInteger("State",1);
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

                state = State.Move;
            }
        }

        IEnumerator Attack()
        {
            agent.SetDestination(transform.position);
            state = State.Busy;
            transform.LookAt(playerTransform);
            animator.SetInteger("State",2);
            
            
            
            yield return new WaitForSeconds(0.5f);
            
            SoundManager.instance.Play(enemyAttackSound);
            if (Vector3.Distance(transform.position, targetTransform.position) <= attackRangeRadius+1)
            {
                playerObject.TakeDamage(damage);
            }
            
            yield return new WaitForSeconds(0.5f);

            
            
            animator.SetInteger("State",0);
            state = State.Idle;
        }



        public void EnemyOnDeath()
        {
            StartCoroutine(AnimateDeath());
        }

        IEnumerator AnimateDeath()
        {
            state = State.Busy;
            agent.SetDestination(transform.position);
            animator.SetInteger("State",3);
            yield return new WaitForSeconds(1f);
            PropThrower.instance.ThrowProp(throwHeadPosition.position);
            Destroy(gameObject);
        }

        //Düşman destroy olduğunda çalışır
        private void OnDestroy()
        {
            GameMaster.instance.enemyList.Remove(this);//Düşman listesinden bunu çıkarır
        }
    }
}
