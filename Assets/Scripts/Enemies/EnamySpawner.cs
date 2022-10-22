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
    [SerializeField] private int maxAmountOfEnemies = 20;
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
        }
        CheckEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < amountOfEnemiesToSpawn; i++)
        {
            if (_spawnedEnemies.Count >= maxAmountOfEnemies) 
                return;
            
            Vector3 positionOutsideOfCamera = _camera.PlaceOutsideFrustum(_myBounds, 50);
            positionOutsideOfCamera.y = player.transform.position.y;
            EnemyBase currEnemey = Instantiate(spawnableEnemies[Random.Range(0, spawnableEnemies.Length - 1)],
                positionOutsideOfCamera, quaternion.identity, transform);
            currEnemey.Player = player;
            _spawnedEnemies.Add(currEnemey);
        }
        _spawnTimer = spawnCooldown;
    }

    private void CheckEnemies()
    {
        for (int i = _spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (!_spawnedEnemies[i])
            {
                _spawnedEnemies.RemoveAt(i);
            }

            if (Vector3.Magnitude(_spawnedEnemies[i].transform.position - player.transform.position) > 100)
            {
                Destroy(_spawnedEnemies[i].gameObject);
                _spawnedEnemies.RemoveAt(i);
                _spawnTimer = 0;
            }
        }
    }
}
