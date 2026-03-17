using UnityEngine;
using System.Collections.Generic;

public class Chunk : MonoBehaviour
{
    [SerializeField] private GameObject _fencePrefab;
    [SerializeField] private GameObject _applePrefab;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private Transform _propsParent;

    [SerializeField] private float _appleSpawnRate = 0.3f;
    [SerializeField] private float _coinSpawnRate = 0.5f;
    [SerializeField] private float _coinSeparationLength = 2f;
    [SerializeField] private int _maxCoinsInRow = 5;
    [SerializeField] private float[] _lanes = { -2.5f, 0f, 2.5f };

    private List<int> _availableLanes = new List<int> { 0, 1, 2 };
    public ChunkPool OwningPool { get; private set; }

    private void OnEnable()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoins();
    }

    private void OnDisable()
    {
        DespawnAllProps();
        _availableLanes = new List<int> { 0, 1, 2 };
    }

    private void SpawnFences()
    {
        int fenceToSpawn = Random.Range(0, 3);

        for (int i = 0; i < fenceToSpawn; i++)
        {
            int selectedLane = SelectLane();

            Vector3 spawnPosition = new Vector3(_lanes[selectedLane], 0, transform.position.z);
            Instantiate(_fencePrefab, spawnPosition, Quaternion.identity, _propsParent);
        }
    }

    private void SpawnApple()
    {
        if (Random.value > _appleSpawnRate || _availableLanes.Count == 0) return;

        int selectedLane = SelectLane();
        Vector3 spawnPosition = new Vector3(_lanes[selectedLane], transform.position.y, transform.position.z);
        Instantiate(_applePrefab, spawnPosition, Quaternion.identity, _propsParent);
    }

    private void SpawnCoins()
    {
        if (Random.value > _coinSpawnRate || _availableLanes.Count == 0) return;
        int coinsToSpawn = Random.Range(0, _maxCoinsInRow);

        float topOfChunkZ = transform.position.z + _coinSeparationLength * 2f;
        int selectedLane = SelectLane();

        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPositionZ = topOfChunkZ - i * _coinSeparationLength;
            Vector3 spawnPosition = new Vector3(_lanes[selectedLane], transform.position.y, spawnPositionZ);
            Instantiate(_coinPrefab, spawnPosition, Quaternion.identity, _propsParent);
        }
    }

    private void DespawnAllProps()
    {
        foreach (Transform child in _propsParent)
        {
            Destroy(child.gameObject);
        }
    }

    private int SelectLane()
    {
        if (_availableLanes.Count == 0) return -1;

        int randomLaneIndex = Random.Range(0, _availableLanes.Count);
        int selectedLane = _availableLanes[randomLaneIndex];
        _availableLanes.RemoveAt(randomLaneIndex);

        return selectedLane;
    }

    public void SetOwningPool(ChunkPool pool)
    {
        OwningPool = pool;
    }

}
