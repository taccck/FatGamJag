using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBase : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] private float Health = 10;
    [SerializeField] private float Attack = 1;
    [SerializeField, Tooltip("Seconds between attacks")] private float attackCooldown = 0.5f;
    [SerializeField] private float Speed = 5;

    private float attackCooldownTimer = 0;

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckHealth();

        if (attackCooldownTimer > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = Player.transform.position - transform.position;

        body.velocity = directionToPlayer.normalized * Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (attackCooldownTimer <= 0)
        {
            Health--;
            attackCooldownTimer = attackCooldown;
        }
    }

    private void CheckHealth()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
