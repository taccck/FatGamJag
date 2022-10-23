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

    private int _killsToLevelUp = 10;
    private int _kills = 0;
    private List<EnemyBase> _spawnedEnemies = new List<EnemyBase>();
    private float _spawnTimer;
    private Bounds _myBounds;
    private Camera _camera;
    
    void Start()
    {
        _spawnedEnemies = new List<EnemyBase>();
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
            EnemyBase currEnemey = Instantiate(spawnableEnemies[Random.Range(0, spawnableEnemies.Length)],
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
            bool changedList = false;
            if (_spawnedEnemies[i].dead)
            {
                _spawnedEnemies.RemoveAt(i);
                changedList = true;
                _kills++;
            }
            else if (!_spawnedEnemies[i])
            {
                _spawnedEnemies.RemoveAt(i);
                changedList = true;
            }
            else if (Vector3.Magnitude(_spawnedEnemies[i].transform.position - player.transform.position) > 100)
            {
                Destroy(_spawnedEnemies[i].gameObject);
                _spawnedEnemies.RemoveAt(i);
                changedList = true;
                _spawnTimer = 0;
            }

            if (changedList) i--;
        }

        print("kills: " + _kills);
        if (_kills >= _killsToLevelUp)
        {
            KenoManager.Instance.StartCoroutine(player.GetComponentInChildren<VolvoKeno>().StartKeno());
            _kills = 0;
            _killsToLevelUp = (int)(_killsToLevelUp * 1.5f);
            maxAmountOfEnemies = (int) (maxAmountOfEnemies * 1.8f);
        }
    }
}
