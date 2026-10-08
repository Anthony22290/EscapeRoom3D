using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Min(0f)]
    public float speed = 5f;

    private void Update()
    {
        Move();
        Debug.Log("Movement system active");
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interaction key pressed");
        }
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = Vector3.ClampMagnitude(new Vector3(h, 0f, v), 1f);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
