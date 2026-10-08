using UnityEngine;

public class KeyItem : MonoBehaviour, IInteractable, IInteractionPrompt
{
    public string Prompt => "E · Recoger llave";
    public void Interact()
    {
        if (!gameObject.activeInHierarchy || GameManager.Instance == null) return;
        GameManager.Instance.ObtainKey();
        gameObject.SetActive(false);
        GameHUD.Instance?.SetMessage("Llave obtenida. Ve a la puerta de salida.");
        Debug.Log("Llave obtenida.");
    }
}
