using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSwawner : MonoBehaviour
{
    [SerializeField] private float _maxTime = 1.5f;
    [SerializeField] private float _heightRange = 0.45f;

    [SerializeField] private GameObject _pipePrefab1; // voor score < 10
    [SerializeField] private GameObject _pipePrefab2; // voor score 10 - 30
    [SerializeField] private GameObject _pipePrefab3; // voor score > 30
    [SerializeField] private GameObject _pipePrefab4; // voor score < 10
    [SerializeField] private GameObject _pipePrefab5; // voor score 10 - 30
    [SerializeField] private GameObject _pipePrefab6; // voor score > 30
   

    private float _timer;

    void Start()
    {
        SpawnPipe();
    }

    void Update()
    {
        if (_timer > _maxTime)
        {
            SpawnPipe();
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    private void SpawnPipe()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-_heightRange, _heightRange));

        int score = Score.Instance != null ? Score.Instance.GetScore() : 0;
       

        GameObject prefabToUse;

        if (score <= 6)
        {
            prefabToUse = _pipePrefab1;
        }
        else if (score <= 16)
        {
            prefabToUse = _pipePrefab2;
        }
        else if(score <= 26)
        {
            prefabToUse = _pipePrefab3;
        }
        else if (score <= 36)
        {
            prefabToUse = _pipePrefab4;
        }
        else if (score <= 46)
        {
            prefabToUse = _pipePrefab5;
        }
        else
        {
            prefabToUse = _pipePrefab6;
        }
       

        GameObject pipe = Instantiate(prefabToUse, spawnPos, Quaternion.identity);
        Destroy(pipe, 10f);
    }
}
