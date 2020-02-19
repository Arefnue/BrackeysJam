using UnityEngine;

namespace Controllers
{
    public class BulletController : MonoBehaviour
    {
        public float speed;
        public Vector3 moveDirection;

        private void Start()
        {
            Invoke("KillThisBullet",3f); //Belli bir süre sonra mermiyi yok et.
        }

        private void Update()
        {
            transform.Translate(moveDirection * (speed * Time.deltaTime)); //Mermiyi belirlenen directionda sürekli ileri götürür.
        }

        //Merminin süresi dolduğunda çalışır
        void KillThisBullet()
        {
            Destroy(gameObject);
        }

        //Merminin çarptığı şeyin colliderını alıp other'a atar
        private void OnTriggerEnter(Collider other)
        {
            //Eğer mermi düşmana çarptıysa
            if (other.CompareTag("Enemy"))
            {
                //Buraya mermi yok olma animasyonu ve particleı gelecek
            
                Debug.Log("Bullet in Enemy");
                Destroy(gameObject);
            }
        
            //Eğer mermi yere çarptıysa
            if (other.CompareTag("Ground"))
            {
                //Buraya merminin yere çarpma animasyonu ve particleı gelecek.
            
                //Eğer zaman kalırsa mermi yere çarpınca mini Holwy'i çarptığı yerde açıp bir süre bırakacak bir fonksiyon eklenecek
            
                Debug.Log("Bullet in Ground");
                Destroy(gameObject);
            }
        }
    }
}
