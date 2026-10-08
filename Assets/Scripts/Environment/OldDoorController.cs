using UnityEngine;

public class OldDoorController : MonoBehaviour
{
    [SerializeField] private float openingAngle = 90f;
    public bool IsOpen { get; private set; }

    public void OpenDoor()
    {
        if (IsOpen) return;
        transform.localRotation = Quaternion.Euler(0f, openingAngle, 0f);
        IsOpen = true;
        Debug.Log("Door opened");
    }
}
