using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactionDistance = 2.5f;
    [SerializeField] private Camera playerCamera;

    private void Update()
    {
        if (GameHUD.IsModalOpen) return;
        IInteractable target = FindTarget();
        string prompt = target is IInteractionPrompt named ? named.Prompt : "";
        if (GameHUD.Instance != null) GameHUD.Instance.SetPrompt(prompt);
        if (Input.GetKeyDown(KeyCode.E) && target != null) target.Interact();
    }

    private IInteractable FindTarget()
    {
        if (playerCamera == null) return null;
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        return Physics.Raycast(ray, out RaycastHit hit, interactionDistance)
            ? hit.collider.GetComponentInParent<IInteractable>() : null;
    }
}
