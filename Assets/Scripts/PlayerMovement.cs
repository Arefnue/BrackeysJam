using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController characterController;
	private Animator animator;

	[SerializeField]
	private float forwardMoveSpeed = 7.5f;
	[SerializeField]
	private float backwardMoveSpeed = 3;
	[SerializeField]
	private float turnSpeed = 150f;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		//Hareket etme tuşları ile yönü belirle
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");
		
		animator.SetFloat("Speed", vertical); //Animasyon için

		transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);// Sağ sol tuşlarına basıldığında döndürür

		if (vertical != 0)
		{
			float moveSpeedToUse = (vertical > 0) ? forwardMoveSpeed : backwardMoveSpeed; //İleri ve geri hız değerlerini belirler

			characterController.SimpleMove(transform.forward * (moveSpeedToUse * vertical)); //Hareket ettirir
		}
		
	}
}