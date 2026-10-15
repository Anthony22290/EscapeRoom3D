using UnityEngine;
using UnityEngine.InputSystem;

namespace Asterion
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerController : MonoBehaviour
    {
        public Camera view;
        public float speed = 3.2f;
        public float sensitivity = 0.085f;
        public Interactable Target { get; private set; }
        CharacterController motor;
        float pitch, fallSpeed, stepDistance;

        void Awake() => motor = GetComponent<CharacterController>();

        void Update()
        {
            var game = GameManager.Instance;
            if (game == null || game.UI == null) return;
            var keyboard = Keyboard.current;
            var mouse = Mouse.current;
            if (keyboard != null)
            {
                if (keyboard.escapeKey.wasPressedThisFrame) game.UI.Escape();
                if (game.Started && !game.Won && keyboard.jKey.wasPressedThisFrame) game.UI.ToggleJournal();
                if (game.Started && !game.Won && keyboard.hKey.wasPressedThisFrame) game.UI.ShowHint();
            }
            if (!game.Started || game.Won || game.UI.ModalOpen)
            {
                Target = null;
                return;
            }
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                if (mouse != null && mouse.leftButton.wasPressedThisFrame) game.UI.LockPointer();
                return;
            }
            if (mouse != null)
            {
                Vector2 look = mouse.delta.ReadValue() * sensitivity;
                transform.Rotate(0, look.x, 0);
                pitch = Mathf.Clamp(pitch - look.y, -78, 78);
                view.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
            }
            Vector2 input = Vector2.zero;
            if (keyboard != null)
            {
                input.x = (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed ? 1 : 0) - (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed ? 1 : 0);
                input.y = (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed ? 1 : 0) - (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed ? 1 : 0);
            }
            input = Vector2.ClampMagnitude(input, 1);
            float pace = keyboard != null && keyboard.leftShiftKey.isPressed ? speed * 1.45f : speed;
            if (motor.isGrounded && fallSpeed < 0) fallSpeed = -2;
            fallSpeed += Physics.gravity.y * Time.deltaTime;
            Vector3 displacement = (transform.right * input.x + transform.forward * input.y) * pace;
            displacement.y = fallSpeed;
            Vector3 before = transform.position;
            motor.Move(displacement * Time.deltaTime);
            Vector3 travelled = transform.position - before;
            stepDistance += new Vector2(travelled.x, travelled.z).magnitude;
            if (stepDistance > 1.8f && motor.isGrounded)
            {
                stepDistance = 0;
                game.Audio.Step();
            }
            Target = null;
            if (Physics.Raycast(view.transform.position, view.transform.forward, out RaycastHit hit, 3.2f, ~0, QueryTriggerInteraction.Ignore))
                Target = hit.collider.GetComponentInParent<Interactable>();
            if (Target != null && keyboard != null && keyboard.eKey.wasPressedThisFrame) Target.Interact();
        }

        void OnApplicationFocus(bool focused)
        {
            if (!focused && GameManager.Instance != null && GameManager.Instance.Started && !GameManager.Instance.Won && !GameManager.Instance.UI.ModalOpen)
                GameManager.Instance.UI.ShowPause();
        }
    }
}
