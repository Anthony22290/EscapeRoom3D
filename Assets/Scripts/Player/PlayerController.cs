using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Min(0f)]
    public float speed = 5f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float mouseSensitivity = 2f;
    private CharacterController controller;
    private float pitch;
    private float verticalVelocity;

    private void Awake() => controller = GetComponent<CharacterController>();

    private void Update()
    {
        if (GameHUD.IsModalOpen || Cursor.lockState != CursorLockMode.Locked) return;
        Move();
        transform.Rotate(0f, Input.GetAxis("Mouse X") * mouseSensitivity, 0f);
        pitch = Mathf.Clamp(pitch - Input.GetAxis("Mouse Y") * mouseSensitivity, -75f, 75f);
        if (playerCamera != null) playerCamera.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = Vector3.ClampMagnitude(new Vector3(h, 0f, v), 1f);
        if (controller.isGrounded && verticalVelocity < 0f) verticalVelocity = -2f;
        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        Vector3 velocity = transform.TransformDirection(movement) * speed;
        velocity.y = verticalVelocity;
        controller.Move(velocity * Time.deltaTime);
    }
}
