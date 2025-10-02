using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [Header("Referencias")]
    public Transform cameraTransform;

    [Header("Configuración")]
    public float sensitivity = 0.5f;
    public float maxCameraDistance = 5f;

    [Header("Límites de rotación")]
    public float pitchMin = -30f;
    public float pitchMax = 30f;
    public float yawMin = -70f;
    public float yawMax = 70f;

    [Header("Colisión")]
    public LayerMask collisionMask;

    [SerializeField] private float yaw = 0f;
    [SerializeField] private float pitch = 0f;

    private InputManager inputActions;

    public Vector3 pivotPosition;
    public Vector3 desiredCameraPos;

    void Start()
    {
        inputActions = new InputManager();
        inputActions.Enable();

        Vector3 startRotation = transform.eulerAngles;
        yaw = startRotation.y;
        pitch = startRotation.x;
    }

    void Update()
    {
        HandleInput();
        UpdateCameraPosition();
    }

    void HandleInput()
    {
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        yaw += moveInput.x * sensitivity;
        pitch -= moveInput.y * sensitivity;

        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
        yaw = Mathf.Clamp(yaw, yawMin, yawMax);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    void UpdateCameraPosition()
    {
        pivotPosition = transform.position;
        desiredCameraPos = pivotPosition - transform.forward * maxCameraDistance;

        RaycastHit hit;
        if (Physics.Raycast(pivotPosition, -transform.forward, out hit, maxCameraDistance, collisionMask))
        {
            cameraTransform.position = hit.point + transform.forward * 0.2f;
        }
        else
        {
            cameraTransform.position = desiredCameraPos;
        }

        cameraTransform.LookAt(pivotPosition);
    }
}
