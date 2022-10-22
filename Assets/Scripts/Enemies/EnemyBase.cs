using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBase : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float Health = 10;
    [SerializeField] private float Attack = 1;
    [SerializeField] private float Speed = 5;

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        moveTowardsPlayer();
    }

    private void moveTowardsPlayer()
    {
        Vector3 directionToPlayer = Player.transform.position - transform.position;

        body.velocity = directionToPlayer.normalized * Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ToDo: Attack or take damage
    }
}
