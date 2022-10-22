using UnityEngine;
using UnityEngine.InputSystem;

public class VolvoController : MonoBehaviour
{
    [SerializeField] private float acceleration = 25f;
    [SerializeField] private float maxSpeed = 40f;
    [SerializeField] private float turnSpeed = 1f;
    [SerializeField] private float driftTurnSpeed = 3f;

    [SerializeField] private float velocityToTrailAt = 1f;
    private Rigidbody rb;
    private TrailRenderer trail;

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
            float deltaSpeed = Time.deltaTime * VolvoConfig.Get.currAcceleration;
            Vector3 carDirection = transform.right;
            //rb.velocity = carDirection * Vector3.Dot(rb.velocity, carDirection);

            if (rb.velocity.sqrMagnitude >= VolvoConfig.Get.currMaxSpeed * VolvoConfig.Get.currMaxSpeed)
            {
                float newDirDiff = Vector3.Dot(rb.velocity.normalized, carDirection);
                float reducePrecent = Mathf.Clamp01(1 + newDirDiff);
                rb.velocity -= rb.velocity.normalized * (deltaSpeed * reducePrecent);
            }

            rb.velocity += carDirection * deltaSpeed;
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
                turnDirection *= -1;

            float newYaw = angles.y + turnDirection * deltaYaw * Time.deltaTime;
            if (Mathf.Abs(angles.y - newYaw) > Mathf.Abs(angles.y - targetYaw)) newYaw = targetYaw;
            transform.rotation = Quaternion.Euler(angles.x, newYaw, angles.z);
        }
    }

    private void UpdateVFX()
    {
        float playerSpeed = rb.velocity.magnitude;
        print(playerSpeed);
        trail.enabled = playerSpeed > velocityToTrailAt;
    }

    private void Update()
    {
        Drive();
        Turn();
        UpdateVFX();
    }

    private void Start()
    {
        VolvoConfig.Get.baseAcceleration = acceleration;
        VolvoConfig.Get.baseMaxSpeed = maxSpeed;

        VolvoConfig.Get.currAcceleration = acceleration;
        VolvoConfig.Get.currMaxSpeed = maxSpeed;

        VolvoConfig.Get.SomePlayerComponent = this;
    }

    private void Awake()
    {
        VolvoConfig.Init();
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
    }
}
