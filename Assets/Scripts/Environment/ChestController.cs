using UnityEngine;

public class ChestController : MonoBehaviour, IInteractable
{
    [SerializeField] private bool unlocked;
    [SerializeField] private Animator animator;

    public void Unlock()
    {
        unlocked = true;
        Debug.Log("Cofre desbloqueado.");
    }

    public void Interact()
    {
        if (!unlocked)
        {
            Debug.Log("El cofre está cerrado.");
            return;
        }

        if (animator != null) animator.SetTrigger("Open");
        Debug.Log("Cofre abierto.");
    }
}
