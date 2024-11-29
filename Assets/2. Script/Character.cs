using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float jumpPower;

    Vector3 moveDirection = Vector3.zero;
    private void Update()
    {
        Move();

        Jump();
    }

    void Move()
    {
        moveDirection = Vector3.zero;

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

    void Jump()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpPower;
                controller.SimpleMove(moveDirection);
            }
        }
    }
}
