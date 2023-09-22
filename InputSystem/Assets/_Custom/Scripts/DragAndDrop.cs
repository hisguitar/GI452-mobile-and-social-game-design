using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private InputAction touchPressAciton;
    private InputAction touchPositionAction;

    [SerializeField] private float touchDragPhysicsSpeed = 10.0f;
    [SerializeField] private float touchDragSpeed = 1.0f;

    private InputActionMap touchActionMap;
    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;

    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Awake()
    {
        mainCamera = Camera.main;
        touchPressAciton = playerInput.actions["TouchPress"];
        touchPositionAction = playerInput.actions["TouchPosition"];
        touchActionMap = playerInput.actions.FindActionMap("Touch");
    }
    /* OnEnable and OnDisable used [Find..]
     * to reduce script execution for increased performance.*/
    private void OnEnable()
    {
        touchPressAciton.performed += TouchPressed;
        touchActionMap.Enable();
    }

    private void OnDisable()
    {
        touchActionMap.Disable();
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        /* Ascending pressing speed (Tap > Press > Hold) */
        Debug.Log($"Press{context.interaction is PressInteraction}");
        Debug.Log($"Tap{context.interaction is TapInteraction}");
        Debug.Log($"Hold{context.interaction is HoldInteraction}");

        Vector3 position = touchPositionAction.ReadValue<Vector2>();
        Ray ray = mainCamera.ScreenPointToRay(position);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Draggable")
                || gameObject.layer == LayerMask.NameToLayer("Draggable"))
            {
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        float initialDistance = Vector3.Distance(clickedObject.transform.position, mainCamera.transform.position);
        clickedObject.TryGetComponent<Rigidbody>(out var rb);
        while(touchPressAciton.ReadValue<float>() != 0)
        {
            Vector3 position = touchPositionAction.ReadValue<Vector2>();
            Ray ray = mainCamera.ScreenPointToRay(position);
            if(rb != null)
            {
                Vector3 direction = ray.GetPoint(initialDistance) - clickedObject.transform.position;
                rb.velocity = direction * touchDragPhysicsSpeed;
                yield return waitForFixedUpdate;
            }
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position,
                    ray.GetPoint(initialDistance), ref velocity, touchDragSpeed);
                yield return null;
            }
        }
    }
}
