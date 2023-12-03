using System.Collections;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    [SerializeField] private Resource _resourcePrefab;
    [SerializeField] private float _spawnDelay;

    private void Start()
    {
        StartCoroutine(SpawnResources());
    }

    private IEnumerator SpawnResources()
    {
        var wait = new WaitForSeconds(_spawnDelay);

        while(enabled)
        {
            var randomPoint = Random.Range(0, _points.Length);

            Instantiate(_resourcePrefab, _points[randomPoint].transform);
            yield return wait;
        }
    }
}
