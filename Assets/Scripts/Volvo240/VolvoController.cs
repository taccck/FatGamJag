using UnityEngine;
using UnityEngine.InputSystem;

public class VolvoController : MonoBehaviour
{
    [SerializeField] private float acceleration = 25f;
    [SerializeField] private float maxSpeed = 6.66f;
    private Rigidbody rb;

    private Vector2 moveDirection;
    private bool moving;
    private float targetYaw;

    private bool drifting;

    private void OnMove(InputValue input)
    {
        moveDirection = input.Get<Vector2>();
        moving = moveDirection.x != 0 || moveDirection.y != 0;
        targetYaw = Mathf.Acos(moveDirection.x) * Mathf.Rad2Deg;
        if (moveDirection.y > 0)
            targetYaw = 360f - targetYaw;
    }

    private void OnDrift(InputValue input)
    {
        drifting = input.isPressed;
    }

    private void Drive()
    {        
        if (moving && !drifting)
        {
            float deltaSpeed = Time.deltaTime * acceleration;
            Vector3 moveDirection3d = new(moveDirection.x, 0, moveDirection.y);

            if(rb.velocity.magnitude >= maxSpeed * maxSpeed)
            {
                float newDirDiff = Vector3.Dot(rb.velocity.normalized, moveDirection3d);
                float reducePrecent = Mathf.Clamp01(1 + newDirDiff);
                rb.velocity -= rb.velocity.normalized * (deltaSpeed * reducePrecent);
            }

            rb.velocity += moveDirection3d * (Time.deltaTime * acceleration);
        }
    }

    private void Turn()
    {
        if (moving)
        {
            transform.rotation = Quaternion.Euler(0, targetYaw, 0);
        }
    }

    private void Update()
    {
        Turn();
        Drive();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
