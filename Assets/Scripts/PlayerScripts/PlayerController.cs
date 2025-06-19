using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float sensitivity = 2f;
    public float lookXLimit = 90f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    public bool canMove = true;
    public bool isAlive = true;

    public bool canCastFireballs = false;
    public float castCooldown = 0.5f;
    
    public bool canWallJump = false;
    private bool wallJumpable = false;
    public float wallJumpCooldown = 1f;
    private float timer = 0f;
    
    public bool canDash = false;
    private bool isDashing = false;
    public float dashSpeed = 100f;
    public float dashDuration = 1f;
    public float dashLength = 3f;
    public float dashCooldown = 1f;

    public bool lightSecretObtained = false;
    public bool fireSecretObtained = false;
    public bool airSecretObtained = false;
    
    public int secretCount = 0;
    public int totalSecretCount = 3;

    void Start() {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        if (canMove) {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float currentSpeedX, currentSpeedY;
            if (isRunning) {
                currentSpeedX = runSpeed * Input.GetAxis("Vertical");
                currentSpeedY = runSpeed * Input.GetAxis("Horizontal");
            }
            else {
                currentSpeedX = walkSpeed * Input.GetAxis("Vertical");
                currentSpeedY = walkSpeed * Input.GetAxis("Horizontal");
            }
            
            float movementDirectionY = moveDirection.y;
            moveDirection = forward * currentSpeedX + right * currentSpeedY;

            if (Input.GetButton("Jump") && (characterController.isGrounded || (wallJumpable && canWallJump && Time.time > timer))) {
                if (wallJumpable) {
                    timer = Time.time + wallJumpCooldown;
                }
                moveDirection.y = jumpPower;
            } else {
                moveDirection.y = movementDirectionY;
            }

            if (!characterController.isGrounded) {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) && canDash && !isDashing) {
                StartCoroutine(Dash());
            }

            characterController.Move(moveDirection * Time.deltaTime);

            
            rotationX += -Input.GetAxis("Mouse Y") * sensitivity;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("WallJumpableWall")) {
            wallJumpable = true;
            //Debug.Log(other.name);
            //Debug.Log("CAN WALL JUMP");
        }
        
        
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("WallJumpableWall")) {
            wallJumpable = false;
            //Debug.Log("CAN NOT WALL JUMP");
        }
    }

    private IEnumerator Dash() {
        isDashing = true;
        float startTime = Time.time;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 dashDirection = (transform.right * moveX + transform.forward * moveZ) * dashLength;

        while (Time.time < startTime + dashDuration) {
            characterController.Move(dashDirection.normalized * Time.deltaTime * dashSpeed);
            yield return null;
        }
        
        yield return new WaitForSeconds(GetDashCooldown());
        isDashing = false;
    }

    public float GetCastCooldown() {
        if (fireSecretObtained) {
            return castCooldown / 2;
        }
        else {
            return castCooldown;
        }
    }

    private float GetDashCooldown() {
        if (airSecretObtained) {
            return dashCooldown / 2;
        }
        else {
            return dashCooldown;
        }
    }
}