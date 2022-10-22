using UnityEngine;

public class VolvoFighting : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private uint health = 10;

    private Rigidbody rb;

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
