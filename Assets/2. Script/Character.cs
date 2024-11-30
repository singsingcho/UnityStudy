using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] InputActionProperty move;
    [SerializeField] InputActionProperty look;
    [SerializeField] InputActionProperty jump;
    [SerializeField] InputActionProperty interaction;

    protected Vector2 MoveDirection;
    protected Vector3 LookDirection;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float jumpPower;

    Vector3 controlDirection = Vector3.zero;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        originalStepOffset = controller.stepOffset;
    }

    private void FixedUpdate()
    {
        Move();
        Look();
        Jump();
        Interaction();

        controller.Move(controlDirection * Time.fixedDeltaTime);
    }

    void Move()
    {
        if (controller.isGrounded)
        {
            MoveDirection = move.action.ReadValue<Vector2>();

            controlDirection.x = MoveDirection.x * moveSpeed;
            controlDirection.z = MoveDirection.y * moveSpeed;
        }
    }

    void Look()
    {
        LookDirection = look.action.ReadValue<Vector2>();
        LookDirection.y = 0f;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.forward + LookDirection), rotateSpeed * Time.deltaTime);
    }

    private float ySpeed;
    private float originalStepOffset;
    void Jump()
    {
        ySpeed += Physics.gravity.y * Time.fixedDeltaTime;

        if (controller.isGrounded)
        {
            controller.stepOffset = originalStepOffset;
            ySpeed = -0.8f;

            if (jump.action.ReadValue<float>() > 0f)
            {
                ySpeed = jumpPower;
            }
        }
        else
        {
            controller.stepOffset = 0;
        }

        controlDirection.y = ySpeed;
    }

    void Interaction()
    {
        if (interaction.action.ReadValue<float>() > 0f)
        {
            GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < go.Length; i++)
            {
                if (Vector3.Distance(go[i].transform.position, transform.position) < 15f)
                {
                    Debug.Log(go[i].name);
                }
            }
        }
    }

    private void OnEnable()
    {
        move.action.Enable();
        look.action.Enable();
        jump.action.Enable();
    }

    private void OnDisable()
    {
        move.action.Disable();
        look.action.Disable();
        jump.action.Disable();   
    }
}
