using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpHight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 5.0f;

    private PlayerInput playerInput;
    private CharacterController controller;
    private Transform cameraTransform;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Animator anim;

    private InputActionMap gameActionMap;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        gameActionMap = playerInput.actions.FindActionMap("Game");
    }

    /* OnEnable and OnDisable are used to reduce script execution.
     * OnEnable will be started before "Start()", so you need to use "Awake()" instead of "Start()" function. */
    private void OnEnable()
    {
        gameActionMap.Enable();
    }

    private void OnDisable()
    {
        gameActionMap.Disable();
    }

    void Update()
    {
        /* Check grounded */
        groundedPlayer = controller.isGrounded;
        if(groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        /* Move */
        Vector2 Input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(Input.x, 0, Input.y);
        move = move.x * cameraTransform.right + move.z * cameraTransform.forward;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        /* Jump */
        /* I use CharacterController Component
         * So i can't use the Rigidbody normally
         * , i have to create gravity by myself in Scripting. */
        if (playerInput.actions["Jump"].triggered && groundedPlayer)
        {
            // anim.Play("Jump");
            playerVelocity.y += Mathf.Sqrt(jumpHight * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        /* Rotate character's face to the direction you move. */
        /* Default character rotation must be facing camera. */
        if (Input != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(Input.x, Input.y) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }
}