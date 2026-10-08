using UnityEngine;

public class OldDoorController : MonoBehaviour
{
    public void OpenDoor()
    {
        transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
    }
}
