using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Character : Player
{
    [SerializeField] CharacterController controller;

    [SerializeField] InputActionProperty move;
    [SerializeField] InputActionProperty look;
    [SerializeField] InputActionProperty jump;
    [SerializeField] InputActionProperty interaction;

    protected Vector2 MoveDirection;
    protected Vector2 LookDirection;

    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float jumpPower;

    Vector3 controlDirection = Vector3.zero;
    RunState run = new RunState();
    IdleState idle = new IdleState();

    private void Start()
    {
        originalStepOffset = controller.stepOffset;
        targetRotation = transform.rotation;
        targetCameraRotation = Camera.main.transform.localRotation;
    }

    private void FixedUpdate()
    {
        Jump();

        controller.Move(controlDirection * Time.fixedDeltaTime);
    }

    void Move(InputAction.CallbackContext obj)
    {
        if (controller.isGrounded)
        {
            MoveDirection = obj.ReadValue<Vector2>();

            controlDirection.x = MoveDirection.x * moveSpeed;
            controlDirection.z = MoveDirection.y * moveSpeed;

            if (MoveDirection.magnitude > 0)
            {
                SetState(run);
            }
            else
            {
                SetState(idle);
            }
        }
    }

    void MoveEnd(InputAction.CallbackContext obj)
    {
        if (controller.isGrounded)
        {
            MoveDirection = Vector2.zero;
            controlDirection.x = 0.0f;
            controlDirection.z = 0.0f;
            SetState(idle);
        }
    }

    private float verticalLookRotation;
    private Quaternion targetRotation;
    private Quaternion targetCameraRotation;
    void Look(InputAction.CallbackContext obj)
    {
        LookDirection = obj.ReadValue<Vector2>();
        LookDirection.y = 0f;

        targetRotation *= Quaternion.Euler(0f, LookDirection.x * rotateSpeed, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime);

        verticalLookRotation -= LookDirection.y * rotateSpeed;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -70f, 80f);
        targetCameraRotation = Quaternion.Euler(verticalLookRotation, 0f, 0f);
        Camera.main.transform.localRotation = Quaternion.Slerp(Camera.main.transform.localRotation, targetCameraRotation, Time.fixedDeltaTime);
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
            else
            {
                if (!move.action.IsInProgress())
                {
                    MoveDirection = Vector2.zero;
                    controlDirection.x = 0.0f;
                    controlDirection.z = 0.0f;
                    SetState(idle);
                }
            }
        }
        else
        {
            controller.stepOffset = 0;
        }

        controlDirection.y = ySpeed;
    }

    void Interaction(InputAction.CallbackContext obj)
    {
        if (obj.ReadValue<float>() > 0f)
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
        interaction.action.Enable();

        move.action.performed += Move;
        move.action.canceled += MoveEnd;
        look.action.performed += Look;
        interaction.action.performed += Interaction;
    }

    private void OnDisable()
    {
        move.action.Disable();
        look.action.Disable();
        jump.action.Disable();
        interaction.action.Disable();

        move.action.performed -= Move;
        move.action.canceled -= MoveEnd;
        look.action.performed -= Look;
        interaction.action.performed -= Interaction;
    }
}
