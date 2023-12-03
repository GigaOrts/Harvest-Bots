using System.Collections.Generic;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius = 100f;
    [SerializeField] private float _scanDelay = 1f;

    public float ScanDelay => _scanDelay;
    public float AfterScanPause => 0.001f;

    private const string LayerResource = "Resource";

    private readonly Queue<Resource> _resourcesQueue = new Queue<Resource>();

    public Queue<Resource> Scan()
    {
        RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position, _scanRadius, Vector3.one,
            Mathf.Infinity, LayerMask.GetMask(LayerResource));

        foreach (var hitInfo in raycastHits)
        {
            var resource = hitInfo.collider.GetComponent<Resource>();

            if (resource.IsOrdered == false && _resourcesQueue.Contains(resource) == false)
                _resourcesQueue.Enqueue(resource);
        }

        return _resourcesQueue;
    }
}
