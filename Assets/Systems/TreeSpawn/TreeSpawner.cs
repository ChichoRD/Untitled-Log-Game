using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEditor;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{

    [SerializeField]
    private Transform _spawnTransform;

    [SerializeField]
    private Transform[] _loganTransforms;

    

    private Timer _spawnTimer;

    [SerializeField]
    private float _spawnTime = 1.0f;

    [SerializeField]
    private GameObject _treePrefab;



    private void Awake()
    {
        _spawnTimer = new Timer(_spawnTime * 1000)
        {
            Enabled = true,
            AutoReset = true
        };

        _spawnTimer.Elapsed += OnSpawnTimerElapsed;
    }

    private void OnSpawnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        GameObject instantiatedTree = Instantiate(_treePrefab, _spawnTransform);

       TreeInputProvider treeInput = instantiatedTree.GetComponentInChildren<TreeInputProvider>();


        Transform treeTarget = _loganTransforms[Random.Range(0,_loganTransforms.Length)];

        if (treeInput != null)
        {
            treeInput.LoganTransform = treeTarget;
        }
    }
}
