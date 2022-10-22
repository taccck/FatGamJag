using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBase : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] private float health = 10;
    [SerializeField] private float damage = 1;
    [SerializeField, Tooltip("Seconds between attacks")] private float attackCooldown = 0.5f;
    [SerializeField] private float speed = 5;
    [SerializeField] private VisualEffect blood;

    private float _attackCooldownTimer;
    private bool _collidingWithPlayer;

    private bool _dead;
    private float _despawnTimer = 4;

    private Rigidbody _body;

    private void Start()
    {
        _body = GetComponent<Rigidbody>();
        blood.Stop();
    }

    private void Update()
    {
        Dead();

        if (_attackCooldownTimer > 0)
        {
            _attackCooldownTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (_dead) return;
        MoveTowardsPlayer();
        Attack();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !_dead)
        {
            _dead = true;
            GetComponentInChildren<SetRagdoll>().SetState(true);
            _body.mass = 0;
            _body.freezeRotation = false;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 directionToPlayer = Player.transform.position - transform.position;
        directionToPlayer.y = 0;
        
        transform.rotation = Quaternion.LookRotation(directionToPlayer);

        _body.velocity = directionToPlayer.normalized * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Player)
        {
            blood.Play();
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
    private void Attack()
    {
        if (!_collidingWithPlayer) return;
        if (_attackCooldownTimer <= 0)
        {
            Player.GetComponent<VolvoFighting>().TakeDamage(damage);
            _attackCooldownTimer = attackCooldown;
        }
    }

    private void Dead()
    {
        if (!_dead) return;
        if(_despawnTimer < 0) Destroy(gameObject);
        _despawnTimer -= Time.deltaTime;
    }
}
