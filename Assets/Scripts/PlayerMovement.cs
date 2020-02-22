using System;
using System.Collections;
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
	private float sideMoveSpeed = 7.5f;
	
	[SerializeField]
	private float turnSpeed = 150f;
	
	public int maxHealth = 100;
	public int currentHealth;

	public Health healthBar;

	private Vector3 moveDirection;
	public float gravityScale = 5f;

	public float jumpForce = 20f;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
	}

	private void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			UiMaster.instance.OpenPausePanel(true);
		}
		
		var yStore = moveDirection.y;
		//Hareket etme tuşları ile yönü belirle
		var horizontal = Input.GetAxis("Mouse X");
		var vertical = Input.GetAxis("Vertical");

		var horizontalMove = Input.GetAxis("Horizontal");

		var speedAnim = vertical;
		var forwardMoveSpeedHolder = forwardMoveSpeed;
		if (Input.GetKey(KeyCode.LeftShift) && vertical >0)
		{
			forwardMoveSpeedHolder *= 2;
			speedAnim *= 2;
			GameMaster.instance.canAttack = false;
		}
		else
		{
			GameMaster.instance.canAttack = true;
		}
		
		
		animator.SetFloat("Speed", speedAnim); //Animasyon için
		animator.SetFloat("Rotate",horizontalMove);
		transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);// Sağ sol tuşlarına basıldığında döndürür
		
		
		if (vertical != 0)
		{
			float moveSpeedToUse = (vertical > 0) ? forwardMoveSpeedHolder : backwardMoveSpeed; //İleri ve geri hız değerlerini belirler

			characterController.SimpleMove(transform.forward * (moveSpeedToUse * vertical)); //Hareket ettirir
		}

		if (horizontalMove != 0)
		{
			
			characterController.SimpleMove(transform.right * (sideMoveSpeed * horizontalMove));
		}
		moveDirection.y = yStore;

		if (characterController.isGrounded)
		{
			if (Input.GetButtonDown("Jump"))
			{
				moveDirection.y = 0f;
				moveDirection.y = jumpForce;
			}
		}

		moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);
		
		characterController.Move(moveDirection*Time.deltaTime);

		if (currentHealth <= 0)
		{
			StartCoroutine(PlayerOnDeath());
			Debug.Log("Player died");
			
		}
		

	}

	IEnumerator PlayerOnDeath()
	{
		animator.SetBool("MeDie",true);
		yield return new WaitForSeconds(3f);
		UiMaster.instance.OpenPlayerDeadPanel(true);
	}
	
	
	
	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		healthBar.SetHealth(currentHealth);
	}
}