using System;
using System.Collections;
using Cinemachine;
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
		
		[SerializeField] private Transform playerTransform;

		[SerializeField] private Transform firePoint;

		[SerializeField] private BulletController bullet;

		public AudioClip laserSound;
		
		public float bulletSpeed;

		private float timer;

		public LayerMask bulletLayer;

		LineRenderer line;

		private void Start()
		{
			line = GetComponent<LineRenderer>();
			line.SetVertexCount(2);
			
			line.SetWidth(0.1f, 0.25f);
		}

		void Update()
		{
			timer += Time.deltaTime;// Atış sıklığı için timer. Performans artışı için güncellenebilir
			if (timer >= fireRate)
			{
				//Ateş etme tuşuna basarsa
				if (Input.GetButton("Fire1") && GameMaster.instance.canAttack != false)
				{
					timer = 0f;
					FireGun();
				}
			}
		}

		//Ateş et
		private void FireGun()
		{
			//muzzleParticle.Play();//Ateş etme particle.Geçici
			SoundManager.instance.Play(laserSound);

			Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f); //Ekranımızının tam ortasına ışın atar
			
			// BulletController newBullet = Instantiate(bullet,firePoint.position,Quaternion.identity) as BulletController; //Siahın ucunda mermi oluşturur.
			//
			// newBullet.speed = bulletSpeed; //Merminin hızını ayarlar
			

			RaycastHit hitInfo;

			if (Physics.Raycast(ray, out hitInfo, 10000,bulletLayer))
			{
				//Eğer ışın bir şeye çarptıysa onun bilgilerini hitInfoya kaydeder

				StartCoroutine(ShotEffect());
				line.SetPosition(0,firePoint.position);
				line.SetPosition(1,hitInfo.point);
				
				//newBullet.moveDirection = hitInfo.point;
				
				if (hitInfo.collider.CompareTag("Enemy"))
				{
					EnemyController enemyController = hitInfo.collider.gameObject.GetComponent<EnemyController>();
					
					enemyController.EnemyOnDeath();

				}
			}
			
			
			
			//newBullet.transform.SetParent(GameMaster.instance.levelHolder);
		}

		IEnumerator ShotEffect()
		{
			line.enabled = true;
			yield return new WaitForSeconds(.01f);
			line.enabled = false;
		}

		
	}
}
