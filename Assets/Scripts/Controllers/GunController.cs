using UnityEngine;

namespace Controllers
{
	public class GunController : MonoBehaviour
	{
		[SerializeField]
		[Range(0.1f, 1.5f)]
		private float fireRate = 0.3f;

		[SerializeField]
		[Range(1, 10)]
		private int damage = 1;

		[SerializeField]
		private ParticleSystem muzzleParticle;

		[SerializeField]
		private AudioSource gunFireSource;

		[SerializeField] private Transform playerTransform;

		[SerializeField] private Transform firePoint;

		[SerializeField] private BulletController bullet;
	
		public float bulletSpeed;

		private float timer;

		void Update()
		{
			timer += Time.deltaTime;// Atış sıklığı için timer. Performans artışı için güncellenebilir
			if (timer >= fireRate)
			{
				//Ateş etme tuşuna basarsa
				if (Input.GetButton("Fire1"))
				{
					timer = 0f;
					FireGun();
				}
			}
		}

		//Ateş et
		private void FireGun()
		{
			muzzleParticle.Play();//Ateş etme particle.Geçici
			gunFireSource.Play();// Ateş etme ses. Geçici

			Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f); //Ekranımızının tam ortasına ışın atar
			
			//Test
			Debug.DrawRay(firePoint.position, ray.direction * 100, Color.red, 12f); //Silahın ucundan rayin doğrultusunda ışın atar
			Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 12f); //Rayin merkezinden rayin doğrultusuna ışın atar
		
			
			BulletController newBullet = Instantiate(bullet,firePoint.position,Quaternion.identity) as BulletController; //Siahın ucunda mermi oluşturur.
			
			newBullet.speed = bulletSpeed; //Merminin hızını ayarlar
			newBullet.moveDirection = ray.direction*100; // Merminin gideceği yönü ayarlar. Buralarda bir yerlerde aim ile ilgili bir sıkıntı var.
		
			RaycastHit hitInfo;

			if (Physics.Raycast(ray, out hitInfo, 10))
			{
				//Eğer ışın bir şeye çarptıysa onun bilgilerini hitInfoya kaydeder
			}
		}
	}
}
