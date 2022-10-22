using UnityEngine;

public class VolvoFighting : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float health = 10;

    private Rigidbody rb;
    private void OnCollisionEnter(Collision collision)
    {
        
    }

    float DealDamage()
    {
        float speedModifier = rb.velocity.magnitude / VolvoConfig.Get.baseMaxSpeed;
        return damage * speedModifier;
    }

    private void Start()
    {
        VolvoConfig.Get.baseDamage = damage;
        VolvoConfig.Get.baseHealth = health;

        VolvoConfig.Get.currDamage = damage;
        VolvoConfig.Get.currHealth = health;
    }

    private void Awake()
    {
        VolvoConfig.Init();
        rb = GetComponent<Rigidbody>();
    }
}
