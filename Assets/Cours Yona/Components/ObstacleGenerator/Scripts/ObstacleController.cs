using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField, Tooltip("Translation speed of chunks in m/s")] private float _translationSpeed = 1f;
    [SerializeField] private int _activeChunksCount = 5;
    [SerializeField] private int _behindChunkCount = 1;
    [SerializeField] private float _stopDelayOnDamage = 0.2f;

    [Header("Components")]
    [SerializeField] private ChunkController[] _chunksPool;

    private readonly List<ChunkController> _instancedChunks = new(); // readonly : appellé qu'une fois au début + lecture plus rapide par les boucles "for"
    private float _baseTranslationSpeed;

    private float _stopDelayTimer;
    private bool _stopped;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventSystem.OnPlayerLifeUpdate += HandlePlayerLifeUpdate;
        AddBaseChunk();
    }

    private void OnDestroy()
    {
        EventSystem.OnPlayerLifeUpdate -= HandlePlayerLifeUpdate;
    }

    private void HandlePlayerLifeUpdate(int playerLifeCount)
    {
        if(playerLifeCount > 0)
        {
            _stopped = true;
        }

        _translationSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        ResetMovementAfterDelay();

        foreach (var chunk in _instancedChunks)
        {
            chunk.transform.Translate(Vector3.back * _translationSpeed *  Time.deltaTime);
        }

        UpdateChunks();
    }

    private void ResetMovementAfterDelay()
    {
        if (!_stopped)
        {
            return;
        }
        
        _stopDelayTimer += Time.deltaTime;
        if(_stopDelayTimer >= _stopDelayOnDamage)
        {
            _stopped = false;
            _translationSpeed = _baseTranslationSpeed;
            _stopDelayTimer = 0f;
        }
        
    }

    private void UpdateChunks()
    {
        List<ChunkController> behindChunks = new();

        foreach (var chunk in _instancedChunks)
        {
            if (chunk.IsBehindPlayer())
            {
                behindChunks.Add(chunk);
            }
        }

        // Delete potential chunks behind player.
        if (behindChunks.Count > _behindChunkCount)
        {
            int chunkToDeleteCount = behindChunks.Count - _behindChunkCount;

            for (int i = 0; i < chunkToDeleteCount; i++)
            {
                var chunkToDelete = behindChunks[i];
                _instancedChunks.Remove(chunkToDelete);

                Destroy(chunkToDelete.gameObject);
            }
        }

        // Add potential new chunks.
        int missingChunkCount = _activeChunksCount - _instancedChunks.Count;
        for (int i = 0;i < missingChunkCount;i++)
        {
            var chunk = AddChunk(LastActiveChunk().EndAnchor);
            _instancedChunks.Add(chunk);
        }

    }

    private void AddBaseChunk()
    {
        for (int i = 0; i < _activeChunksCount; i++)
        {
            if(i == 0)
            {
                var baseChunk = AddChunk(transform.position);
                _instancedChunks.Add(baseChunk);
                continue; // existe : return, break ...
            }

            var chunk = AddChunk(LastActiveChunk().EndAnchor);
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

    private ChunkController LastActiveChunk()
    {
        return _instancedChunks[_instancedChunks.Count - 1];
    }

}
