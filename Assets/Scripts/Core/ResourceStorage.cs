using TMPro;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    [SerializeField] private TextMeshPro _resourcesCountTMP;

    public int ResourcesCount { get; private set; }

    public void UpdateResourcesCount()
    {
        ResourcesCount++;
        _resourcesCountTMP.text = ResourcesCount.ToString();
    }
}
