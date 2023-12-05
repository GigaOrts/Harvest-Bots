using System.Collections;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Transform _bag;

    private ResourceStorage _storage;
    private Resource _targetResource;
    private BotBase _botBase;

    private float _speed = 5;

    public bool IsBusy { get; private set; }

    public void Init(BotBase botBase, ResourceStorage storage)
    {
        _botBase = botBase;
        _storage = storage;
    }

    public void Run()
    {
        StartCoroutine(RunAsync());
    }

    public void SetTarget(Resource resource)
    {
        _targetResource = resource;
    }

    private void Take()
    {
        _targetResource.transform.SetParent(transform);
        _targetResource.transform.position = _bag.position;
    }

    private void Drop()
    {
        _storage.UpdateResourcesCount(1);

        Destroy(_targetResource.gameObject);
    }

    private IEnumerator RunAsync()
    {
        if (_targetResource == null)
            yield break;

        IsBusy = true;
        yield return MoveTo(_targetResource.transform);
        Take();

        yield return MoveTo(_botBase.transform);
        Drop();
        IsBusy = false;
    }

    private IEnumerator MoveTo(Transform target)
    {
        while (Vector3.Distance(transform.position, target.position) > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
            yield return null;
        }
    }
}
