using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable, IInteractionPrompt
{
    [SerializeField] private Animator animator;
    public bool IsOpen { get; private set; }
    public string Prompt => "E · Abrir salida";
    public void Interact()
    {
        if (IsOpen) return;
        if (GameManager.Instance == null || !GameManager.Instance.HasKey)
        {
            GameHUD.Instance?.SetMessage("Necesitas la llave del cofre para salir.");
            return;
        }
        IsOpen = true;
        if (animator != null) animator.SetTrigger("Open");
        Collider obstacle = GetComponent<Collider>();
        if (obstacle != null) obstacle.enabled = false;
        GameManager.Instance.CompleteEscape();
        GameHUD.Instance?.ShowVictory();
        Debug.Log("Puerta abierta. Escape Room completado.");
    }
}
