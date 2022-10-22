using UnityEngine;
using UnityEngine.InputSystem;

public class VolvoController : MonoBehaviour
{
    [SerializeField] private float Acceleration = 25f;
    private Rigidbody rb;

    private Vector2 moveDirection;
    private bool moving;

    private void OnMove(InputValue input)
    {
        moveDirection = input.Get<Vector2>();
        moving = moveDirection.x != 0 || moveDirection.y != 0;
    }

    private void Update()
    {
        if (moving)
        {
            rb.velocity += new Vector3(moveDirection.x, 0, moveDirection.y) * (Time.deltaTime * Acceleration);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
