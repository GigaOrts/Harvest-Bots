using UnityEngine;

public class BotFabric : MonoBehaviour
{
    [SerializeField] private Bot _botPrefab;

    public Bot SpawnBot()
    {
        return Instantiate(_botPrefab, transform);
    }
}
