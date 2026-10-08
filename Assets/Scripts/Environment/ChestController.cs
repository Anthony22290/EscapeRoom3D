using UnityEngine;

public class ChestController : MonoBehaviour, IInteractable, IInteractionPrompt
{
    [SerializeField] private bool unlocked;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject keyObject;
    public bool Unlocked => unlocked;
    public bool IsOpen { get; private set; }
    public string Prompt => IsOpen ? "Cofre abierto" : "E · Abrir cofre";

    public void Unlock()
    {
        unlocked = true;
        Debug.Log("Cofre desbloqueado.");
    }

    public void Interact()
    {
        if (!unlocked)
        {
            GameHUD.Instance?.SetMessage("El cofre está cerrado. Resuelve el código del terminal.");
            return;
        }

        if (IsOpen) return;
        IsOpen = true;
        if (animator != null) animator.SetTrigger("Open");
        if (keyObject != null) keyObject.SetActive(true);
        GameHUD.Instance?.SetMessage("Cofre abierto. Recoge la llave dorada con E.");
        Debug.Log("Cofre abierto.");
    }
}
