using UnityEngine;

public class VolvoFighting : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float health = 10;

    private Rigidbody rb;
    private void OnCollisionEnter(Collision collision)
    {
        //check tag or sumthing

        if(collision.gameObject.TryGetComponent(out EnemyBase enemy))
        {
            enemy.TakeDamage(DealDamage());
        }
    }

    float DealDamage()
    {
        float speedModifier = rb.velocity.magnitude / VolvoConfig.Get.baseMaxSpeed;
        return damage * speedModifier;
    }

    void TakeDamage(float dealtDamage)
    {
        health -= dealtDamage;
        if(health <= 0)
        {
            print("is ded");
        }
    }

    private void Start()
    {
        VolvoConfig.Get.baseDamage = damage;
        VolvoConfig.Get.baseHealth = health;

        VolvoConfig.Get.currDamage = damage;
        VolvoConfig.Get.currHealth = health;

        VolvoConfig.Get.SomePlayerComponent = this;
    }

    private void Awake()
    {
        VolvoConfig.Init();
        rb = GetComponent<Rigidbody>();
    }
}
