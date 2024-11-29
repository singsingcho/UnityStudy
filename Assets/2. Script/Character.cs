using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] InputActionProperty move;
    [SerializeField] InputActionProperty jump;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float jumpPower;

    Vector3 moveDirection = Vector3.zero;
    private void Awake()
    {
        move.reference.action.performed += Move;
        jump.reference.action.performed += Jump;
    }

    void Move(InputAction.CallbackContext obj)
    {
        moveDirection = obj.ReadValue<Vector3>();

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.z = moveSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x = -moveSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection.z = -moveSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x = moveSpeed;
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            moveDirection.z = 0;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            moveDirection.x = 0;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            moveDirection.z = 0;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            moveDirection.x = 0;
        }

        controller.SimpleMove(moveDirection);
    }

    void Jump(InputAction.CallbackContext obj)
    {
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //controller.f
            }
        }
    }
}
