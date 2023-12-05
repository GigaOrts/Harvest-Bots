using TMPro;
using UnityEngine;

public class ResourceStorage : MonoBehaviour
{
    [SerializeField] private TextMeshPro _resourcesCountTMP;

    public int ResourcesCount { get; private set; }

    public void UpdateResourcesCount(int amount)
    {
        ResourcesCount += amount;
        _resourcesCountTMP.text = ResourcesCount.ToString();
    }
}
