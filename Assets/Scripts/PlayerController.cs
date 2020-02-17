using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private CharacterController characterController;
   public float moveSpeed = 10f;
   public float turnSpeed = 10f;
   public float jumpForce = 10f;
   public float gravityScale = 10f;
   private Vector3 moveDirection;
   public GunController theGun;

   private void Start()
   {
      characterController = GetComponent<CharacterController>();
   }


   private void Update()
   {
      MovePlayer();
      
      if (Input.GetMouseButtonDown(0))
      {
         theGun.isFiring = true;
      }

      if (Input.GetMouseButtonUp(0))
      {
         theGun.isFiring = false;
      }
      
   }

   void MovePlayer()
   {
      var yStore = moveDirection.y;
      var vertical = Input.GetAxisRaw("Vertical");
      var horizontal = Input.GetAxisRaw("Horizontal");

      moveDirection = (transform.forward * vertical) + (transform.right * horizontal);
      
      moveDirection = moveDirection.normalized*moveSpeed;
      moveDirection.y = yStore;

      if (characterController.isGrounded)
      {
         if (Input.GetButtonDown("Jump"))
         {
            moveDirection.y = 0f;
            moveDirection.y = jumpForce;
         }
      }
      
      moveDirection.y += (Physics.gravity.y * gravityScale*Time.deltaTime);
      var lookVector = moveDirection;
      lookVector.y = 0;
      
      characterController.Move(moveDirection*Time.deltaTime);
      
      if (lookVector.magnitude > 0)
      {
         if (vertical >= 0 || Math.Abs(horizontal) > 0)
         {
            var newDirection = Quaternion.LookRotation(lookVector);

            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * turnSpeed);
         }
         
      }

     
      
   }

   
   
}
