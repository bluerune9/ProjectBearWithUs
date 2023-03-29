using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    
    [SerializeField]
    private float rotationSpeed;
    
    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private float jumpButtonGracePeriod; //grace period so jump functions even if player presses button too early/too late (just enough)

    [SerializeField]
    private Transform cameraTransform;

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime; //question mark indicates that this field is nulliable. either float or null value.
    private float? jumpbuttonPressedTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
        
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        //**adjusting for gravity
        ySpeed += Physics.gravity.y * Time.deltaTime;

        
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            jumpbuttonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod) //instead of checking if the character is grounded now we check if it was within the grace period
        {
            characterController.stepOffset = originalStepOffset; 
            ySpeed = -0.5f; //to make sure our character jumps every time its grounded bc isGrounded function is a bit hit or miss
           
            
            if (Time.time - jumpbuttonPressedTime <= jumpButtonGracePeriod) // same logic for the button is pressed check as above
            {
                ySpeed = jumpSpeed;

                //resetting nullible fields when character jumps
                jumpbuttonPressedTime = null;
                lastGroundedTime = null;
            }
        }

        else 
        {
            characterController.stepOffset = 0;
        }
        //extracting our calculation and assigning it to a variable
        Vector3 velocity = movementDirection * speed * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);



        //to check if a character is moving we check if movement direction is not zero
        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up); //stores rotation specifically

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
    
}
