using UnityEngine;

namespace Controllers
{
    public class HolwyController : MonoBehaviour
    {
        [Range(0,0.01f)]
        public float divRate;

        public float minLimit;
        public float maxLimit;
    
    
    
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
                GameMaster.instance.hungerOnPlayer = 0; //Elimizdeki yemekleri resetle
            
                //Yedirdiğimiz oranda Holwy'i büyüt
                var holwyRadius = new Vector3(hungerOnPlayer*(divRate),0,hungerOnPlayer*(divRate)); 
                transform.localScale += holwyRadius;
            
                //Eğer yeterince büyük ise
                if (transform.localScale.magnitude >= maxLimit)
                {
                    //Burası oyunu kazanma yeri
                
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
                var enemyController = other.GetComponent<EnemyController>(); //Çarptığı düşmanın bilgilerini kaydet

                var enemyControllerHungerValue = enemyController.hungerValue; //Düşmanın tadı ne kadar kötü
            
                Destroy(other.gameObject); //Bilgileri aldık o zaman destroy it
            
                GameMaster.instance.hungerOnHolwy -= enemyControllerHungerValue; //Holwy'i bu kadar acıktır
            
            
                var holwyRadius = new Vector3(enemyControllerHungerValue*(divRate),0,enemyControllerHungerValue*(divRate));// Holwy'i küçült
                transform.localScale -= holwyRadius;
                
                //Eğer Holwy çok küçülürse
                if (transform.localScale.magnitude <= minLimit)
                {
                    //Oyunu kaybettin
                    
                    Debug.Log("Holwy is dead");
                    Destroy(gameObject);
                }

            }
        }


    }
}
