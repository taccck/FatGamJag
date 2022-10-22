using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class EnamySpawner : MonoBehaviour
{
    [SerializeField] private EnemyBase[] spawnableEnemies;
    [SerializeField] private GameObject player;
    [SerializeField] private int amountOfEnemiesToSpawn = 5;
    [SerializeField] private float spawnCooldown = 5f;
    private float spawnTimer = 0;

    private Bounds myBounds;
    private Camera camera;
    void Start()
    {
        myBounds = GetComponent<Renderer>().bounds;
        camera = Camera.main;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnEnemies();
            spawnTimer = spawnCooldown;
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < amountOfEnemiesToSpawn; i++)
        {
            print("bounds " + myBounds);
            Vector3 positionOutsideOfCamera = camera.PlaceOutsideFrustum(myBounds, 50);
            positionOutsideOfCamera.y = player.transform.position.y;
            EnemyBase currEnemey = Instantiate(spawnableEnemies[Random.Range(0, spawnableEnemies.Length - 1)],
                positionOutsideOfCamera, quaternion.identity);
            currEnemey.Player = player;
        }
    }
}
