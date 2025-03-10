using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")] public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpForce;
    public LayerMask groundLayer;

    [Header("Look")] public Transform cameraContainer;
    public float mixXLook;
    public float maxXLook;
    private float camCurxRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;
    
    [HideInInspector] public bool canLook = true;
    
    public Action inventory;
    
    private Rigidbody rd;
    public JumpPaddle jp;
    public float useStamina;
    
    private void Awake()
    {
        rd = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        moveSpeed = 5f;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnInventoryButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    public void OnUsePaddle(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started&&jp.IsPad)
        {
            Vector3 jumpForce = new Vector3(0, 200f, 0);
            rd.AddForce(jumpForce, ForceMode.Impulse);
        }
    }
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
            curMovementInput = context.ReadValue<Vector2>();
        else
            curMovementInput = Vector2.zero;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rd.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            CharacterManager.Instance.Player.condition.UseStamina(useStamina);
        }
    }
    
    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        
        dir *= moveSpeed;
        dir.y = rd.velocity.y;
        
        rd.velocity = dir;
    }

    private void CameraLook()
    {
        camCurxRot += mouseDelta.y * lookSensitivity;
        camCurxRot = Mathf.Clamp(camCurxRot, -maxXLook, maxXLook);
        cameraContainer.localRotation = Quaternion.Euler(-camCurxRot, 0, 0);
        
        transform.eulerAngles += new Vector3(0,mouseDelta.x * lookSensitivity,0);
    }
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward*0.2f)+(transform.up*0.01f),Vector3.down),
            new Ray(transform.position + (-transform.forward*0.2f)+(transform.up*0.01f),Vector3.down),
            new Ray(transform.position + (transform.right*0.2f)+(transform.up*0.01f),Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayer))
            {
                return true;
            }
        }
        return false;
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;    
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
