using UnityEngine;
using UnityEngine.InputSystem;

public class VolvoController : MonoBehaviour
{
    [SerializeField] private float acceleration = 25f;
    [SerializeField] private float maxSpeed = 40f;
    [SerializeField] private float turnSpeed = 1f;
    [SerializeField] private float driftTurnSpeed = 3f;

    [SerializeField] private float velocityToTrailAt = 1f;
    [SerializeField] private ParticleSystem[] driftEffect;
    private Rigidbody rb;
    private TrailRenderer trail;

    private Vector2 moveDirection;
    private bool moving;
    private float targetYaw;

    private float drifting;

    private void OnMove(InputValue input)
    {
        moveDirection = input.Get<Vector2>();
        moving = moveDirection.x != 0 || moveDirection.y != 0;
        targetYaw = Mathf.Acos(moveDirection.x) * Mathf.Rad2Deg;
        if (moveDirection.y > 0)
            targetYaw = 360f - targetYaw;
    }

    private void Drive()
    {
        if (moving)
        {
            float deltaSpeed = Time.deltaTime * VolvoConfig.Get.currAcceleration;
            Vector3 carDirection = transform.right;
            Vector3 driveVelocity = rb.velocity;

            //reduce speed to turn when at max speed
            if (driveVelocity.sqrMagnitude >= VolvoConfig.Get.currMaxSpeed * VolvoConfig.Get.currMaxSpeed)
            {
                float newDirDiff = Vector3.Dot(driveVelocity.normalized, carDirection);
                float reducePrecent = Mathf.Clamp01(1 + newDirDiff);
                driveVelocity -= driveVelocity.normalized * (deltaSpeed * reducePrecent);
            }

            driveVelocity += carDirection * deltaSpeed;
            drifting = 1f - Mathf.Abs(Vector3.Dot(driveVelocity.normalized, carDirection));
            rb.velocity = driveVelocity;
        }
    }

    private void Turn()
    {
        if (moving)
        {
            Vector3 angles = transform.rotation.eulerAngles;
            float turnSpeedGraphed = Mathf.Sqrt(rb.velocity.magnitude * turnSpeed);
            float distToTurn = targetYaw - angles.y;
            int turnDirection = (int)Mathf.Sign(distToTurn);
            if (Mathf.Abs(distToTurn) > 180)
                turnDirection *= -1;

            float newYaw = angles.y + turnDirection * turnSpeedGraphed * Time.deltaTime;
            if (Mathf.Abs(angles.y - newYaw) > Mathf.Abs(angles.y - targetYaw)) newYaw = targetYaw;
            transform.rotation = Quaternion.Euler(angles.x, newYaw, angles.z);
        }
    }

    private void UpdateVFX()
    {
        float playerSpeed = rb.velocity.magnitude;
        //print(playerSpeed);
        trail.enabled = playerSpeed > velocityToTrailAt;
        foreach(ParticleSystem ps in driftEffect)
        {
            var main = ps.main;
            Color newAlpha = main.startColor.color;
            newAlpha.a = drifting;
            main.startColor = newAlpha;
            print(ps.main.startColor.color.a);
        }
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
