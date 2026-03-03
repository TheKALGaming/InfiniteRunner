using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField, Tooltip("Translation speed of chunks in m/s")] private float _translationSpeed = 1f;
    [SerializeField] private int _activeChunksCount = 5;

    [Header("Components")]
    [SerializeField] private ChunkController[] _chunksPool;

    private List<ChunkController> _instancedChunks = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddBaseChunk();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddBaseChunk()
    {
        for (int i = 0; 1 < _activeChunksCount; i++)
        {
            if(i == 0)
            {
                var baseChunk = AddChunk(transform.position);
                _instancedChunks.Add(baseChunk);
                continue; // existe : return, break ...
            }

            var chunk = AddChunk(LastChunk().EndAnchor);
            _instancedChunks.Add(chunk);
        }

        // Loop break
    }

    private ChunkController AddChunk(Vector3 position)
    {
        if (_chunksPool.Length == 0)
        {
            Debug.LogError("No chunks in pool");
            return null;
        }

        var index = Random.Range(0, _chunksPool.Length);
        ChunkController chunk = Instantiate (_chunksPool[index], position, Quaternion.identity);
        return chunk;
    }

    private ChunkController LastChunk()
    {
        return _instancedChunks[_instancedChunks.Count - 1];
    }

}
