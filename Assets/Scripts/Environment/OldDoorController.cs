using UnityEngine;

public class OldDoorController : MonoBehaviour
{
    [SerializeField] private float openingAngle = 90f;
    public bool IsOpen { get; private set; }

    public void OpenDoor()
    {
        if (IsOpen) return;
        // Caso 17: regresión deliberada que se deshará con Revert.
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        IsOpen = true;
        Debug.Log("Door opened");
    }
}
