using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{

    [SerializeField]
    private Transform _spawnTransform;

    [SerializeField]
    private Transform[] _loganTransforms;

    
    private Coroutine _spawnTreeCoroutine;

    [SerializeField]
    private float _spawnTime = 1.0f;

    [SerializeField]
    private GameObject _treePrefab;

    [SerializeField]
    private bool _beginOnStart = true;

    private void Start()
    {
        if (_beginOnStart)
            TryStartTreeSpawnCoroutine();
    }

    public bool TryStartTreeSpawnCoroutine()
    {
        if (_spawnTreeCoroutine != null)
            return false;

        _spawnTreeCoroutine = StartCoroutine(SpawnTreeCoroutine());
        return true;
    }

    public bool TryStopTreeSpawnCoroutine()
    {
        if (_spawnTreeCoroutine == null)
            return false;

        StopCoroutine(_spawnTreeCoroutine);
        _spawnTreeCoroutine = null;
        return true;
    }

    private IEnumerator SpawnTreeCoroutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_spawnTime);
            OnTreeSpawn();
        }
    }

    private void OnTreeSpawn()
    {
        GameObject instantiatedTree = Instantiate(_treePrefab, _spawnTransform);

        TreeInputProvider treeInput = instantiatedTree.GetComponentInChildren<TreeInputProvider>();

        Transform treeTarget = _loganTransforms[Random.Range(0, _loganTransforms.Length)];

        if (treeInput != null)
        {
            treeInput.LoganTransform = treeTarget;
        }

    }

}
