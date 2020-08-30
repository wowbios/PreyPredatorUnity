using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using Random = System.Random;

public class WorldController : MonoBehaviour
{
    public SpawnerSettings[] Spawners;
    public int XMin, XMax, YMin, YMax;

    public static WorldController Instance;

    public readonly Random Rand = new Random();
    private List<SpawnerSettings> _initSpawners;
    private List<SpawnerSettings> _tickSpawners;
    private DateTime[] _ticks;

    void Start()
    {
        Instance = this;
        _initSpawners = Spawners.Where(x => x.OnInit).ToList();
        _tickSpawners = Spawners.Where(x => x.OnTick).ToList();
        _ticks = Enumerable.Repeat(DateTime.Now, _tickSpawners.Count).ToArray();

        SpawnOnInit();
    }

    void Update()
    {
        SpawnOnTick();
    }

    private void SpawnOnInit()
    {
        foreach(SpawnerSettings spawner in _initSpawners)
            for (var i = 0; i < spawner.Count; i++)
                Spawn(spawner.Entity);
    }

    private void SpawnOnTick()
    {
        var now = DateTime.Now;
        for (var i = 0; i < _tickSpawners.Count; i++)
        {
            SpawnerSettings tickSpawner = _tickSpawners[i];
            if ((now - _ticks[i]).TotalMilliseconds > tickSpawner.SpawnMilliseconds)
            {
                _ticks[i] = now;
                Spawn(tickSpawner.Entity);
            }
        }
    }

    private void Spawn(GameObject prefab) => Instantiate(prefab, GetRandomPosition(), Quaternion.identity);

    public Vector2 GetRandomPosition()
    {
        float x = (float)Rand.Next(XMin * 100, XMax * 100) / 100;
        float y = (float)Rand.Next(YMin * 100, YMax * 100) / 100;
        return new Vector2(x, y);
    }
}
