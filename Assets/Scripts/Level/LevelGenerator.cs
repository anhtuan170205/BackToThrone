using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class LevelGenerator : SingletonMonoBehaviour<LevelGenerator>
{
    [Header("References")]
    [SerializeField] private ChunkPool[] _chunkPools;
    [SerializeField] private ChunkPool _checkpointChunkPool;

    [Header("Level Settings")]
    [Tooltip("Number of chunks to spawn at the start of the game")]
    [SerializeField] private int _visibleChunkCount = 6;
    [SerializeField] private int _chunksPerCheckpoint = 10;
    [SerializeField] private float _chunkLength = 10f;
    [SerializeField] private float _chunkMoveSpeed = 5f;
    [SerializeField] private float _minChunkMoveSpeed = 2f;
    [SerializeField] private float _maxChunkMoveSpeed = 20f;
    [SerializeField] private float _minGravityZ = -22f;
    [SerializeField] private float _maxGravityZ = -2f;
    [SerializeField] private int _safeChunkCount = 5;
    private readonly List<Chunk> _activeChunks = new List<Chunk>();

    public event Action<float> OnSpeedUp;
    private Camera _mainCamera;
    private ChunkPool _lastUsedPool;
    private int _spawnedChunkCount = 0;

    private bool _isMovingChunks;
    private float _defaultChunkMoveSpeed;

    protected override void Awake()
    {
        base.Awake();
        _mainCamera = Camera.main;
        _defaultChunkMoveSpeed = _chunkMoveSpeed;
    }

    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void Update()
    {
        if (!_isMovingChunks) { return; }
        MoveChunks();
    }

    private void HandleGameStateChanged(GameState newState)
    {
        switch (newState)
        {
            case GameState.MainMenu:
                PrepareLevel();
                break;
            case GameState.InGame:
                StartGeneratingLevel();
                break;
            case GameState.GameOver:
                StopGeneratingLevel();
                break;
        }
    }

    public void PrepareLevel()
    {
        StopGeneratingLevel();
        ResetLevel();
        ResetDifficulty();
        _chunkMoveSpeed = _defaultChunkMoveSpeed;
        SpawnInitialChunks();
    }

    public void StartGeneratingLevel()
    {
        _isMovingChunks = true;
    }

    public void StopGeneratingLevel()
    {
        _isMovingChunks = false;
    }

    private void ResetLevel()
    {
        for (int i = _activeChunks.Count - 1; i >= 0; i--)
        {
            Chunk chunk = _activeChunks[i];
            if (chunk == null) { continue; }
            ChunkPool owningPool = chunk.OwningPool;
            if (owningPool != null)
            {
                owningPool.ReturnObjectToPool(chunk);
            }
            else
            {
                chunk.gameObject.SetActive(false);
            }
        }
        _activeChunks.Clear();
        _spawnedChunkCount = 0;
        _lastUsedPool = null;
    }

    private void SpawnInitialChunks()
    {
        for (int i = 0; i < _visibleChunkCount; i++)
        {
            bool shouldSpawnProps = i >= _safeChunkCount;
            SpawnChunk(shouldSpawnProps);
        }
    }

    private float CalculateSpawnPositionZ()
    {
        if (_activeChunks.Count == 0)
        {
            return transform.position.z;
        }

        Chunk lastChunk = _activeChunks[_activeChunks.Count - 1];
        return lastChunk.transform.position.z + _chunkLength;
    }

    private void SpawnChunk(bool shouldSpawnProps = true)
    {
        ChunkPool selectedPool = ShouldSpawnCheckpointBool() ? GetCheckpointPool() : GetRandomAvailablePool();
        if (selectedPool == null) { return; }

        Chunk newChunk = selectedPool.GetObjectFromPool();
        if (newChunk == null) { return; }


        float spawnPositionZ = CalculateSpawnPositionZ();
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);

        newChunk.transform.position = spawnPosition;
        newChunk.transform.rotation = Quaternion.identity;
        newChunk.SetOwningPool(selectedPool);
        newChunk.Initialize(shouldSpawnProps);

        _activeChunks.Add(newChunk);
        _spawnedChunkCount++;
    }

    private bool ShouldSpawnCheckpointBool()
    {
        if (_checkpointChunkPool == null) { return false; }
        if (_chunksPerCheckpoint <= 0) { return false; }

        int nextSpawnIndex = _spawnedChunkCount + 1;
        return nextSpawnIndex % _chunksPerCheckpoint == 0;
    }

    private ChunkPool GetCheckpointPool()
    {
        if (_checkpointChunkPool == null) { return null; }
        if (!_checkpointChunkPool.HasAvailableObjects()) { return null; }

        return _checkpointChunkPool;
    }


    private ChunkPool GetRandomAvailablePool()
    {
        if (_chunkPools == null || _chunkPools.Length == 0) { return null; }

        List<ChunkPool> availablePools = new List<ChunkPool>();

        for (int i = 0; i < _chunkPools.Length; i++)
        {
            ChunkPool pool = _chunkPools[i];
            if (pool != null && pool != _lastUsedPool && pool.HasAvailableObjects())
            {
                availablePools.Add(pool);
            }
        }

        if (availablePools.Count == 0)
        {
            for (int i = 0; i < _chunkPools.Length; i++)
            {
                ChunkPool pool = _chunkPools[i];
                if (pool != null && pool.HasAvailableObjects())
                {
                    availablePools.Add(pool);
                }
            }
        }

        if (availablePools.Count == 0) { return null; }

        ChunkPool selectedPool = availablePools[Random.Range(0, availablePools.Count)];
        _lastUsedPool = selectedPool;
        return selectedPool;
    }


    private void MoveChunks()
    {
        for (int i = _activeChunks.Count - 1; i >= 0; i--)
        {
            Chunk chunk = _activeChunks[i];
            chunk.transform.Translate(-transform.forward * _chunkMoveSpeed * Time.deltaTime);

            if (IsChunkBehindCamera(chunk))
            {
                _activeChunks.RemoveAt(i);
                ChunkPool owningPool = chunk.OwningPool;
                owningPool.ReturnObjectToPool(chunk);
                SpawnChunk();
            }
        }
    }

    private bool IsChunkBehindCamera(Chunk chunk)
    {
        return chunk.transform.position.z < _mainCamera.transform.position.z - _chunkLength;
    }

    public void ChangeLevelSpeed(float amount)
    {
        if (!_isMovingChunks) { return; }

        float oldMoveSpeed = _chunkMoveSpeed;
        float newMoveSpeed = Mathf.Clamp(_chunkMoveSpeed + amount, _minChunkMoveSpeed, _maxChunkMoveSpeed);

        if (Mathf.Approximately(oldMoveSpeed, newMoveSpeed)) { return; }

        _chunkMoveSpeed = newMoveSpeed;

        float newGravityZ = Mathf.Clamp(Physics.gravity.z - amount, _minGravityZ, _maxGravityZ);
        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);

        OnSpeedUp?.Invoke(amount);
    }

    public void IncreaseDifficulty()
    {
        _chunksPerCheckpoint += 10;
    }

    public void ResetDifficulty()
    {
        _chunksPerCheckpoint = 10;
    }

}
