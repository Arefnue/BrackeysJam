using UnityEngine;

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
		timer += Time.deltaTime;
		if (timer >= fireRate)
		{
			if (Input.GetButton("Fire1"))
			{
				timer = 0f;
				FireGun();
			}
		}
	}

	private void FireGun()
	{
		muzzleParticle.Play();
		gunFireSource.Play();

		Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);

		Debug.DrawRay(firePoint.position, ray.direction * 100, Color.red, 12f);
		Debug.DrawRay(ray.origin, ray.direction * 100, Color.green, 12f);
		
		
		BulletController newBullet = Instantiate(bullet,firePoint.position,Quaternion.identity) as BulletController;
		newBullet.speed = bulletSpeed;
		newBullet.moveDirection = ray.direction*100;
		
		RaycastHit hitInfo;

		if (Physics.Raycast(ray, out hitInfo, 10))
		{
			
		}
	}
}
