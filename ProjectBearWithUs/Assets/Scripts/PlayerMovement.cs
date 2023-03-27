using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;

    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;  //to fix the stepoffset glitch
    }

    // Update is called once per frame
    void Update()
    {
        //**mapping movement to the arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //3d vector for the player object to move
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        //**keeping movement consistent through both singular + diagonal direction 
        float magnitude = movementDirection.magnitude;
        magnitude = Mathf.Clamp01(magnitude);
        movementDirection.Normalize();

        //**adjusting for gravity
        ySpeed += Physics.gravity.y * Time.deltaTime;

        
        if (characterController.isGrounded)
        {
            characterController.stepOffset = originalStepOffset; 
            ySpeed = -0.5f; //to make sure our character jumps every time its grounded bc isGrounded function is a bit hit or miss
            if (Input.GetButtonDown ("Jump"))
            {
                ySpeed = jumpSpeed;
            }
        }

        else 
        {
            characterController.stepOffset = 0;
        }
        //ecstracting our calculation and assigning it to a variable
        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);



        //to check if a character is moving we check if movement direction is not zero
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up); //stores rotation specifically

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
