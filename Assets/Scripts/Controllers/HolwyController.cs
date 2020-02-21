using System;
using UnityEngine;

namespace Controllers
{
    public class HolwyController : MonoBehaviour
    {
        [Range(0,0.01f)]
        public float divRate;

        public float minLimit;
        public float maxLimit;

        private bool enemyHit = true;
        
        public int maxHealth = 100;
        public int currentHealth;

        public Health healthBar;

        private void Start()
        {
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            TakeDamage(maxHealth/2);
            GameMaster.instance.hungerOnHolwy = maxHealth / 2;
        }

        //Holwy'nin karnını doyurur
        public void EatThisHolwy()
        {
            //Eğer elimizde yemek yoksa
            if (GameMaster.instance.hungerOnPlayer <= 0)
            {    
                //Elimizde yemek olmayıp Holwy ile etkileşime geçince olacak şeyler burada
            
                Debug.Log("You have nothing...");
            }
            else
            {
                //Eğer elimizde yemek var ise
            
                var hungerOnPlayer = GameMaster.instance.hungerOnPlayer; //Ne kadar yemek olduğunu kaydet
                GameMaster.instance.hungerOnHolwy += hungerOnPlayer; //Holwy'nin karnını o kadar doyur
                TakeDamage(-hungerOnPlayer);
                GameMaster.instance.hungerOnPlayer = 0; //Elimizdeki yemekleri resetle
                UiMaster.instance.carrySlider.SetHealth(0);
                //Yedirdiğimiz oranda Holwy'i büyüt
                var holwyRadius = new Vector3(hungerOnPlayer*(divRate),0,hungerOnPlayer*(divRate)); 
                transform.localScale += holwyRadius;
                
                
            
                //Eğer yeterince büyük ise
                if (GameMaster.instance.hungerOnHolwy >= maxLimit)
                {
                    //Burası oyunu kazanma yeri
                    UiMaster.instance.OpenNextLevelPanel(true);
                    Debug.Log("Holwy is big");
                
                }
            }
        }
    
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                EatThisHolwy();
            }

            if (other.CompareTag("Enemy"))
            {
                if (enemyHit)
                {
                    enemyHit = false;
                    var enemyController = other.GetComponent<EnemyController>(); //Çarptığı düşmanın bilgilerini kaydet

                    var enemyControllerHungerValue = enemyController.hungerValue; //Düşmanın tadı ne kadar kötü

                    enemyController.state = EnemyController.State.Destroy;
                    Destroy(other.gameObject); //Bilgileri aldık o zaman destroy it
            
                    GameMaster.instance.hungerOnHolwy -= enemyControllerHungerValue; //Holwy'i bu kadar acıktır
            
            
                    var holwyRadius = new Vector3(enemyControllerHungerValue*(divRate),0,enemyControllerHungerValue*(divRate));// Holwy'i küçült
                    transform.localScale -= holwyRadius;
                    
                    TakeDamage(enemyControllerHungerValue);
                    
                }
                //Eğer Holwy çok küçülürse
                if (GameMaster.instance.hungerOnHolwy <= minLimit)
                {
                    //Oyunu kaybettin
                    
                    UiMaster.instance.OpenHolwyDeadPanel(true);
                    Debug.Log("Holwy is dead");
                    Destroy(gameObject);
                }
                

            }
        }

        private void LateUpdate()
        {
            enemyHit = true;
        }
        
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
    }
}
