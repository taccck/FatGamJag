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

    private List<EnemyBase> _spawnedEnemies = new List<EnemyBase>();
    private float _spawnTimer;
    private Bounds _myBounds;
    private Camera _camera;
    
    void Start()
    {
        _myBounds = GetComponent<Renderer>().bounds;
        _camera = Camera.main;
    }

    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0)
        {
            SpawnEnemies();
            _spawnTimer = spawnCooldown;
        }
        CheckEnemies();
        print("Enemies: " + _spawnedEnemies.Count);
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < amountOfEnemiesToSpawn; i++)
        {
            Vector3 positionOutsideOfCamera = _camera.PlaceOutsideFrustum(_myBounds, 50);
            positionOutsideOfCamera.y = player.transform.position.y;
            EnemyBase currEnemey = Instantiate(spawnableEnemies[Random.Range(0, spawnableEnemies.Length - 1)],
                positionOutsideOfCamera, quaternion.identity);
            currEnemey.Player = player;
            _spawnedEnemies.Add(currEnemey);
        }
    }

    private void CheckEnemies()
    {
        for (int i = _spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (!_spawnedEnemies[i])
            {
                _spawnedEnemies.RemoveAt(i);
            }
        }
    }
}
