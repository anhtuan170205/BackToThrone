using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleSpawner : SingletonMonoBehaviour<ObstacleSpawner>
{
    [SerializeField] private ObstaclePool[] _obstaclePools;
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private float _spawnRangeMin = -3f;
    [SerializeField] private float _spawnRangeMax = 3f;
    [SerializeField] private float _spawnHeight = 5f;

    private Coroutine _spawnCoroutine;
    private bool _isSpawning = false;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState newState)
    {
        if (newState == GameState.InGame)
        {
            StartSpawning();
        }
        else
        {
            StopSpawning();
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }

    public void StartSpawning()
    {
        if (_isSpawning) return;

        _isSpawning = true;
        _spawnCoroutine = StartCoroutine(SpawnObstacles());
    }

    public void StopSpawning()
    {
        if (!_isSpawning) return;

        _isSpawning = false;
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnObstacle()
    {
        if (_obstaclePools == null || _obstaclePools.Length == 0) { return;}

        ObstaclePool selectedPool = GetRandomPool();
        if (selectedPool == null) { return; }

        Obstacle obstacle = selectedPool.GetObjectFromPool();
        if (obstacle == null) { return; }


        Vector3 spawnPoint = GetRandomSpawnPoint();

        obstacle.SetOwningPool(selectedPool);
        obstacle.transform.position = spawnPoint;
        obstacle.transform.rotation = Random.rotation;
    }

    private ObstaclePool GetRandomPool()
    {
        int randomIndex = Random.Range(0, _obstaclePools.Length);
        return _obstaclePools[randomIndex];
    }

    private Vector3 GetRandomSpawnPoint()
    {
        float randomX = Random.Range(_spawnRangeMin, _spawnRangeMax);
        Vector3 spawnPosition = new Vector3(randomX, _spawnHeight, transform.position.z);
        return spawnPosition;
    }
}
