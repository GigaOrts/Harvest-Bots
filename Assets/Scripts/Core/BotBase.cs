using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(ResourceScanner))]
[RequireComponent(typeof(ResourceStorage))]
[RequireComponent(typeof(BotFabric))]
public class BotBase : MonoBehaviour
{
    [SerializeField] private int _botsCountAtStart;

    private readonly List<Bot> _bots = new List<Bot>();

    private ResourceScanner _resourceScanner;
    private ResourceStorage _resourceStorage;
    private BotFabric _botFabric;

    private void Awake()
    {
        _resourceScanner = GetComponent<ResourceScanner>();
        _resourceStorage = GetComponent<ResourceStorage>();
        _botFabric = GetComponent<BotFabric>();
    }

    private void Start()
    {
        for (int i = 0; i < _botsCountAtStart; i++)
        {
            CreateBot();
        }

        StartCoroutine(RunAsync());
    }

    private void CreateBot()
    {
        Bot bot = _botFabric.SpawnBot();
        _bots.Add(bot);

        bot.Init(this, _resourceStorage);
    }

    private IEnumerator RunAsync()
    {
        var waitScanDelay = new WaitForSeconds(_resourceScanner.ScanDelay);
        var waitAfterScanDelay = new WaitForSeconds(_resourceScanner.AfterScanPause);

        Queue<Resource> freeResources;
        Queue<Bot> freeBots;

        while (enabled)
        {
            freeResources = _resourceScanner.Scan();
            freeBots = FindFreeBots();
            //yield return waitAfterScanDelay;

            SendBots(freeResources, freeBots);
            yield return waitScanDelay;
        }
    }

    private Queue<Bot> FindFreeBots()
    {
        IEnumerable<Bot> enumerable = _bots.Where(bot => bot.IsBusy == false);

        return new Queue<Bot>(enumerable);
    }

    private void SendBots(Queue<Resource> freeResources, Queue<Bot> freeBots)
    {
        while (freeResources.TryPeek(out Resource resource) && freeBots.TryPeek(out Bot bot))
        {
            if (resource.IsOrdered)
            {
                freeResources.Dequeue();
                continue;
            }

            freeBots.Dequeue();

            resource.SetOrderedStatus();
            bot.SetTarget(resource);
            bot.Run();
        }
    }
}