using UnityEngine;
using UnityEngine.InputSystem;

public class VolvoController : MonoBehaviour
{
    [SerializeField] private float acceleration = 25f;
    [SerializeField] private float maxSpeed = 6.66f;
    [SerializeField] private float turnSpeed = 1f;
    [SerializeField] private float driftTurnSpeed = 5f;
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

            if (rb.velocity.magnitude >= maxSpeed * maxSpeed)
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
            Vector3 angles = transform.rotation.eulerAngles;
            float turnSpeedGraphed = Mathf.Sqrt(rb.velocity.magnitude * turnSpeed);
            float deltaYaw = drifting ? driftTurnSpeed + turnSpeedGraphed : turnSpeedGraphed;
            float distToTurn = targetYaw - angles.y;
            int turnDirection = (int)Mathf.Sign(distToTurn);
            if(Mathf.Abs(distToTurn) > 180)
            {
                distToTurn = Mathf.Abs(distToTurn) - 180;
                turnDirection *= -1;
            }
            float percentToTurn = Mathf.Abs(distToTurn) / 180f;
            float newYaw = angles.y + turnDirection * deltaYaw * percentToTurn;
            transform.rotation = Quaternion.Euler(angles.x, newYaw, angles.z);
        }
    }

    private void Update()
    {
        Drive();
        Turn();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
