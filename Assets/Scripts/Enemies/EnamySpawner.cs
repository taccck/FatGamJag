using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class EnamySpawner : MonoBehaviour
{
    [SerializeField] private EnemyBase[] SpawnableEnemies;
    [SerializeField] private int amountOfEnemiesToSpawn = 1;
    [SerializeField] private float spawnCooldown = 2f;
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
            spawnEnemies();
            spawnTimer = spawnCooldown;
        }
    }

    private void spawnEnemies()
    {
        for (int i = 0; i < amountOfEnemiesToSpawn; i++)
        {
            print("bounds " + myBounds);
            Vector3 positionOutsideOfCamera = camera.PlaceOutsideFrustum(myBounds, 50, true);
            print(positionOutsideOfCamera);
        }
    }
}
