using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Animator yellowAnimator;

    private Vector3 moveDirection = Vector3.zero;
    private float horizontalInput;
    private float verticalInput;
    private float moveSpeed = 2;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            yellowAnimator.SetBool("Jump", true);
            
        }
        else
        {
            moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
                yellowAnimator.SetBool("Jump", false);
            }
        characterController.Move(moveDirection * Time.deltaTime * moveSpeed);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            yellowAnimator.SetBool("CanRun", true);
        }
        else { yellowAnimator.SetBool("CanRun", false); }
    }



}