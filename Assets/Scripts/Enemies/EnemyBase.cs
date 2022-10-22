using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBase : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] private float health = 10;
    [SerializeField] private float attack = 1;
    [SerializeField, Tooltip("Seconds between attacks")] private float attackCooldown = 0.5f;
    [SerializeField] private float speed = 5;

    private float _attackCooldownTimer;
    private bool _collidingWithPlayer;

    private Rigidbody _body;

    private void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckHealth();
        Attack();

        if (_attackCooldownTimer > 0)
        {
            _attackCooldownTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = Player.transform.position - transform.position;
        directionToPlayer.y = 0;

        _body.velocity = directionToPlayer.normalized * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Player)
        {
            _collidingWithPlayer = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == Player)
        {
            _collidingWithPlayer = false;
        }
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            print("Dead");
            Destroy(gameObject);
        }
    }

    private void Attack()
    {
        if (!_collidingWithPlayer) return;
        if (_attackCooldownTimer <= 0)
        {
            health--;
            _attackCooldownTimer = attackCooldown;
        }
    }
}
